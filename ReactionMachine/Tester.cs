using System;

namespace ReactionMachine
{
    /// <summary>
    /// TesterHD
    /// --------
    /// High-Distinction test harness for the HD Reaction Controller.
    ///
    /// Philosophy:
    /// - Tests are STATE-FOCUSED: each test names the current state, the event, and the expected next state/display.
    /// - Deterministic: a Fake RNG is used; you MUST set the next delay BEFORE any transition that reads it.
    /// - Coverage: normal flow + all HD edge cases (timeout, cheating, skips, caps, average).
    /// - Traceable: every test has a comment explaining the intent and what requirement it proves.
    ///
    /// How to read an assertion:
    ///   Expect("TAG", "ExpectedDisplay")
    ///   TAG explains scenario; "ExpectedDisplay" is what the GUI must show right now.
    /// </summary>
    class TesterHD
    {
        // System under test (SUT)
        private static ReactionController? controller;
        private static DummyGui? gui;

        // Probe of the GUI (single source of truth for what the player sees)
        private static string? displayText;

        // Deterministic RNG hook (we set this before transitions that read RNG)
        private static int nextRnd;

        // Counters
        private static int passed = 0;
        private static int total = 0;

        static void Main(string[] args)
        {
            RunAll();
            Console.WriteLine(
                $"\n=====================================\nSummary: {passed} tests passed out of {total}"
            );
        }

