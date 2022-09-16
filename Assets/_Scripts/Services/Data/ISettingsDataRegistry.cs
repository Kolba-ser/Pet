using Pet.Service;

namespace Pet.StaticData
{
    public interface ISettingsDataRegistry : IService
    {
        public MonsterSettings ForMonster(EnemyType typeId);

        public void Load();

        public LevelSettings ForLevel(string sceneKey);
    }
}