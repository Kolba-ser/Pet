using CodeBase.CameraLogic;
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

        private const string HUD_PATH = @"Hud/Hud";
        private const string HERO_PATH = @"Hero/hero";
        private const string INITIAL_POINT_TAG = "InitialPoint";

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, Logic.LoadingScreen loadingScreen)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingScreen = loadingScreen;
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

            GameObject hero = Instantiate(HERO_PATH, initialPoint.transform.position);
            Instantiate(HUD_PATH);
            CameraFollow(hero);

            _gameStateMachine.Enter<GameLoopState>();
        }
        private void CameraFollow(GameObject target) =>
            Camera.main.GetComponent<CameraFollow>().Follow(target);

        private static GameObject Instantiate(string path, Vector3 at) =>
            Object.Instantiate(Resources.Load<GameObject>(path), at, Quaternion.identity);
        private static GameObject Instantiate(string path) =>
           Object.Instantiate(Resources.Load<GameObject>(path));
    }
}