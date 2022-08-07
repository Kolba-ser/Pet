using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
    public class RotateToHero : Follow
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private float _speed;

        private Transform _heroTransform;
        private IGameFactory _gameFactory;

        private Vector3 _positionToLook;

        private void Start()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();

            if (_gameFactory.Hero)
                InitializeHero();
            else
                _gameFactory.HeroCreated += HeroCreated;
        }

        private bool Initialized() =>
            _heroTransform != null;

        private void HeroCreated() =>
            InitializeHero();

        private void InitializeHero() =>
            _heroTransform = _gameFactory.Hero.transform;

        private void Update()
        {
            if (Initialized())
                RotateTowardsHero();
        }

        private void RotateTowardsHero()
        {
            UpdatePositionLookAtTarget();

            transform.rotation = SmoothRotation(transform.rotation, _positionToLook);
        }

        private Quaternion SmoothRotation(Quaternion rotation, Vector3 positionToLook)
        {
            return Quaternion.Lerp(rotation, TargetRotation(positionToLook), SpeedFactor());
        }

        private Quaternion TargetRotation(Vector3 positionToLook) =>
            Quaternion.LookRotation(positionToLook);


        private float SpeedFactor() => 
            _speed * Time.deltaTime;

        private void UpdatePositionLookAtTarget()
        {
            Vector3 difference = _heroTransform.position - transform.position;
            _positionToLook = new Vector3(difference.x, transform.position.y, difference.z);
        }
    }
}