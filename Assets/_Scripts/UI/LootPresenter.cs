using Pet.Data;
using TMPro;
using UnityEngine;

namespace Pet.UI
{
    public class LootPresenter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _counter;

        private WorldData _worldData;

        public void Construct(WorldData worldData)
        {
            _worldData = worldData;
            _worldData.LootData.OnLootValueChanged += UpdateCounter;
            UpdateCounter();
        }

        private void UpdateCounter()
        {
            _counter.text = $"{_worldData.LootData.Collected}";
        }
    }
}