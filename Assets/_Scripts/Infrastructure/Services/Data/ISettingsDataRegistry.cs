using Pet.Infrastructure.Services;
using Pet.UI.Services;

namespace Pet.StaticData
{
    public interface ISettingsDataRegistry : IService
    {
        public MonsterSettings ForMonster(MonsterType typeId);

        public void Load();

        public LevelSettings ForLevel(string sceneKey);

        public UIWindow ForWindow(WindowType shop);
    }
}