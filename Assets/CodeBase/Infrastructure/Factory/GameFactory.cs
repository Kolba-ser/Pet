using Assets.CodeBase.UI;
using CodeBase.Enemy;
using CodeBase.Infrastructure.AssetManagment;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Services.Randomizer;
using CodeBase.StaticData;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {

        private readonly IAssets _assets;
        private readonly IStaticDataService _staticData;
        private readonly IRandomService _random;
        private readonly IPersistentProgressService _persistentProgress;

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        private GameObject _hero;

        public GameFactory(IAssets assets, IStaticDataService staticData, IRandomService random, IPersistentProgressService persistentProgress)
        {
            _assets = assets;
            _staticData = staticData;
            _random = random;
            _persistentProgress = persistentProgress;
        }

        public void CleanUp()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        public GameObject CreateHUD()
        {
            GameObject hud = InstantiateRegistered(AssetPath.HUD_PATH);
            
            hud.GetComponentInChildren<LootCounter>()
                .Construct(_persistentProgress.Progress.WorldData);
            
            return hud;
        }

        public GameObject CreateMonster(MonsterTypeId typeId, Transform parent)
        {
            MonsterStaticData monsterData = _staticData.ForMonster(typeId);
            GameObject monster = UnityEngine.Object.Instantiate(monsterData.Prefab, parent.position, Quaternion.identity, parent);

            IHealth health = monster.GetComponent<IHealth>();
            health.Current = monsterData.Hp;
            health.Max = monsterData.Hp;

            monster.GetComponent<ActorUI>().Construct(health);
            monster.GetComponent<AgentMoveToPlayer>().Construct(_hero.transform, monsterData.MinimalMoveDistance);
            monster.GetComponent<NavMeshAgent>().speed = monsterData.MoveSpeed;
            
            Attack attack = monster.GetComponent<Attack>();
            attack.Construct(_hero.transform, monsterData.Damage, monsterData.Cleavage, monsterData.EffectiveDistance);

            monster.GetComponent<RotateToHero>()?.Construct(_hero.transform);
            LootSpawner lootSpawner = monster.GetComponentInChildren<LootSpawner>();
            lootSpawner.Construct(this, new RandomService());
            lootSpawner.SetLoot(monsterData.MinLootValue, monsterData.MaxLootValue);

            return monster;
        }

        public LootPiece CreateLoot()
        {
            LootPiece lootPiece = InstantiateRegistered(AssetPath.LOOT).GetComponent<LootPiece>();
            lootPiece.Construct(_persistentProgress.Progress.WorldData);
            return lootPiece;
        }

        public GameObject CreateHero(Vector3 at)
        {
            _hero = InstantiateRegistered(AssetPath.HERO_PATH, at);
            return _hero;
        }

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
                Register(progressReader);
        }

        private GameObject InstantiateRegistered(string path, Vector3 at)
        {
            var gameObject = _assets.Instantiate(AssetPath.HERO_PATH, at);
            
            RegisterProgressWatchers(gameObject);

            return gameObject;
        }
        
        private GameObject InstantiateRegistered(string path)
        {
            var gameObject = _assets.Instantiate(path);
            
            RegisterProgressWatchers(gameObject);

            return gameObject;
        }

        public void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
                ProgressWriters.Add(progressWriter);

            ProgressReaders.Add(progressReader);
        }

    }
}
