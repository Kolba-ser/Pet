using Pet.Services;
using Pet.UI.Services;

namespace Pet.StaticData
{
    public interface ISettingsDataRegistry : IService
    {
        public MonsterSettings ForMonster(EnemyType typeId);

        public void Load();

        public LevelSettings ForLevel(string sceneKey);

        public UIWindow ForWindow(WindowType shop);
    }
}