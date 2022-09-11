using CodeBase.StaticData;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase.StaticData
{
    [CreateAssetMenu(menuName = "StaticData/Windows", fileName = "New Windows")]
    public class WindowStaticData : ScriptableObject
    {
        public List<WindowConfig> Configs;
    }
}