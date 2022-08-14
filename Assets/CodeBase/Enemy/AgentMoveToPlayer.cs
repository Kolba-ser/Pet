using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
    public class AgentMoveToPlayer : Follow
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
