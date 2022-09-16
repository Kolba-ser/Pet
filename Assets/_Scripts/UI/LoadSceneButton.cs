using Pet.Infrastructure;
using UnityEngine;
using UnityEngine.UI;

namespace Pet.UI
{
    [RequireComponent(typeof(Button))]
    public class LoadSceneButton : MonoBehaviour
    {
        [SerializeField] private string _scene;

        private IStateMachine _stateMachine;

        private void Awake()
        {
            _stateMachine = Service.Services.Container.Single<IStateMachine>();
            GetComponent<Button>().onClick.AddListener(Load);
        }

        private void Load() => 
            _stateMachine.Enter<LoadLevelState, string>(_scene);
    }
}
