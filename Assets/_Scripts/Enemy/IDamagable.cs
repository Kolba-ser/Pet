using System;

namespace Pet.Enemy
{
    public interface IDamagable
    {
        public float Current
        {
            get; set;
        }

        public float Max
        {
            get; set;
        }

        public event Action OnHealthChange;

        void TakeDamage(float damage);
    }
}