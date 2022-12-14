using Pet.CameraLogic;
using Pet.Factory;
using Pet.Service.Progress;
using Pet.Logic;
using Pet.Player;
using Pet.StaticData;
using Pet.UI;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Pet.Infrastructure
{
    public class LoadLevelState : IPayloadState<string>
    {
        private readonly StateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingScreen _loadingScreen;
        private readonly IGameFactory _gameFactory;
        private readonly IProgressHolderService _persistentProgress;
        private readonly ISettingsDataRegistry _staticData;

        public LoadLevelState(StateMachine gameStateMachine, SceneLoader sceneLoader, LoadingScreen loadingScreen, IGameFactory gameFactory, IProgressHolderService persistentProgress, ISettingsDataRegistry staticData)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingScreen = loadingScreen;
            _gameFactory = gameFactory;
            _persistentProgress = persistentProgress;
            _staticData = staticData;
        }

        public void Enter(string sceneName)
        {
            _sceneLoader.Load(sceneName, OnLoaded);
            _gameFactory.CleanUp();
            _loadingScreen.Show();
        }

        public void Exit()
        {
            _loadingScreen.Hide();
        }

        private async void OnLoaded()
        {
            if (SceneManager.GetActiveScene().name != "MainMenu")
            {
                _gameFactory.CleanUp();
                await InitGameWorld();
                await _gameFactory.WarmUp();
                InformProgressReaders();
            }
            _gameStateMachine.Enter<GameLoopState>();
        }


        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
            {
                progressReader.Load(_persistentProgress.Progress);
            }
        }

        private async Task InitGameWorld()
        {
            LevelSettings levelData = LevelStaticData();

            await InitSpawners(levelData);
            GameObject hero = await InitHeroAsync(levelData);
            await InitHudAsync(hero, levelData);
            CameraFollow(hero);
        }

        private LevelSettings LevelStaticData()
        {
            string sceneKey = SceneManager.GetActiveScene().name;
            LevelSettings levelData = _staticData.ForLevel(sceneKey);
            return levelData;
        }

        private async Task<GameObject> InitHeroAsync(LevelSettings levelData) =>
            await _gameFactory.CreateHeroAsync(levelData.InitialHeroPosition);

        private async Task InitSpawners(LevelSettings levelData)
        {
            foreach (EnemySpawnerSettings spawnerData in levelData.EnemySpawners)
                await _gameFactory.CreateSpawner(spawnerData.Position, spawnerData.Id, spawnerData.MonsterTypeId);
        }

        private async Task InitHudAsync(GameObject hero, LevelSettings levelData)
        {
            if(SceneManager.GetActiveScene().name != "MainMenu")
            {
                GameObject hud = await _gameFactory.CreateHUDAsync();
                hud.GetComponentInChildren<HealthPresenter>().Construct(hero.GetComponent<PlayerHealth>());
            }
        }

        private void CameraFollow(GameObject target)
        {
            if(Camera.main.TryGetComponent(out CameraFollower cameraFollower))
                cameraFollower.Follow(target);
        }
    }
}