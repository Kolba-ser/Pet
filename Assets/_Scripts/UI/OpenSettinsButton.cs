using UnityEngine;
using UnityEngine.UI;

namespace Pet.UI
{
    [RequireComponent(typeof(Button))]
    public class OpenSettinsButton : MonoBehaviour
    {
        [SerializeField] private SettingsMenu settingsMenu;

        private void Awake() => 
            GetComponent<Button>().onClick.AddListener(OpenSettingsMenu);

        private void OpenSettingsMenu() =>
            settingsMenu.Open();
    }
}