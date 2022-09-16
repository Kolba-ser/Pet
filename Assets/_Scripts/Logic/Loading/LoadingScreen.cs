using System.Collections;
using UnityEngine;

namespace Pet.Logic
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _screen;

        private void Awake() => 
            DontDestroyOnLoad(this);

        public void Show()
        {
            gameObject.SetActive(true);
            _screen.alpha = 1;
        }

        public void Hide() =>
            StartCoroutine(FadeOut());

        private IEnumerator FadeOut()
        {
            while (_screen.alpha > 0)
            {
                _screen.alpha -= 0.03f;
                yield return new WaitForSeconds(0.03f);
            }

            gameObject.SetActive(false);
        }
    }
}