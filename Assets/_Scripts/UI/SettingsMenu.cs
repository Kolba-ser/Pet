using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Pet.UI
{
    public class SettingsMenu : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private AudioMixerGroup  _group;
        [SerializeField] private GameObject _content;

        private void Awake()
        {
            if (PlayerPrefs.HasKey("Volume"))
            {
                float volume = PlayerPrefs.GetFloat("Volume");
                _group.audioMixer.SetFloat("Volume", volume);
                _slider.value = volume;
            }

            _slider.onValueChanged.AddListener(ChangeVolume);
        }


        public void Open() =>
            _content.SetActive(true);

        private void ChangeVolume(float volume)
        {
            _group.audioMixer.SetFloat("Volume", volume);

            PlayerPrefs.SetFloat("Volume", volume);
        }


    }
}