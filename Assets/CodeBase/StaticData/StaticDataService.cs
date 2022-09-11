using Assets.CodeBase.StaticData;
using CodeBase.Infrastructure;
using CodeBase.UI.Services;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<MonsterTypeId, MonsterStaticData> _monstres;
        private Dictionary<string, LevelStaticData> _levels;
        private Dictionary<WindowId, WindowConfig> _windowsConfigs;

        public void Load()
        {
            _monstres = 
                Resources.LoadAll<MonsterStaticData>(AssetAddress.STATIC_DATA_MONSTERS_PATH)
                         .ToDictionary(x => x.TypeId, x => x);
            _levels = 
                Resources.LoadAll<LevelStaticData>(AssetAddress.STATIC_DATA_LEVELS_PATH)
                         .ToDictionary(x => x.LevelKey, x => x);
            _windowsConfigs = 
                Resources.Load<WindowStaticData>(AssetAddress.STATIC_DATA_WINDOWS_PATH)
                         .Configs
                         .ToDictionary(x => x.WindowId, x => x);
        }

        public MonsterStaticData ForMonster(MonsterTypeId typeId) => 
            _monstres.TryGetValue(typeId, out MonsterStaticData staticData)
            ? staticData 
            : null;

        public LevelStaticData ForLevel(string levelKey) => 
            _levels.TryGetValue(levelKey, out LevelStaticData staticData) 
            ? staticData 
            : null;

        public WindowConfig ForWindow(WindowId windowId) =>
            _windowsConfigs.TryGetValue(windowId, out WindowConfig staticData)
            ? staticData
            : null;
    }
}