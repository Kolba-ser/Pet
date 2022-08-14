using CodeBase.Data;
using CodeBase.Enemy;
using CodeBase.Infrastructure.Services.PersistentProgress;
using System;
using UnityEngine;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(HeroAnimator))]
    public class HeroHealth : MonoBehaviour, ISavedProgress, IHealth
    {
        [SerializeField] private HeroAnimator _heroAnimator;

        private HeroState _heroState;

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