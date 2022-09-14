using Pet.Enemy;
using Pet.Services;
using Pet.Services.Progress;
using Pet.StaticData;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Pet.Factory
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