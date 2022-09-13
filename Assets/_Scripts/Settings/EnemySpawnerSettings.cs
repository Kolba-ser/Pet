using System;
using UnityEngine;

namespace Pet.StaticData
{
    [Serializable]
    public class EnemySpawnerSettings
    {
        public string Id;
        public EnemyType MonsterTypeId;
        public Vector3 Position;

        public EnemySpawnerSettings(string id, EnemyType monsterTypeId, Vector3 position)
        {
            Id = id;
            MonsterTypeId = monsterTypeId;
            Position = position;
        }
    }
}