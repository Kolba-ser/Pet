using CodeBase.UI;
using CodeBase.UI.Services;
using System;

namespace CodeBase.StaticData
{
    [Serializable]
    public class WindowConfig
    {
        public WindowId WindowId;
        public WindowBase Prefab;
    }
}