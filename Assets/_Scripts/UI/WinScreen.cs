using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Pet.UI
{
    public class WinScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _screen;

        private void Awake()
        {
            if (SceneManager.GetActiveScene().name == "MainMenu")
                Destroy(gameObject);
        }

        private void OnEnable() =>
            FadeOut();

        private IEnumerator FadeOut()
        {
            gameObject.SetActive(true);
            _screen.alpha = 0;
            while (_screen.alpha < 1)
            {
                _screen.alpha += 0.03f;
                yield return new WaitForSeconds(0.03f);
            }
        }
    }
}