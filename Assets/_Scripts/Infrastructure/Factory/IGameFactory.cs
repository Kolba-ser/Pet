using Pet.Enemy;
using Pet.Infrastructure.Services;
using Pet.Infrastructure.Services.Progress;
using Pet.StaticData;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Pet.Infrastructure.Factory
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

        public Task<EnemyLoot> CreateLoot();

        public Task<GameObject> CreateMonster(EnemyType typeId, Transform parent);

        public Task CreateSpawner(Vector3 position, string spawnerId, EnemyType monsterTypeId);

        Task WarmUp();
    }
}