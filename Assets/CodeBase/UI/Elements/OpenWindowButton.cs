using CodeBase.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.CodeBase.UI.Elements
{
    public class OpenWindowButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        [SerializeField] private WindowId _windowId;
        
        private IWindowService _windowService;

        public void Construct(IWindowService windowService) => 
            _windowService = windowService;

        private void Awake() => 
            _button.onClick.AddListener(Open);

        private void Open() => 
            _windowService.Open(_windowId);
    }
}
