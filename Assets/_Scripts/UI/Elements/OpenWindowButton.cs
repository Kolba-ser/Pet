using Pet.UI.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Pet.UI.Elements
{
    public class OpenWindowButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        [SerializeField] private WindowType _windowId;

        private IWindowService _windowService;

        public void Construct(IWindowService windowService) =>
            _windowService = windowService;

        private void Awake() =>
            _button.onClick.AddListener(Open);

        private void Open() =>
            _windowService.Open(_windowId);
    }
}