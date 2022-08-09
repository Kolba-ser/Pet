using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using System;
using System.Linq;
using UnityEngine;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class Attack : MonoBehaviour
    {
        [SerializeField] private EnemyAnimator _enemyAnimator;
        [SerializeField] private float _attackCooldown = 3f;
        [SerializeField] private float _cleavege = 0.5f;
        [SerializeField] private float _effectiveDistance;

        private IGameFactory _gameFactory;


        private Transform _heroTransform;
        private float currentAttackCooldown;


        private bool _isAttacked;
        private int _layerMask;
        private Collider[] _hits = new Collider[1];
        private bool _attackIsActive;

        private void Awake()
        {
            _layerMask = 1 << LayerMask.NameToLayer("Player");

            _gameFactory = AllServices.Container.Single<IGameFactory>();
            _gameFactory.HeroCreated += OnHeroCreated;
        }

        private void Update()
        {
            UpdateCooldown();

            if (CanAttack())
                StartAttack();
        }

        public void EnableAttack() =>
            _attackIsActive = true;

        public void DisableAttack() => 
            _attackIsActive = false;

        private void OnAttack()
        {
            if (Hit(out Collider hit))
            {
                PhysicsDebug.DrawRay(hit.transform.position, _cleavege, 2);
            }
        }

        private void OnAttackedEnded()
        {
            currentAttackCooldown = _attackCooldown;
            _isAttacked = false;
        }

        private void StartAttack()
        {
            transform.LookAt(_heroTransform);
            _enemyAnimator.PlayAttack();
            _isAttacked = true;
        }

        private bool CanAttack() =>
           _attackIsActive && !_isAttacked && CooldownIsUp();

        private bool CooldownIsUp() =>
            currentAttackCooldown <= 0;

        private void OnHeroCreated() =>
            _heroTransform = _gameFactory.Hero.transform;

        private void UpdateCooldown()
        {
            if (!CooldownIsUp())
                currentAttackCooldown -= Time.deltaTime;
        }

        private bool Hit(out Collider hit)
        {
            int hitsCount = Physics.OverlapSphereNonAlloc(StartPoint(), _cleavege, _hits, _layerMask);

            hit = _hits.FirstOrDefault();
            return hitsCount > 0;
        }

        private Vector3 StartPoint()
        {
            return new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) +
                transform.forward * _effectiveDistance;
        }
    }
}
