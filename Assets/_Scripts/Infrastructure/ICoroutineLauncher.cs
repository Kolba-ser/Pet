using System.Collections;
using UnityEngine;

namespace Pet.Infrastructure
{
    public interface ICoroutineLauncher
    {
        public Coroutine StartCoroutine(IEnumerator coroutine);
    }
}