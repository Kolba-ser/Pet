using Pet.Data;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Pet.Enemy
{
    public class EnemyLoot : MonoBehaviour
    {
        [SerializeField] private GameObject _pumpking;

        private Loot _loot;
        private bool _picked;
        private WorldData _worldData;

        public void Construct(WorldData worldData)
        {
            _worldData = worldData;
        }

        public void Initialiaze(Loot loot)
        {
            _loot = loot;
        }

        private void OnTriggerEnter(Collider other) =>
            PickUp();

        private void PickUp()
        {
            if (_picked)
                return;

            _picked = true;

            UpdateLootValue();
            Hide();

            StartCoroutine(StartDestroyTimer());
        }

        private void UpdateLootValue()
        {
            _worldData.LootData.Collect(_loot);
        }

        private void Hide()
        {
            _pumpking.SetActive(false);
        }

        private IEnumerator StartDestroyTimer()
        {
            yield return new WaitForSeconds(1.5f);
            Destroy(gameObject);
        }
    }
}