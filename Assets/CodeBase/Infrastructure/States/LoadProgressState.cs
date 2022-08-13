using CodeBase.Data;
using CodeBase.Infrastructure.SaveLoad;
using CodeBase.Infrastructure.Services.PersistentProgress;
using System;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService progressService, ISaveLoadService saveLoadService)
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