using Pet.Data;
using Pet.SaveLoad;
using Pet.Services.Progress;

namespace Pet.Infrastructure
{
    public class LoadProgressState : IState
    {
        private readonly StateMachine _gameStateMachine;
        private readonly IProgressHolderService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadProgressState(StateMachine gameStateMachine, IProgressHolderService progressService, ISaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadProgress();
            _gameStateMachine.Enter<LoadLevelState, string>("MainMenu");
        }

        public void Exit()
        {
        }

        private void LoadProgress() =>
            _progressService.Progress =
            _saveLoadService.LoadProgress()
            ?? NewProgress();

        private static PlayerProgress NewProgress()
        {
            PlayerProgress playerProgress = new PlayerProgress(initialLevel: "MainMenu");

            playerProgress.HeroState.MaxHealth = 50;
            playerProgress.HeroState.ResetHealth();
            playerProgress.HeroStats.Damage = 20;
            playerProgress.HeroStats.Radius = 1f;

            return playerProgress;
        }
    }
}