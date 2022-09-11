﻿using CodeBase.Data;
using CodeBase.Enemy;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Logic.EnemySpawners
{
    public class SpawnPoint : MonoBehaviour, ISavedProgress
    {
        private IGameFactory _gameFactory;
        private MonsterTypeId _typeId;
        private EnemyDeath _enemyDeath;
        private bool _slain;
        private string _id;

        public void Construct(string id, MonsterTypeId monsterTypeId, IGameFactory gameFactory)
        {
            _id = id;
            _typeId = monsterTypeId;
            _gameFactory = gameFactory;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.KilledData.ClearedSpawner.Contains(_id))
                _slain = true;
            else
                Spawn();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if (_slain)
                progress.KilledData.ClearedSpawner.Add(_id);
        }

        private void Spawn()
        {
            GameObject monster = _gameFactory.CreateMonster(_typeId, transform);
            _enemyDeath = monster.GetComponent<EnemyDeath>();
            _enemyDeath.OnDeath += Slay;
        }

        private void Slay()
        {
            if (_enemyDeath != null)
                _enemyDeath.OnDeath -= Slay;
            _slain = true;
        }
    }
}