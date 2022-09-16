using Pet.Logic.EnemySpawners;
using Pet.UI;
using System;
using UnityEngine;

namespace Logic.EnemySpawners
{
    public class WinTrigger : MonoBehaviour
    {
        [SerializeField] private WinScreen _winScreen;

        private bool _isTriggered;

        private void OnTriggerEnter(Collider other)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (_isTriggered || enemies.Length > 0)
                return;

            _isTriggered = true;
            Instantiate(_winScreen);
        }
    }
}