        private static void RunAll()
        {
            // --- Arrange (once) ---
            controller = new ReactionController();
            gui = new DummyGui();
            gui.Connect(controller!);
            controller!.Connect(gui!, new RndGenerator());

            // Small helper to reset SUT into a known baseline (Idle)
            static void Reset(string expect)
            {
                // Purpose: return to Idle, clear session counters, show Insert coin
                controller!.Init();
                Expect("RESET", expect);
            }

            // ------------------------------------------------------------
            // 1) IDLE & COIN / BASIC NAVIGATION
            // ------------------------------------------------------------

            Reset("Insert coin");

            // Purpose: Pressing Go in Idle is a no-op (guard); remains in Idle.
            DoGoStop("GO_NOCOIN (Idle ignores Go)", "Insert coin");

            // Purpose: Insert coin transitions Idle -> WaitingForGo, shows Press GO!.
            DoInsertCoin("COIN -> WaitingForGo", "Press GO!");

            // Purpose: WaitingForGo enforces 10s timeout if the user never presses Go.
            DoTicks("GO_TIMEOUT (10s -> Idle)", 1000, "Insert coin");

            // ------------------------------------------------------------
            // 2) CHEAT HANDLING (Go during Wait...)
            // ------------------------------------------------------------

            Reset("Insert coin");
            DoInsertCoin("COIN2", "Press GO!");

            // Purpose: Proper entry into WaitingPeriod; RNG must be set BEFORE pressing Go.
            GoToWait("GO_TO_WAIT (enter Wait...)", 120);

            // Purpose: Pressing Go during Wait is cheating => abort session to Idle, no average.
            DoGoStop("CHEAT (Go during Wait -> Idle)", "Insert coin");

            // ------------------------------------------------------------
            // 3) GAME 1 (manual stop), RESULT SKIP, GAME 2 (auto cap), RESULT AUTO
            // ------------------------------------------------------------

            Reset("Insert coin");
            DoInsertCoin("COIN3", "Press GO!");

            // Enter Wait, with deterministic delay 110 ticks
            GoToWait("GO_TO_WAIT2", 110);

            // Purpose: Wait elapses -> Measuring starts at 0.00
            DoTicks("WAIT_DONE -> Measuring", 110, "0.00");

            // Purpose: Measuring updates; here, 0.05s so we can stop manually
            DoTicks("MEASURE_0_05", 5, "0.05");

            // Purpose: Manual stop records 0.05s, shows result (t.tt), enters ShowResult
            DoGoStop("STOP_0_05 -> ShowResult", "0.05");

            // Purpose: Skip ShowResult hold with Go; MUST set next RNG for the next Wait BEFORE pressing
            GoToWait("SKIP_RESULT_HOLD -> next Wait", 100);

            // Purpose: Next Wait elapses -> Measuring begins at 0.00 (Game 2)
            DoTicks("WAIT2_DONE -> Measuring", 100, "0.00");

            // Purpose: Auto cap at 2.00s (no user press)
            DoTicks("AUTO_CAP 2.00", 200, "2.00");

            // Purpose: Auto-advance from ShowResult after 3s; set next delay BEFORE the 3s ticks
            AutoAdvanceResultToNextWait("RESULT_HOLD3S auto -> Wait", 115);

            // ------------------------------------------------------------
            // 4) GAME 3 (manual short), THEN AVERAGE
            // ------------------------------------------------------------

            // Purpose: Wait elapses -> Measuring
            DoTicks("WAIT3_DONE -> Measuring", 115, "0.00");

            // Purpose: Short measure (0.12s), then manual stop
            DoTicks("MEASURE_0_12", 12, "0.12");
            DoGoStop("STOP_0_12 -> ShowResult", "0.12");

            // Purpose: Skip result hold to Average immediately
            DoGoStop("SKIP_TO_AVG", StartsWith("Average = "));

            // Purpose: Average correctness check: (0.05 + 2.00 + 0.12) / 3 = 0.72
            Expect("AVG_VALUE (0.72)", "Average = 0.72");

            // ------------------------------------------------------------
            // 5) MIXED SESSION: short, skip, auto, then Average (also exercise TO_AVG via Go)
            // ------------------------------------------------------------

            Reset("Insert coin");
            DoInsertCoin("COIN4", "Press GO!");

            GoToWait("GO->WAIT", 100);
            DoTicks("WAIT_DONE4 -> Measuring", 100, "0.00");

            // Manual stop at 0.01
            DoTicks("MEASURE_0_01", 1, "0.01");
            DoGoStop("STOP_0_01 -> ShowResult", "0.01");

            // Hold for 1s (still in ShowResult), then skip to next Wait; set RNG BEFORE pressing
            DoTicks("RESULT_HOLD_1S", 100, "0.01");
            GoToWait("SKIP_HOLD_TO_WAIT", 100);

            // Game 2
            DoTicks("WAIT_DONE5 -> Measuring", 100, "0.00");
            DoTicks("MEASURE_0_10", 10, "0.10");
            DoGoStop("STOP_0_10 -> ShowResult", "0.10");

            // Auto-advance to Game 3 (set RNG BEFORE the 3s hold)
            AutoAdvanceResultToNextWait("RESULT_HOLD_AUTO", 100);

            // Game 3
            DoTicks("WAIT_DONE6 -> Measuring", 100, "0.00");
            DoTicks("MEASURE_0_20", 20, "0.20");
            DoGoStop("STOP_0_20 -> ShowResult", "0.20");

            // Still in ShowResult; pressing Go should take us to Average immediately
            DoGoStop("TO_AVG (Go) -> Average", StartsWith("Average = "));

            // Average value check: (0.01 + 0.10 + 0.20)/3 = 0.1033.. -> 0.10
            Expect("AVG_VAL2 (0.10)", $"Average = {((0.01 + 0.10 + 0.20) / 3):0.00}");

            // Purpose: Skip Average immediately with Go -> Idle
            DoGoStop("AVG_SKIP_END -> Idle", "Insert coin");

            // ------------------------------------------------------------
            // 6) TIMEOUT WINDOW SANITY + RE-COIN
            // ------------------------------------------------------------

            // Purpose: Insert coin; waiting half the timeout remains in WaitingForGo
            DoInsertCoin("COIN5", "Press GO!");
            DoTicks("GO_WAITING (still Press GO!)", 500, "Press GO!");

            // Purpose: Extra coin while in WaitingForGo should not change state/display
            DoInsertCoin("COIN_AGAIN (no effect)", "Press GO!");

            // Purpose: Enter Wait then cheat again; confirm deterministic cheat abort
            GoToWait("GO_TO_WAIT5", 150);
            DoTicks("MID_WAIT (still Wait...)", 50, "Wait...");
            DoGoStop("CHEAT2 -> Idle", "Insert coin");

            // ------------------------------------------------------------
            // 7) FULL-AUTO SESSION (min/max bounds) + AUTO AVERAGE END
            // ------------------------------------------------------------

            Reset("Insert coin");
            DoInsertCoin("COIN6", "Press GO!");

            // Game 1: 0.30 manual
            GoToWait("GO_WAIT6", 125);
            DoTicks("WAIT6_DONE", 125, "0.00");
            DoTicks("MEAS_A_30", 30, "0.30");
            DoGoStop("STOP_A_30", "0.30");
            AutoAdvanceResultToNextWait("HOLD_AUTO1", 140);

            // Game 2: auto-cap 2.00
            DoTicks("WAIT7_DONE", 140, "0.00");
            DoTicks("MEAS_B_CAP", 200, "2.00");
            AutoAdvanceResultToNextWait("HOLD_AUTO2", 160);

            // Game 3: 0.50 manual, then Average auto after 3s, then auto end after 5s
            DoTicks("WAIT8_DONE", 160, "0.00");
            DoTicks("MEAS_C_50", 50, "0.50");
            DoGoStop("STOP_C_50", "0.50");

            // Now tick 3s to auto-advance to Average (no Go press)
            DoTicks("TO_AVG_AUTO (3s)", 300, StartsWith("Average = "));

            // Average value: (0.30 + 2.00 + 0.50)/3 = 0.9333.. -> 0.93
            Expect("AVG_VAL3 (0.93)", $"Average = {((0.30 + 2.00 + 0.50) / 3):0.00}");

            // Let Average auto-end after 5s
            DoTicks("AVG_ELAPSE 5s -> Idle", 500, "Insert coin");

            // ------------------------------------------------------------
            // 8) BOUNDARY CHECKS (min/max waits, cap boundary, zero)
            // ------------------------------------------------------------

            Reset("Insert coin");
            DoInsertCoin("COIN7", "Press GO!");

            // Min wait = 100; instant 0.00 then zero-time stop
            GoToWait("GO_WAIT7 (min wait 100)", 100);
            DoTicks("WAIT_MIN -> Measuring", 100, "0.00");
            DoTicks("MEAS_ZERO", 0, "0.00");
            DoGoStop("STOP_ZERO -> ShowResult", "0.00");

            // Next wait uses max = 250
            GoToWait("SKIP_RES_ZERO -> next Wait", 250);
            DoTicks("WAIT_MAX -> Measuring", 250, "0.00");

            // Cap boundary: 1.99 then next tick 2.00
            DoTicks("MEAS_199", 199, "1.99");
            DoTicks("MEAS_CAP final 2.00", 1, "2.00");

            // Auto-advance with mid wait and small time
            AutoAdvanceResultToNextWait("HOLD_AUTO3", 200);
            DoTicks("WAIT_MID -> Measuring", 200, "0.00");
            DoTicks("MEAS_10", 10, "0.10");
            DoGoStop("STOP_0_10_B -> ShowResult", "0.10");

            // Press Go to reveal Average screen (don’t validate numeric value here)
            DoGoStop("AVG_BOUND (Average visible)", StartsWith("Average = "));
        }

