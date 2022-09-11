using Assets.CodeBase.UI;
using CodeBase.CameraLogic;
using CodeBase.Hero;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Logic;
using CodeBase.StaticData;
using CodeBase.UI.Services;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure
{
    public class LoadLevelState : IPayloadState<string>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingScreen _loadingScreen;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _persistentProgress;
        private readonly IStaticDataService _staticData;
        private readonly IUIFactory _uiFactory;
        private const string INITIAL_POINT_TAG = "InitialPoint";

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingScreen loadingScreen, IGameFactory gameFactory, IPersistentProgressService persistentProgress, IStaticDataService staticData, IUIFactory uiFactory)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingScreen = loadingScreen;
            _gameFactory = gameFactory;
            _persistentProgress = persistentProgress;
            _staticData = staticData;
            _uiFactory = uiFactory;
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

        private void OnLoaded()
        {
            InitUIRoot();
            InitGameWorld();
            InformProgressReaders();

            _gameStateMachine.Enter<GameLoopState>();
        }

        private void InitUIRoot() => 
            _uiFactory.CreateUIRoot();

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
            {
                progressReader.LoadProgress(_persistentProgress.Progress);
            }
        }

        private void InitGameWorld()
        {
            InitSpawners();

            var initialPoint = GameObject.FindWithTag(INITIAL_POINT_TAG);
            var hero = _gameFactory.CreateHero(initialPoint.transform.position);
            InitHud(hero);
            CameraFollow(hero);
        }

        private void InitSpawners()
        {
            string sceneKey = SceneManager.GetActiveScene().name;
            LevelStaticData levelData = _staticData.ForLevel(sceneKey);
            foreach (EnemySpawnerData spawnerData in levelData.EnemySpawners)
            {
                _gameFactory.CreateSpawner(spawnerData.Position, spawnerData.Id, spawnerData.MonsterTypeId);
            }
        }

        private void InitHud(GameObject hero)
        {
            GameObject hud = _gameFactory.CreateHUD();

            hud.GetComponentInChildren<ActorUI>().Construct(hero.GetComponent<HeroHealth>());
        }

        private void CameraFollow(GameObject target) =>
            Camera.main.GetComponent<CameraFollow>().Follow(target);
    }
}