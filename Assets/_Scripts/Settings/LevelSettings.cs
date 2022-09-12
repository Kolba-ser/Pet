using System.Collections.Generic;
using UnityEngine;

namespace Pet.StaticData
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Settings/Level")]
    public class LevelSettings : ScriptableObject
    {
        public string LevelKey;
        public List<EnemySpawnerSettings> EnemySpawners;

        public Vector3 InitialHeroPosition;
    }
}