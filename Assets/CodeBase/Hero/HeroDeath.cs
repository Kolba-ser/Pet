using CodeBase.Hero;
using System;
using UnityEngine;

namespace Assets.CodeBase.Hero
{
    public class HeroDeath : MonoBehaviour
    {

        [SerializeField] private HeroHealth _health;
        [SerializeField] private HeroMove _heroMove;
        [SerializeField] private HeroAttack _heroAttack;
        [SerializeField] private HeroAnimator _heroAnimator;


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

            Instantiate(_deathFx, transform.position, Quaternion.identity);
        }
    }
}
