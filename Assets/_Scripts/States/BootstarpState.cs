using Pet.AssetManagment;
using Pet.Factory;
using Pet.SaveLoad;
using Pet.Service;
using Pet.Service.Progress;
using Pet.Service.Input;
using Pet.Service.Randomizer;
using Pet.StaticData;

namespace Pet.Infrastructure
{
    public class BootstarpState : IState
    {
        private readonly StateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly Service.Services _allServices;

        private const string INITIAL = "Initial";

        public BootstarpState(StateMachine gameStateMachine, SceneLoader sceneLoader, Service.Services allServices)
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
            RegisterAssetProvider();
            _allServices.RegisterSingle<IProgressHolderService>(new ProgressHolderService());
            _allServices.RegisterSingle<IStateMachine>(_gameStateMachine);


            _allServices.RegisterSingle<IInputService>(InputService());
            _allServices.RegisterSingle<IService>(new RandomService());
            _allServices.RegisterSingle<IGameFactory>(new GameFactory(
                _allServices.Single<IAssetsProvider>(),
                _allServices.Single<ISettingsDataRegistry>(),
                _allServices.Single<IRandomService>(),
                _allServices.Single<IProgressHolderService>()));

            _allServices.RegisterSingle<ISaveLoadService>(new SaveLoadService(
                _allServices.Single<IGameFactory>(),
                _allServices.Single<IProgressHolderService>()));
        }

        private void RegisterAssetProvider()
        {
            AssetProvider assetProvider = new AssetProvider();
            _allServices.RegisterSingle<IAssetsProvider>(assetProvider);
            assetProvider.Initialize();
        }

        private void RegisterStaticData()
        {
            ISettingsDataRegistry staticDataService = new SettingsDataRegistry();
            staticDataService.Load();
            _allServices.RegisterSingle<ISettingsDataRegistry>(staticDataService);
        }

        private static IInputService InputService()
        {
            return new StandaloneInputService();

        }
    }
}