using System;
using System.Collections;
using UnityEngine;

namespace Pet.Enemy
{
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] private EnemyHealth enemyHealth;
        [SerializeField] private EnemyAnimator enemyAnimator;
        [SerializeField] private AgentMover agentMoveToPlayer;

        public event Action OnDeath;

        private void Start() =>
            enemyHealth.OnHealthChange += OnHealthChange;

        private void OnDestroy() =>
            enemyHealth.OnHealthChange -= OnHealthChange;

        private void OnHealthChange()
        {
            if (enemyHealth.Current <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            enemyHealth.OnHealthChange -= OnHealthChange;
            agentMoveToPlayer.enabled = false;
            enemyAnimator.PlayDeath();
            StartCoroutine(DestroyTimer());
            OnDeath?.Invoke();
        }

        private IEnumerator DestroyTimer()
        {
            GetComponentInChildren<Collider>().enabled = false;
            yield return new WaitForSeconds(2);
            Destroy(gameObject);
        }
    }
}