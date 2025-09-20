namespace ReactionMachine
{
    /// <summary>
    /// ReactionController implements a finite state machine for the HD Reaction Game.
    /// Each state (Idle, WaitingForGo, WaitingPeriod, Measuring, ShowResult, ShowAverage)
    /// is encapsulated as a class that implements the IState interface.
    /// </summary>
    public class ReactionController : IController
    {
        private IGui gui = null!;
        private IRandom rng = null!;
        private IState current = null!;
        private int tickCount; // counts ticks since entering current state (10ms per tick)

        // --- Timing and configuration constants ---
        private const int GamesPerCoin = 3; // number of games per coin
        private const int ResultHoldTicks = 300; // 3.00s hold at 10ms per tick
        private const int AvgHoldTicks = 500; // 5.00s hold at 10ms per tick
        private const int GoTimeoutTicks = 1000; // 10.00s timeout for "Press GO!"
        private const int MaxMeasureTicks = 200; // 2.00s maximum measurement

        // --- Session data ---
        private int gamesPlayed; // number of games completed this coin
        private double sumSeconds; // sum of measured times to compute average
        private bool cheatAbort; // true if user pressed Go during waiting period

        // --- State interface definition ---
        private interface IState
        {
            void CoinInserted();
            void GoStopPressed();
            void Tick();
        }

        /// <summary>
        /// Wire up GUI and RNG, then initialise to Idle state.
        /// </summary>
        public void Connect(IGui gui, IRandom rng)
        {
            this.gui = gui;
            this.rng = rng;
            Init();
        }

        /// <summary>
        /// Initialise the controller to Idle and reset session variables.
        /// </summary>
        public void Init()
        {
            ChangeState(new Idle(this));
            gamesPlayed = 0;
            sumSeconds = 0;
            cheatAbort = false;
            gui.SetDisplay("Insert coin");
        }

        // Event entry points delegate to current state
        public void CoinInserted() => current.CoinInserted();

        public void GoStopPressed() => current.GoStopPressed();

        /// <summary>
        /// Called every 10ms. Increments tickCount and lets current state handle time-based logic.
        /// </summary>
        public void Tick()
        {
            tickCount++;
            current.Tick();
        }

        /// <summary>
        /// Change current state and reset tick counter for the new state.
        /// </summary>
        private void ChangeState(IState s)
        {
            current = s;
            tickCount = 0;
        }

        /// <summary>
        /// Format ticks as seconds with 2 decimal places.
        /// </summary>
        private static string TimeFmt(int ticks)
        {
            double sec = ticks * 0.01; // each tick = 0.01s
            return sec.ToString("0.00");
        }

        /// <summary>
        /// Decide whether to start another game or show the average.
        /// Called after showing a result.
        /// </summary>
        private void StartNextGameOrAverage()
        {
            if (cheatAbort)
            {
                // Entire session aborted due to cheating
                EndSession();
                return;
            }

            if (gamesPlayed < GamesPerCoin)
            {
                // More games to play → start next WaitingPeriod
                gui.SetDisplay("Wait...");
                int delay = rng.GetRandom(100, 251); // random delay in ticks
                ChangeState(new WaitingPeriod(this, delay));
            }
            else
            {
                // All games complete → compute and show average
                double avg = sumSeconds / GamesPerCoin;
                gui.SetDisplay($"Average = {avg:0.00}");
                ChangeState(new ShowAverage(this));
            }
        }

        /// <summary>
        /// Reset to Idle and clear session counters.
        /// </summary>
        private void EndSession()
        {
            gui.SetDisplay("Insert coin");
            ChangeState(new Idle(this));
            gamesPlayed = 0;
            sumSeconds = 0;
            cheatAbort = false;
        }

        // ---------------- States ----------------

        /// <summary>
        /// IDLE: Waiting for a coin.
        /// Only responds to CoinInserted → transitions to WaitingForGo.
        /// </summary>
        private class Idle(ReactionController c) : IState
        {
            private readonly ReactionController c = c;

            public void CoinInserted()
            {
                c.gui.SetDisplay("Press GO!");
                c.ChangeState(new WaitingForGo(c));
            }

            public void GoStopPressed() { /* ignored */
            }

            public void Tick() { /* no effect */
            }
        }

        /// <summary>
        /// WAITING FOR GO: User must press Go within 10s to start.
        /// Timeout returns to Idle. GoStopPressed → WaitingPeriod.
        /// </summary>
        private class WaitingForGo(ReactionController c) : IState
        {
            private readonly ReactionController c = c;

            public void CoinInserted() { /* ignored */
            }

            public void GoStopPressed()
            {
                c.gui.SetDisplay("Wait...");
                int delay = c.rng.GetRandom(100, 251);
                c.ChangeState(new WaitingPeriod(c, delay));
            }

            public void Tick()
            {
                if (c.tickCount >= GoTimeoutTicks)
                {
                    // Timed out, session ends
                    c.EndSession();
                }
            }
        }

        /// <summary>
        /// WAITING PERIOD: Random delay before measuring.
        /// If user presses Go → cheating → abort whole session.
        /// Otherwise after delay → transition to Measuring.
        /// </summary>
        private class WaitingPeriod(ReactionController c, int delay) : IState
        {
            private readonly ReactionController c = c;
            private readonly int delay = delay;

            public void CoinInserted() { /* ignored */
            }

            public void GoStopPressed()
            {
                // Cheating: pressed Go too early
                c.cheatAbort = true;
                c.EndSession();
            }

            public void Tick()
            {
                if (c.tickCount >= delay)
                {
                    c.gui.SetDisplay("0.00");
                    c.ChangeState(new Measuring(c));
                }
            }
        }

        /// <summary>
        /// MEASURING: Display increments every 0.01s until user presses Go
        /// or until 2.00s cap is reached.
        /// Records time and moves to ShowResult.
        /// </summary>
        private class Measuring(ReactionController c) : IState
        {
            private readonly ReactionController c = c;

            public void CoinInserted() { /* ignored */
            }

            public void GoStopPressed()
            {
                // Record measured time (capped at 2.00s)
                int finalTicks = Math.Min(c.tickCount, MaxMeasureTicks);
                c.gui.SetDisplay(TimeFmt(finalTicks));
                double asSec = finalTicks * 0.01;
                c.sumSeconds += asSec;
                c.gamesPlayed++;
                c.ChangeState(new ShowResult(c));
            }

            public void Tick()
            {
                int t = Math.Min(c.tickCount, MaxMeasureTicks);
                c.gui.SetDisplay(TimeFmt(t));
                if (c.tickCount >= MaxMeasureTicks)
                {
                    // Auto-stop at 2.00s cap
                    double asSec = MaxMeasureTicks * 0.01;
                    c.sumSeconds += asSec;
                    c.gamesPlayed++;
                    c.ChangeState(new ShowResult(c));
                }
            }
        }

        /// <summary>
        /// SHOW RESULT: Displays a single game result for up to 3s.
        /// User can press Go to skip the hold immediately.
        /// After hold → StartNextGameOrAverage.
        /// </summary>
        private class ShowResult(ReactionController c) : IState
        {
            private readonly ReactionController c = c;

            public void CoinInserted() { /* ignored */
            }

            public void GoStopPressed()
            {
                // Skip result hold immediately
                c.StartNextGameOrAverage();
            }

            public void Tick()
            {
                if (c.tickCount >= ResultHoldTicks)
                    c.StartNextGameOrAverage();
            }
        }

        /// <summary>
        /// SHOW AVERAGE: Displays the average of three games.
        /// Ends after 5s or immediately if Go is pressed.
        /// </summary>
        private class ShowAverage(ReactionController c) : IState
        {
            private readonly ReactionController c = c;

            public void CoinInserted() { /* ignored */
            }

            public void GoStopPressed()
            {
                // User skips average → back to Idle
                c.EndSession();
            }

            public void Tick()
            {
                if (c.tickCount >= AvgHoldTicks)
                    c.EndSession();
            }
        }
    }
}
