using CodeBase.Infrastructure.Services;
using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class Game
    {
        public GameStateMachine _stateMachine;

        public Game(ICoroutineRunner coroutineRunner, Logic.LoadingScreen loadingScreen)
        {
            _stateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingScreen, AllServices.Container);
        }
    }
}