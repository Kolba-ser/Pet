using UnityEngine;
using UnityEngine.AI;

namespace Pet.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(EnemyAnimator))]
    public class MovementAnimator : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private EnemyAnimator _animator;

        private const float MINIMAL_VELOCITY = 0.1f;

        private void Update()
        {
            if (ShouldMove())
                _animator.Move(_agent.velocity.magnitude);
            else
                _animator.StopMoving();
        }

        private bool ShouldMove() => _agent.velocity.magnitude >= MINIMAL_VELOCITY && _agent.velocity.magnitude > _agent.radius;
    }
}