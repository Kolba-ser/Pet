using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Logic
{
    public class LevelTransferTrigger : MonoBehaviour
    {
        private const string PLAYER_TAG = "Player";
        [SerializeField] private string _transferTo;

        private IGameStateMachine _stateMachine;

        private bool _triggered;

        private void Awake() => 
            _stateMachine = AllServices.Container.Single<IGameStateMachine>();

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
