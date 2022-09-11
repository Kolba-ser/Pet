using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "MonserData", menuName = "StaticData/Moster")]
    public class MonsterStaticData : ScriptableObject
    {
        [FormerlySerializedAs("Prefab")]
        public AssetReference Prefab;
        [Space(20)]
        public MonsterTypeId TypeId;

        [Space(20)]
        public int MaxLootValue;
        public int MinLootValue;

        [Space(20)]
        [Range(1, 100)] public int Hp;
        [Range(1, 30)] public float Damage;

        [Space(20)]
        [Range(0.5f, 1f)] public float EffectiveDistance;
        [Range(0.5f, 1f)] public float Cleavage;
        
        [Space(20)]
        [Range(1, 5)] public float MoveSpeed;
        [Range(1, 5)] public float MinimalMoveDistance;
        

    }
}
