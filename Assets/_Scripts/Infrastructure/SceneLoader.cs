using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Pet.Infrastructure
{
    public class SceneLoader
    {
        private readonly ICoroutineLauncher _coroutineRunner;

        public SceneLoader(ICoroutineLauncher coroutineRunner) =>
            _coroutineRunner = coroutineRunner;

        public void Load(string name, Action onLoaded = null) =>
            _coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));

        private IEnumerator LoadScene(string name, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == name)
            {
                onLoaded?.Invoke();
                yield break;
            }

            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(name);
            while (!waitNextScene.isDone)
                yield return null;

            onLoaded?.Invoke();
        }
    }
}