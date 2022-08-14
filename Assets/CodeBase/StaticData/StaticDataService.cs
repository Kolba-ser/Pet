using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string STATIC_DATA_MONSTERS_PATH = "StaticData/Monsters";
        private Dictionary<MonsterTypeId, MonsterStaticData> _monstres;

        public void LoadMonsters()
        {
            _monstres = Resources.LoadAll<MonsterStaticData>(STATIC_DATA_MONSTERS_PATH)
                                 .ToDictionary(x => x.TypeId, x => x);
        }

        public MonsterStaticData ForMonster(MonsterTypeId typeId)
        {
            if (_monstres.TryGetValue(typeId, out MonsterStaticData staticData))
                return staticData;

            return null;
        }
    }
}
