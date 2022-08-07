using CodeBase.Services.Input;
using System;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class BootstarpState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;

        private const string INITIAL = "Initial";

        public BootstarpState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            RegisterServices();
            _sceneLoader.Load(INITIAL, EnterLoadLevel);
        }

        private void EnterLoadLevel()
        {
            _gameStateMachine.Enter<LoadLevelState, string>("Main");
        }

        private void RegisterServices()
        {
            Game.InputService = SetupInputService();
        }

        public void Exit()
        {
            
        }

        private static IInputService SetupInputService()
        {
#if UNITY_EDITOR
            return new StandaloneInputService();
#else
            retutn new MobileInputService();
#endif
        }
    }
}
