using Pet.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Pet.StaticData
{
    public class SettingsDataRegistry : ISettingsDataRegistry
    {
        private Dictionary<EnemyType, MonsterSettings> _monstres;
        private Dictionary<string, LevelSettings> _levels;

        public void Load()
        {
            _monstres =
                Resources.LoadAll<MonsterSettings>(AssetAddress.STATIC_DATA_MONSTERS_PATH)
                         .ToDictionary(x => x.TypeId, x => x);
            _levels =
                Resources.LoadAll<LevelSettings>(AssetAddress.STATIC_DATA_LEVELS_PATH)
                         .ToDictionary(x => x.LevelKey, x => x);
        }

        public MonsterSettings ForMonster(EnemyType typeId) =>
            _monstres.TryGetValue(typeId, out MonsterSettings staticData)
            ? staticData
            : null;

        public LevelSettings ForLevel(string levelKey) =>
            _levels.TryGetValue(levelKey, out LevelSettings staticData)
            ? staticData
            : null;
    }
}