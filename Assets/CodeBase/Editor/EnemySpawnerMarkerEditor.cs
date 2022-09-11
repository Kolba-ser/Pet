using CodeBase.Logic.EnemySpawners;
using UnityEditor;
using UnityEngine;

namespace Assets.CodeBase.Editor
{
    [CustomEditor(typeof(SpawnMarker))]
    public class EnemySpawnerMarkerEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(SpawnMarker enemySpawner, GizmoType gizmoType)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(enemySpawner.transform.position, 0.5f);
        }
    }
}