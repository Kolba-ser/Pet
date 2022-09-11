using CodeBase.Enemy;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.StaticData;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        public List<ISavedProgressReader> ProgressReaders
        {
            get;
        }

        public List<ISavedProgress> ProgressWriters
        {
            get;
        }

        public void CleanUp();

        public Task<GameObject> CreateHeroAsync(Vector3 at);

        public Task<GameObject> CreateHUDAsync();

        public Task<LootPiece> CreateLoot();

        public Task<GameObject> CreateMonster(MonsterTypeId typeId, Transform parent);

        public Task CreateSpawner(Vector3 position, string spawnerId, MonsterTypeId monsterTypeId);
        Task WarmUp();
    }
}