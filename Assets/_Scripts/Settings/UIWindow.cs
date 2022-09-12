using Pet.UI;
using Pet.UI.Services;
using System;

namespace Pet.StaticData
{
    [Serializable]
    public class UIWindow
    {
        public WindowType WindowId;
        public WindowBase Prefab;
    }
}