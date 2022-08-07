using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class Game
    {
        public GameStateMachine _stateMachine;

        public static IInputService InputService { get;  set; }

        public Game(ICoroutineRunner coroutineRunner, Logic.LoadingScreen loadingScreen)
        {
            _stateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingScreen);
        }
    }
}