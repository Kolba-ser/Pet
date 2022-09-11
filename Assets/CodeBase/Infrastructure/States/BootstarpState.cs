using CodeBase.Infrastructure.AssetManagment;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.SaveLoad;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Services.Input;
using CodeBase.Services.Randomizer;
using CodeBase.StaticData;
using CodeBase.UI.Services;
using CodeBase.UI.Services.Factory;
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
            _gameStateMachine.Enter<LoadProgressState>();
        }

        private void RegisterServices()
        {
            RegisterStaticData();
            _allServices.RegisterSingle<IAssets>(new AssetProvider());
            _allServices.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            _allServices.RegisterSingle<IGameStateMachine>(_gameStateMachine);


            _allServices.RegisterSingle<IUIFactory>(new UIFactory(
                _allServices.Single<IAssets>(),
                _allServices.Single<IStaticDataService>(),
                _allServices.Single<IPersistentProgressService>()
                ));
            _allServices.RegisterSingle<IWindowService>(new WindowService(
                _allServices.Single<IUIFactory>()));

            _allServices.RegisterSingle<IInputService>(InputService());
            _allServices.RegisterSingle<IService>(new RandomService());
            _allServices.RegisterSingle<IGameFactory>(new GameFactory(
                _allServices.Single<IAssets>(),
                _allServices.Single<IStaticDataService>(),
                _allServices.Single<IRandomService>(),
                _allServices.Single<IPersistentProgressService>(),
                _allServices.Single<IWindowService>()));

            _allServices.RegisterSingle<ISaveLoadService>(new SaveLoadService(
                _allServices.Single<IGameFactory>(),
                _allServices.Single<IPersistentProgressService>()));


        }

        private void RegisterStaticData()
        {
            IStaticDataService staticDataService = new StaticDataService();
            staticDataService.Load();
            _allServices.RegisterSingle<IStaticDataService>(staticDataService);
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
