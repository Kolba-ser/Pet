namespace Pet.Infrastructure
{
    public class GameLoopState : IState
    {
        private StateMachine gameStateMachine;

        public GameLoopState(StateMachine gameStateMachine)
        {
            this.gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
        }

        public void Exit()
        {
        }
    }
}