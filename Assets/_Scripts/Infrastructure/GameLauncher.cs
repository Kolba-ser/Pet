using UnityEngine;

namespace Pet.Infrastructure
{
    public class GameLauncher : MonoBehaviour
    {
        [SerializeField] private GameBootstrapper _gameBootstrapper;

        private void Awake()
        {
            var bootstraper = FindObjectOfType<GameBootstrapper>();

            if (!bootstraper)
                Instantiate(_gameBootstrapper);
        }
    }
}