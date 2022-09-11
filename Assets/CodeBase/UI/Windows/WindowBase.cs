using CodeBase.Data;
using CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace CodeBase.UI
{
    public class WindowBase : MonoBehaviour
    {
        [SerializeField] protected Button closeButton;
        protected IPersistentProgressService PersistentProgress;
        protected PlayerProgress Progress => PersistentProgress.Progress;

        public void Construct(IPersistentProgressService persistentProgress)
        {
            PersistentProgress = persistentProgress;
        }

        private void Awake() => 
            OnAwake();

        private void OnAwake() => 
            closeButton.onClick.AddListener(() => Destroy(gameObject));

        private void Start()
        {
            Initialize();
            SubscribeUpdates();
        }

        protected virtual void Initialize(){}
        protected virtual void SubscribeUpdates(){}
        protected virtual void CleanUp(){}
    }
}
