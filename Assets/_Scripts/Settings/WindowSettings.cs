using System.Collections.Generic;
using UnityEngine;

namespace Pet.StaticData
{
    [CreateAssetMenu(menuName = "Settings/Windows", fileName = "New Windows")]
    public class WindowSettings : ScriptableObject
    {
        public List<UIWindow> Configs;
    }
}