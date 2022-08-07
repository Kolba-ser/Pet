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
        private IGameFactory _gameFactory;

        private const float MINIMAL_DISTANCE = 1f;

        private void Start()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();

            if (_gameFactory.Hero)
                InitializeHero();
            else
                _gameFactory.HeroCreated += HeroCreated;

        }


        private void Update()
        {
            if (Initialized() && HeroNotReached())
                _agent.destination = _heroTransform.position;
        }

        private bool Initialized() => 
            _heroTransform != null;

        private void HeroCreated() => 
            InitializeHero();

        private void InitializeHero() => 
            _heroTransform = _gameFactory.Hero.transform;

        private bool HeroNotReached() => 
            (_agent.transform.position - _heroTransform.position).sqrMagnitude >= MINIMAL_DISTANCE * MINIMAL_DISTANCE;
    }
}
