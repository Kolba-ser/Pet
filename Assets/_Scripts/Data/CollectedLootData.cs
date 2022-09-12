using System;

namespace Pet.Data
{
    [Serializable]
    public class CollectedLootData
    {
        public int Collected;

        public event Action OnLootValueChanged;

        public void Collect(Loot loot)
        {
            Collected += loot.Value;
            OnLootValueChanged?.Invoke();
        }
    }
}