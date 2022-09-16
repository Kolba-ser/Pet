using System.Linq;
using UnityEngine;

namespace Pet.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private EnemyAnimator _enemyAnimator;
        [SerializeField] private float _attackCooldown = 3f;
        [SerializeField] private float _cleavege = 0.5f;
        [SerializeField] private float _effectiveDistance;
        [SerializeField] private float _damage = 10;

        private Transform _heroTransform;
        private float currentAttackCooldown;

        private bool _isAttacked;
        private int _layerMask;
        private Collider[] _hits = new Collider[1];
        private bool _attackIsActive;

        public void Construct(Transform hero, float damage, float cleavage, float effectiveDistance)
        {
            _heroTransform = hero.transform;
            _damage = damage;
            _effectiveDistance = effectiveDistance;
            _cleavege = cleavage;
        }

        private void Awake()
        {
            _layerMask = 1 << LayerMask.NameToLayer("Player");
        }

        private void Update()
        {
            UpdateCooldown();

            if (CanAttack())
            {
                Debug.Log("Attack");
                StartAttack();
            }
        }

        public void EnableAttack() =>
            _attackIsActive = true;

        public void DisableAttack() =>
            _attackIsActive = false;

        private void OnAttack()
        {
            if (Hit(out Collider hit))
            {
                Extensions.DrawRay(hit.transform.position, _cleavege, 2);
                hit.transform.GetComponent<IDamagable>().TakeDamage(_damage);
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