using Pet.UI;
using UnityEngine;

namespace Pet.Player
{
    public class PlayerDeath : MonoBehaviour
    {
        [SerializeField] private DeathScreen _deathScreen;
        [SerializeField] private PlayerHealth _health;
        [SerializeField] private PlayerMovement _heroMove;
        [SerializeField] private PlayerAttack _heroAttack;
        [SerializeField] private PlayerAnimator _heroAnimator;

        [SerializeField] private GameObject _deathFx;
        private bool _isDeath;

        private void Start() =>
            _health.OnHealthChange += HealthChanged;

        private void OnDestroy() =>
            _health.OnHealthChange -= HealthChanged;

        private void HealthChanged()
        {
            if (!_isDeath && _health.Current <= 0)
                Die();
        }

        private void Die()
        {
            _isDeath = true;
            _heroMove.enabled = false;
            _heroAttack.enabled = false;
            _heroAnimator.PlayDeath();
            Instantiate(_deathScreen);
            Instantiate(_deathFx, transform.position, Quaternion.identity);
        }
    }
}