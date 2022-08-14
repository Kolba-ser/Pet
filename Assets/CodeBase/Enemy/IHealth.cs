using System;

namespace CodeBase.Enemy
{
    public interface IHealth
    {
        public float Current { get; set;}
        public float Max { get; set; }

        public event Action OnHealthChange;

        void TakeDamage(float damage);
    }
}