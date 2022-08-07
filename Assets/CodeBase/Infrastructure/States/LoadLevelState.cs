using CodeBase.CameraLogic;
using CodeBase.Infrastructure.Factory;
using CodeBase.Logic;
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

        private const string INITIAL_POINT_TAG = "InitialPoint";


        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingScreen loadingScreen, IGameFactory gameFactory)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingScreen = loadingScreen;
            _gameFactory = gameFactory;
        }

        public void Enter(string sceneName)
        {
            _sceneLoader.Load(sceneName, OnLoaded);
            _loadingScreen.Show();
        }
        public void Exit() 
        {
            _loadingScreen.Hide();
        }

        private void OnLoaded()
        {
            var initialPoint = GameObject.FindWithTag(INITIAL_POINT_TAG);

            var hero = _gameFactory.CreateHero(initialPoint.transform.position);
            _gameFactory.CreateHUD();
            CameraFollow(hero);

            _gameStateMachine.Enter<GameLoopState>();
        }

        private void CameraFollow(GameObject target) =>
            Camera.main.GetComponent<CameraFollow>().Follow(target);


    }
}