using Pet.Data;
using Pet.Enemy;
using Pet.Factory;
using Pet.Services.Progress;
using Pet.StaticData;
using UnityEngine;

namespace Pet.Logic.EnemySpawners
{
    public class SpawnPoint : MonoBehaviour, ISavedProgress
    {
        private IGameFactory _gameFactory;
        private EnemyType _typeId;
        private EnemyDeath _enemyDeath;
        private bool _dead;
        private string _id;

        public void Construct(string id, EnemyType monsterTypeId, IGameFactory gameFactory)
        {
            _id = id;
            _typeId = monsterTypeId;
            _gameFactory = gameFactory;
        }

        public void Load(PlayerProgress progress)
        {
            if (progress.KilledData.ClearedSpawner.Contains(_id))
            {
                _dead = true;
            }
            else
                Spawn();
        }

        public void Save(PlayerProgress progress)
        {
            if (_dead)
            {
                progress.KilledData.ClearedSpawner.Add(_id);
            }
        }

        private async void Spawn()
        {
            GameObject monster = await _gameFactory.CreateMonster(_typeId, transform);
            _enemyDeath = monster.GetComponent<EnemyDeath>();
            _enemyDeath.OnDeath += Die;
        }

        private void Die()
        {
            if (_enemyDeath != null)
                _enemyDeath.OnDeath -= Die;
            _dead = true;
        }
    }
}