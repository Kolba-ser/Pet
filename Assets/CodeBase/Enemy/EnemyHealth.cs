using System;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        [field: SerializeField] public float Current { get; set; }
        [field: SerializeField] public float Max { get; set; }

        [SerializeField] private EnemyAnimator enemyAnimator;

        public event Action OnHealthChange;

        public void TakeDamage(float damage)
        {
            Current -= damage;

            enemyAnimator.PlayHit();

            OnHealthChange?.Invoke();
        }
    }
}
