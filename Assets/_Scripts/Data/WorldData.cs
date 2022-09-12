using System;

namespace Pet.Data
{
    [Serializable]
    public class WorldData
    {
        public PositionOnScene PositionOnLevel;
        public CollectedLootData LootData;

        public WorldData(string initialLevel)
        {
            PositionOnLevel = new PositionOnScene(initialLevel);
            LootData = new CollectedLootData();
        }
    }
}