using Pet.Data;
using Pet.Infrastructure.Services.Progress;
using UnityEngine;
using UnityEngine.UI;

namespace Pet.UI
{
    public class WindowBase : MonoBehaviour
    {
        [SerializeField] protected Button closeButton;
        protected IProgressHolderService PersistentProgress;
        protected PlayerProgress Progress => PersistentProgress.Progress;

        public void Construct(IProgressHolderService persistentProgress)
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

        protected virtual void Initialize()
        {
        }

        protected virtual void SubscribeUpdates()
        {
        }

        protected virtual void CleanUp()
        {
        }
    }
}