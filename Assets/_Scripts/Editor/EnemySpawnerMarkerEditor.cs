using Pet.Logic.EnemySpawners;
using UnityEditor;
using UnityEngine;

namespace Assets.Pet.Editor
{
    [CustomEditor(typeof(SpawnPointer))]
    public class EnemySpawnerMarkerEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(SpawnPointer enemySpawner, GizmoType gizmoType)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(enemySpawner.transform.position, 0.5f);
        }
    }
}