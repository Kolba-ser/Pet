using Pet.Infrastructure;
using UnityEngine;

namespace Pet.Logic
{
    public class LevelLoadTrigger : MonoBehaviour
    {
        private const string PLAYER_TAG = "Player";
        [SerializeField] private string _transferTo;

        private IStateMachine _stateMachine;

        private bool _triggered;

        private void Awake() =>
            _stateMachine = Infrastructure.Services.Services.Container.Single<IStateMachine>();

        private void OnTriggerEnter(Collider other)
        {
            if (!_triggered && other.CompareTag(PLAYER_TAG))
            {
                _stateMachine.Enter<LoadLevelState, string>(_transferTo);
                _triggered = true;
            }
        }
    }
}