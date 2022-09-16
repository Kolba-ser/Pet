namespace Pet.Infrastructure
{
    public class Game
    {
        public StateMachine _stateMachine;

        public Game(ICoroutineLauncher coroutineRunner, Logic.LoadingScreen loadingScreen)
        {
            _stateMachine = new StateMachine(new SceneLoader(coroutineRunner), loadingScreen, Service.Services.Container);
        }
    }
}