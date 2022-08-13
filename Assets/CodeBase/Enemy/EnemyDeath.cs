using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] private EnemyHealth enemyHealth;
        [SerializeField] private EnemyAnimator enemyAnimator;
        [SerializeField] private AgentMoveToPlayer agentMoveToPlayer;

        [SerializeField] private GameObject deathFx;

        public event Action OnDeath;

        private void Start() => 
            enemyHealth.OnHealthChange += OnHealthChange;

        private void OnDestroy() => 
            enemyHealth.OnHealthChange -= OnHealthChange;

        private void OnHealthChange()
        {
            if(enemyHealth.Current <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            enemyHealth.OnHealthChange -= OnHealthChange;
            agentMoveToPlayer.enabled = false;
            enemyAnimator.PlayDeath();
            SpawnDeathFx();
            StartCoroutine(DestroyTimer());
            OnDeath?.Invoke();
        }

        private void SpawnDeathFx() => 
            Instantiate(deathFx, transform.position, Quaternion.identity);

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(3);
            Destroy(gameObject);
        }
    }
}
