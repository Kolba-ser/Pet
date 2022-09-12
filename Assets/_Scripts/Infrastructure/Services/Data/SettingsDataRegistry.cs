using Pet.Infrastructure;
using Pet.UI.Services;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Pet.StaticData
{
    public class SettingsDataRegistry : ISettingsDataRegistry
    {
        private Dictionary<MonsterType, MonsterSettings> _monstres;
        private Dictionary<string, LevelSettings> _levels;
        private Dictionary<WindowType, UIWindow> _windowsConfigs;

        public void Load()
        {
            _monstres =
                Resources.LoadAll<MonsterSettings>(AssetAddress.STATIC_DATA_MONSTERS_PATH)
                         .ToDictionary(x => x.TypeId, x => x);
            _levels =
                Resources.LoadAll<LevelSettings>(AssetAddress.STATIC_DATA_LEVELS_PATH)
                         .ToDictionary(x => x.LevelKey, x => x);
            _windowsConfigs =
                Resources.Load<WindowSettings>(AssetAddress.STATIC_DATA_WINDOWS_PATH)
                         .Configs
                         .ToDictionary(x => x.WindowId, x => x);
        }

        public MonsterSettings ForMonster(MonsterType typeId) =>
            _monstres.TryGetValue(typeId, out MonsterSettings staticData)
            ? staticData
            : null;

        public LevelSettings ForLevel(string levelKey) =>
            _levels.TryGetValue(levelKey, out LevelSettings staticData)
            ? staticData
            : null;

        public UIWindow ForWindow(WindowType windowId) =>
            _windowsConfigs.TryGetValue(windowId, out UIWindow staticData)
            ? staticData
            : null;
    }
}