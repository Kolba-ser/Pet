using Pet.Logic;
using Pet.Logic.EnemySpawners;
using Pet.StaticData;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Pet.Editor
{
    [CustomEditor(typeof(LevelSettings))]
    public class LevelStaticDataEditor : UnityEditor.Editor
    {
        private const string INITIAL_POINT_TAG = "InitialPoint";

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            LevelSettings levelData = (LevelSettings)target;

            if (GUILayout.Button("Collect"))
            {
                levelData.EnemySpawners = FindObjectsOfType<SpawnPointer>()
                    .Select(x => new EnemySpawnerSettings(x.GetComponent<UniqueId>().Id, x.MonsterTypeId, x.transform.position))
                    .ToList();

                levelData.LevelKey = SceneManager.GetActiveScene().name;
                levelData.InitialHeroPosition = GameObject.FindWithTag(INITIAL_POINT_TAG).transform.position;
            }

            EditorUtility.SetDirty(target);
        }
    }
}