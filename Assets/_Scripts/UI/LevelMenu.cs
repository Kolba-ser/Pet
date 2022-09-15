using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.UI
{
    public class LevelMenu : MonoBehaviour
    {
        [SerializeField] private GameObject content;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                SwitchMenuState();
        }

        private void SwitchMenuState()
        {
            content.SetActive(!content.activeInHierarchy);
            
            Time.timeScale = content.activeInHierarchy ?  0 : 1;
        }

        private void OnDestroy()
        {
            Time.timeScale = 1;
        }
    }
}