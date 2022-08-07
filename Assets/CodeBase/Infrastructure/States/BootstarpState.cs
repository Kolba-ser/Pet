using CodeBase.Infrastructure.AssetManagment;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Services.Input;
using System;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class BootstarpState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _allServices;

        private const string INITIAL = "Initial";

        public BootstarpState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, AllServices allServices)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _allServices = allServices;

            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(INITIAL, EnterLoadLevel);
        }

        public void Exit()
        {
            
        }

        private void EnterLoadLevel()
        {
            _gameStateMachine.Enter<LoadLevelState, string>("Main");
        }

        private void RegisterServices()
        {
            _allServices.RegisterSingle<IInputService>(InputService());
            _allServices.RegisterSingle<IAssets>(new AssetProvider());
            _allServices.RegisterSingle<IGameFactory>(new GameFactory(AllServices.Container.Single<IAssets>()));
        }

        private static IInputService InputService()
        {
#if UNITY_EDITOR
            return new StandaloneInputService();
#else
            retutn new MobileInputService();
#endif
        }
    }
}
