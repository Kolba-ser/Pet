using Pet.Data;
using Pet.Enemy;
using Pet.Factory;
using Pet.Service.Progress;
using Pet.StaticData;
using System;
using UnityEngine;

namespace Pet.Logic.EnemySpawners
{
    public class EnemySpawner : MonoBehaviour, ISavedProgress
    {
        private IGameFactory _gameFactory;
        private EnemyType _typeId;
        private EnemyDeath _enemyDeath;
        private bool _isDead;
        private string _id;

        public bool IsDead => _isDead;

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
                _isDead = true;
            }
            else
                Spawn();
        }

        public void Save(PlayerProgress progress)
        {
            if (_isDead)
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
            _isDead = true;
        }
    }
}