        // =====================================================================
        // Ordered helpers — enforce “set RNG BEFORE transition” discipline
        // =====================================================================

        /// <summary>
        /// Sets the next random delay and presses Go to transition into WaitingPeriod.
        /// Expectation after this call should be "Wait...".
        /// </summary>
        private static void GoToWait(string tag, int delay)
        {
            nextRnd = delay; // MUST be set BEFORE GoStopPressed (WaitingForGo->WaitingPeriod reads RNG)
            controller!.GoStopPressed();
            Expect(tag, "Wait...");
        }

        /// <summary>
        /// Sets the next random delay and advances 3s in ShowResult to auto-transition to the next Wait.
        /// Expectation after this call should be "Wait...".
        /// </summary>
        private static void AutoAdvanceResultToNextWait(string tag, int delay)
        {
            nextRnd = delay; // set BEFORE we tick the 3s hold; transition consumes RNG on entry to WaitingPeriod
            for (int i = 0; i < 300; i++)
                controller!.Tick();
            Expect(tag, "Wait...");
        }

        // =====================================================================
        // Generic helpers
        // =====================================================================

        /// <summary>
        /// Marker for "starts with" expectations (useful for Average = ...).
        /// </summary>
        private static string StartsWith(string s) => "__STARTS__" + s;

        /// <summary>
        /// Asserts that the GUI currently shows the expected string (or prefix).
        /// </summary>
        private static void Expect(string tag, string expected)
        {
            total++;
            bool ok;
            if (expected.StartsWith("__STARTS__"))
            {
                var prefix = expected.Replace("__STARTS__", "");
                ok =
                    displayText != null
                    && displayText.StartsWith(prefix, StringComparison.OrdinalIgnoreCase);
            }
            else
                ok = string.Equals(displayText, expected, StringComparison.OrdinalIgnoreCase);

            if (ok)
            {
                Console.WriteLine($"test {tag}: passed");
                passed++;
            }
            else
            {
                Console.WriteLine(
                    $"test {tag}: failed (expected '{expected}' got '{displayText}')"
                );
            }
        }

        private static void DoInsertCoin(string tag, string expect)
        {
            controller!.CoinInserted();
            Expect(tag, expect);
        }

        private static void DoGoStop(string tag, string expect)
        {
            controller!.GoStopPressed();
            Expect(tag, expect);
        }

        private static void DoTicks(string tag, int n, string expect)
        {
            for (int i = 0; i < n; i++)
                controller!.Tick();
            Expect(tag, expect);
        }

        // =====================================================================
        // Test doubles
        // =====================================================================

        /// <summary>
        /// Dummy GUI that records the last displayed text so tests can assert it.
        /// </summary>
        private class DummyGui : IGui
        {
            public void Connect(IController controller) { }

            public void Init()
            {
                displayText = "?reset?";
            }

            public void SetDisplay(string s)
            {
                displayText = s;
            }
        }

        /// <summary>
        /// Deterministic RNG. Returns 'nextRnd' clamped to the requested range.
        /// Tests MUST set 'nextRnd' BEFORE transitions that read the RNG.
        /// </summary>
        private class RndGenerator : IRandom
        {
            public int GetRandom(int from, int to)
            {
                if (nextRnd < from)
                    nextRnd = from;
                if (nextRnd >= to)
                    nextRnd = to - 1;
                return nextRnd;
            }
        }
    }
}
