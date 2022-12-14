using Pet.Data;
using Pet.Factory;
using Pet.Service.Randomizer;
using UnityEngine;

namespace Pet.Enemy
{
    public class LootSpawner : MonoBehaviour
    {
        [SerializeField] private EnemyDeath enemyDeath;

        private IGameFactory _factory;

        private int _lootMin;
        private int _lootMax;

        private IRandomService _random;

        public void Construct(IGameFactory gameFactory, IRandomService random)
        {
            _factory = gameFactory;
            _random = random;
        }

        private void Start()
        {
            enemyDeath.OnDeath += SpawnLootAsync;
        }

        private async void SpawnLootAsync()
        {
            EnemyLoot enemyLoot = await _factory.CreateLoot();
            enemyLoot.transform.position = transform.position;

            Loot lootItem = GenerateLoot();

            enemyLoot.Initialiaze(lootItem);
        }

        private Loot GenerateLoot() => new Loot()
        {
            Value = _random.Range(_lootMin, _lootMax)
        };

        public void SetLoot(int min, int max)
        {
            _lootMin = min;
            _lootMax = max;
        }
    }
}