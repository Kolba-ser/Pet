using UnityEngine;
using UnityEngine.UI;

namespace Pet.UI
{
    [RequireComponent(typeof(Button))]
    public class ExitButton : MonoBehaviour
    {
        private void Awake() => 
            GetComponent<Button>().onClick.AddListener(Exit);

        private void Exit() => 
            Application.Quit();
    }
}