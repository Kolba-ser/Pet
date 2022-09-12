using System;

namespace Pet.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public WorldData WorldData;
        public PlayerState HeroState;
        public Stats HeroStats;
        public KillData KilledData;

        public PlayerProgress(string initialLevel)
        {
            WorldData = new WorldData(initialLevel);
            HeroState = new PlayerState();
            HeroStats = new Stats();
            KilledData = new KillData();
        }
    }
}