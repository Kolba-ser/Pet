using Pet.Data;
using Pet.Enemy;
using Pet.Infrastructure.Services.Progress;
using System;
using UnityEngine;

namespace Pet.Player
{
    [RequireComponent(typeof(PlayerAnimator))]
    public class PlayerHealth : MonoBehaviour, ISavedProgress, IDamagable
    {
        [SerializeField] private PlayerAnimator _heroAnimator;

        private PlayerState _heroState;

        public event Action OnHealthChange;

        public float Current
        {
            get => _heroState.CurrentHealth;
            set
            {
                OnHealthChange?.Invoke();
                _heroState.CurrentHealth = value;
            }
        }

        public float Max
        {
            get => _heroState.MaxHealth;
            set => _heroState.MaxHealth = value;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _heroState = progress.HeroState;
            OnHealthChange?.Invoke();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.HeroState.CurrentHealth = Current;
        }

        public void TakeDamage(float damage)
        {
            Current -= damage;
            _heroAnimator.PlayHit();
        }
    }
}