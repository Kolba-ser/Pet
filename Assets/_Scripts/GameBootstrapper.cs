using Pet.Logic;
using UnityEngine;

namespace Pet.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineLauncher
    {
        [SerializeField] private LoadingScreen loadingScreenPrefab;

        private Game _game;

        private void Awake()
        {
            _game = new Game(this, Instantiate(loadingScreenPrefab));
            _game._stateMachine.Enter<BootstarpState>();

            DontDestroyOnLoad(this);
        }
    }
}