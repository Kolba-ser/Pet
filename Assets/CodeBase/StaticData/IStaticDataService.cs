using CodeBase.Infrastructure.Services;
using CodeBase.UI.Services;

namespace CodeBase.StaticData
{
    public interface IStaticDataService : IService
    {
        public MonsterStaticData ForMonster(MonsterTypeId typeId);
        public void Load();
        public LevelStaticData ForLevel(string sceneKey);
        public WindowConfig ForWindow(WindowId shop);
    }
}