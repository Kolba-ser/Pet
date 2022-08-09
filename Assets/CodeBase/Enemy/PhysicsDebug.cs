using UnityEngine;

namespace CodeBase.Enemy
{
    public static class PhysicsDebug
    {
        public static void DrawRay(Vector3 worldPosition, float radius, float secods)
        {
            Debug.DrawRay(worldPosition, radius * Vector3.up, Color.red, secods);
            Debug.DrawRay(worldPosition, radius * Vector3.down, Color.red, secods);
            Debug.DrawRay(worldPosition, radius * Vector3.right, Color.red, secods);
            Debug.DrawRay(worldPosition, radius * Vector3.left, Color.red, secods);
            Debug.DrawRay(worldPosition, radius * Vector3.forward, Color.red, secods);
            Debug.DrawRay(worldPosition, radius * Vector3.back, Color.red, secods);
        }
    }
}
