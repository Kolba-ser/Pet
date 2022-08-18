using System;

namespace CodeBase.Data
{
    [Serializable]
    public class LootData
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