using System;

namespace CodeBase.Data
{
    [Serializable]
    public class HeroState
    {
        public float CurrentHealth;
        public float MaxHealth;

        public void ResetHealth() => 
            CurrentHealth = MaxHealth;
    }
}
