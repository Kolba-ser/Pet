using System;

namespace CodeBase.Enemy
{
    public interface IHealth
    {
        public float Current { get; }
        public float Max { get; }

        public event Action OnHealthChange;

        void TakeDamage(float damage);
    }
}