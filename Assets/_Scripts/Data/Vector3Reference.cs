using System;

namespace Pet.Data
{
    [Serializable]
    public class Vector3Reference
    {
        public float X;
        public float Y;
        public float Z;

        public Vector3Reference(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}