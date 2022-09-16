using Pet.SaveLoad;
using Pet.Service;
using Pet.Service.Progress;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Pet.UI
{
    public class DeathScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _screen;
        [SerializeField] private Button _button;

        private void Awake()
        {
            Services.Container.Single<IProgressHolderService>().NewProgress();
            PlayerPrefs.DeleteAll();
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
