namespace ReactionMachine
{
    public class SimpleReactionController : IController
    {
        private IGui gui = null!;
        private IRandom rng = null!;
        private IState currentState = null!;
        private int tickCount;

        // State interface for the State Pattern
        private interface IState
        {
            void CoinInserted();
            void GoStopPressed();
            void Tick();
        }

        public void Connect(IGui gui, IRandom rng)
        {
            this.gui = gui;
            this.rng = rng;
            Init();
        }

        public void Init()
        {
            currentState = new WaitingForCoinState(this);
            tickCount = 0;
            gui.SetDisplay("Insert coin");
        }

        public void CoinInserted()
        {
            if (currentState == null)
                Init();
            currentState?.CoinInserted();
        }

        public void GoStopPressed()
        {
            if (currentState == null)
                Init();
            currentState?.GoStopPressed();
        }

        public void Tick()
        {
            if (currentState == null)
                Init();
            tickCount++;
            currentState?.Tick();
        }

        // Helper method to change state
        private void ChangeState(IState newState)
        {
            currentState = newState;
            tickCount = 0;
        }

        private static string FormatTime(int ticks)
        {
            double seconds = ticks * 0.01;
            return seconds.ToString("0.00");
        }

        // State: Waiting for coin insertion
        private class WaitingForCoinState(SimpleReactionController controller) : IState
        {
            private readonly SimpleReactionController controller = controller;

            public void CoinInserted()
            {
                controller.gui.SetDisplay("Press GO!");
                controller.ChangeState(new WaitingForGoState(controller));
            }

            public void GoStopPressed() { }

            public void Tick() { }
        }

        // State: Waiting for GO button press
        private class WaitingForGoState(SimpleReactionController controller) : IState
        {
            private readonly SimpleReactionController controller = controller;

            public void CoinInserted() { }

            public void GoStopPressed()
            {
                controller.gui.SetDisplay("Wait...");
                int randomDelay = controller.rng.GetRandom(100, 251);
                controller.ChangeState(new WaitingPeriodState(controller, randomDelay));
            }

            public void Tick() { }
        }

        // State: Random waiting period
        private class WaitingPeriodState(SimpleReactionController controller, int delay) : IState
        {
            private readonly SimpleReactionController controller = controller;
            private readonly int randomDelay = delay;

            public void CoinInserted() { }

            public void GoStopPressed()
            {
                controller.gui.SetDisplay("Insert coin");
                controller.ChangeState(new WaitingForCoinState(controller));
            }

            public void Tick()
            {
                if (controller.tickCount >= randomDelay)
                {
                    controller.gui.SetDisplay("0.00");
                    controller.ChangeState(new MeasuringState(controller));
                }
            }
        }

        // State: Measuring reaction time
        private class MeasuringState : IState
        {
            private readonly SimpleReactionController controller;

            public MeasuringState(SimpleReactionController controller)
            {
                this.controller = controller;
                controller.tickCount = 0;
            }

            public void CoinInserted() { }

            public void GoStopPressed()
            {
                string finalTime = FormatTime(controller.tickCount);
                controller.gui.SetDisplay(finalTime);
                controller.ChangeState(new GameOverState(controller));
            }

            public void Tick()
            {
                if (controller.tickCount >= 200)
                {
                    string finalTime = FormatTime(200);
                    controller.gui.SetDisplay(finalTime);
                    controller.ChangeState(new GameOverState(controller));
                }
                else
                {
                    controller.gui.SetDisplay(FormatTime(controller.tickCount));
                }
            }
        }

        // State: Game over, showing results
        private class GameOverState(SimpleReactionController controller) : IState
        {
            private readonly SimpleReactionController controller = controller;

            public void CoinInserted() { }

            public void GoStopPressed()
            {
                controller.gui.SetDisplay("Insert coin");
                controller.ChangeState(new WaitingForCoinState(controller));
            }

            public void Tick()
            {
                if (controller.tickCount >= 300)
                {
                    controller.gui.SetDisplay("Insert coin");
                    controller.ChangeState(new WaitingForCoinState(controller));
                }
            }
        }
    }
}
