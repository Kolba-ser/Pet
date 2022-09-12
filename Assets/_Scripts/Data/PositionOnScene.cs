using System;

namespace Pet.Data
{
    [Serializable]
    public class PositionOnScene
    {
        public Vector3Reference Position;
        public string Level;

        public PositionOnScene(string initialLevel)
        {
            Level = initialLevel;
        }

        public PositionOnScene(Vector3Reference position, string level)
        {
            Position = position;
            Level = level;
        }
    }
}