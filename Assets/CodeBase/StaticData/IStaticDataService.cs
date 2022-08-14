using CodeBase.Infrastructure.Services;

namespace CodeBase.StaticData
{
    public interface IStaticDataService : IService
    {
        public MonsterStaticData ForMonster(MonsterTypeId typeId);
        public void LoadMonsters();
    }
}