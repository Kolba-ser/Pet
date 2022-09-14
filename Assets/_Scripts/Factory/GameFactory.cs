using Pet.Enemy;
using Pet.AssetManagment;
using Pet.Services.Progress;
using Pet.Logic.EnemySpawners;
using Pet.Services.Randomizer;
using Pet.StaticData;
using Pet.UI;
using Pet.UI.Elements;
using Pet.UI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using Pet.Infrastructure;

namespace Pet.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetsProvider _assets;
        private readonly ISettingsDataRegistry _staticData;
        private readonly IRandomService _random;
        private readonly IProgressHolderService _persistentProgress;
        private readonly IWindowService _windowService;

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        private GameObject _hero;

        public GameFactory(IAssetsProvider assets, ISettingsDataRegistry staticData, IRandomService random, IProgressHolderService persistentProgress, IWindowService windowService)
        {
            _assets = assets;
            _staticData = staticData;
            _random = random;
            _persistentProgress = persistentProgress;
            _windowService = windowService;
        }

        public void CleanUp()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
            _assets.CleanUp();
        }

        public async Task WarmUp()
        {
            await _assets.Load<GameObject>(AssetAddress.LOOT);
            await _assets.Load<GameObject>(AssetAddress.SPAWNER);
        }

        public async Task<GameObject> CreateHUDAsync()
        {
            GameObject hud = await InstantiateRegisteredAsync(AssetAddress.HUD_PATH);

            hud.GetComponentInChildren<LootPresenter>()
                .Construct(_persistentProgress.Progress.WorldData);

            foreach (OpenWindowButton button in hud.GetComponentsInChildren<OpenWindowButton>())
                button.Construct(_windowService);

            return hud;
        }

        public async Task<GameObject> CreateMonster(EnemyType typeId, Transform parent)
        {
            MonsterSettings monsterData = _staticData.ForMonster(typeId);

            GameObject prefab = await _assets.Load<GameObject>(monsterData.PrefabReference);

            GameObject monster = UnityEngine.Object.Instantiate(prefab, parent.position, Quaternion.identity, parent);

            IDamagable health = monster.GetComponent<IDamagable>();
            health.Current = monsterData.Hp;
            health.Max = monsterData.Hp;

            monster.GetComponent<HealthPresenter>().Construct(health);
            monster.GetComponent<AgentMover>().Construct(_hero.transform, monsterData.MinimalMoveDistance);
            monster.GetComponent<NavMeshAgent>().speed = monsterData.MoveSpeed;

            EnemyAttack attack = monster.GetComponent<EnemyAttack>();
            attack.Construct(_hero.transform, monsterData.Damage, monsterData.Cleavage, monsterData.EffectiveDistance);

            monster.GetComponent<TowardRotator>()?.Construct(_hero.transform);
            LootSpawner lootSpawner = monster.GetComponentInChildren<LootSpawner>();
            lootSpawner.Construct(this, new RandomService());
            lootSpawner.SetLoot(monsterData.MinLootValue, monsterData.MaxLootValue);

            return monster;
        }

        public async Task<EnemyLoot> CreateLoot()
        {
            var prefab = await _assets.Load<GameObject>(AssetAddress.LOOT);

            EnemyLoot lootPiece = InstantiateRegistered(prefab).GetComponent<EnemyLoot>();
            lootPiece.Construct(_persistentProgress.Progress.WorldData);
            return lootPiece;
        }

        public async Task CreateSpawner(Vector3 position, string spawnerId, EnemyType monsterTypeId)
        {
            var prefab = await _assets.Load<GameObject>(AssetAddress.SPAWNER);

            SpawnPoint spawnPoint = InstantiateRegistered(prefab, position).GetComponent<SpawnPoint>();
            spawnPoint.Construct(spawnerId, monsterTypeId, this);
        }

        public async Task<GameObject> CreateHeroAsync(Vector3 at)
        {
            _hero = await InstantiateRegisteredAsync(AssetAddress.HERO_PATH, at);
            return _hero;
        }

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
            {
                var obj = progressReader as UnityEngine.Object;
                var name = obj != null ? obj.name : "xz";
                Debug.Log($"Зареган {name}");
                Register(progressReader);
            }
        }

        private async Task<GameObject> InstantiateRegisteredAsync(string path, Vector3 at)
        {
            var gameObject = await _assets.Instantiate(path, at);
            RegisterProgressWatchers(gameObject);

            return gameObject;
        }

        private GameObject InstantiateRegistered(GameObject prefab, Vector3 at)
        {
            var gameObject = Object.Instantiate(prefab, at, Quaternion.identity);
            RegisterProgressWatchers(gameObject);

            return gameObject;
        }

        private GameObject InstantiateRegistered(GameObject prefab)
        {
            var gameObject = Object.Instantiate(prefab);
            RegisterProgressWatchers(gameObject);

            return gameObject;
        }

        private async Task<GameObject> InstantiateRegisteredAsync(string path)
        {
            var gameObject = await _assets.Instantiate(path);
            RegisterProgressWatchers(gameObject);

            return gameObject;
        }

        private void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
                ProgressWriters.Add(progressWriter);

            ProgressReaders.Add(progressReader);
        }
    }
}