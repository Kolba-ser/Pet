using System;

namespace Pet.Data
{
    [Serializable]
    public class PlayerState
    {
        public float CurrentHealth;
        public float MaxHealth;

        public void ResetHealth() =>
            CurrentHealth = MaxHealth;
    }
}