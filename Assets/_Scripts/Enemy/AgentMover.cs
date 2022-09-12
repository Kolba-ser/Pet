using UnityEngine;
using UnityEngine.AI;

namespace Pet.Enemy
{
    public class AgentMover : Follower
    {
        [SerializeField] private NavMeshAgent _agent;

        private Transform _heroTransform;

        private float _minimalDistance;

        public void Construct(Transform hero, float minMoveDistance)
        {
            _heroTransform = hero;
            _minimalDistance = minMoveDistance;
        }

        private void Update()
        {
            if (Initialized() && HeroReached())
                _agent.destination = _heroTransform.position;
        }

        private bool Initialized() =>
            _heroTransform != null;

        private bool HeroReached() =>
            (_agent.transform.position - _heroTransform.position).sqrMagnitude >= _minimalDistance * _minimalDistance;
    }
}