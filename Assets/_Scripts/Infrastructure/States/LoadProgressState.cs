using Pet.Data;
using Pet.Infrastructure.SaveLoad;
using Pet.Infrastructure.Services.Progress;

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
            LoadProgressOrInitNew();
            _gameStateMachine.Enter<LoadLevelState, string>(_progressService.Progress.WorldData.PositionOnLevel.Level);
        }

        public void Exit()
        {
        }

        private void LoadProgressOrInitNew() =>
            _progressService.Progress =
            _saveLoadService.LoadProgress()
            ?? NewProgress();

        private static PlayerProgress NewProgress()
        {
            PlayerProgress playerProgress = new PlayerProgress(initialLevel: "Main");

            playerProgress.HeroState.MaxHealth = 50;
            playerProgress.HeroState.ResetHealth();
            playerProgress.HeroStats.Damage = 1;
            playerProgress.HeroStats.Radius = 0.5f;

            return playerProgress;
        }
    }
}