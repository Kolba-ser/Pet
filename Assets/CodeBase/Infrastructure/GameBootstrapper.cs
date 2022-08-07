using CodeBase.Logic;
using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {

        [SerializeField] private LoadingScreen loadingScreenPrefab;

        private Game _game;

        private void Awake()
        {
            _game = new Game(this, Instantiate(loadingScreenPrefab));
            _game._stateMachine.Enter<BootstarpState>();

            DontDestroyOnLoad(this);
        }
    }
}