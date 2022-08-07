using CodeBase.Logic;
using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {

        [SerializeField] private LoadingScreen loadingScreen;

        private Game _game;

        private void Awake()
        {
            _game = new Game(this, loadingScreen);
            _game._stateMachine.Enter<BootstarpState>();

            DontDestroyOnLoad(this);
        }
    }
}