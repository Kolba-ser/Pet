using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameRunner : MonoBehaviour
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