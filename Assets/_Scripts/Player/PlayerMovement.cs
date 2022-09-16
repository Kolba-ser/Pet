using Pet.Data;
using Pet.Service.Progress;
using Pet.Service.Input;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Pet.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private float _movementSpeed = 4.0f;

        private CharacterController _characterController;
        private Camera _camera;

        private IInputService _inputService;

        public void Load(PlayerProgress progress)
        {
            if (CurrentLevel() == progress.WorldData.PositionOnLevel.Level)
            {
                Vector3Reference savedPosition = progress.WorldData.PositionOnLevel.Position;
                if (savedPosition != null)
                    Wrap(to: savedPosition);
            }
        }

        private void Wrap(Vector3Reference to)
        {
            _characterController.enabled = false;
            transform.position = to.AsUnityVector3().AddY(_characterController.height);
            _characterController.enabled = true;
        }

        public void Save(PlayerProgress progress) =>
            progress.WorldData.PositionOnLevel = new PositionOnScene(transform.position.AsVector3Data(), CurrentLevel());

        private static string CurrentLevel() =>
            SceneManager.GetActiveScene().name;

        private void Awake()
        {
            _inputService = Service.Services.Container.Single<IInputService>();

            _characterController = GetComponent<CharacterController>();
        }

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            Vector3 movementVector = Vector3.zero;

            if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
            {
                //Трансформируем экранныые координаты вектора в мировые
                movementVector = _camera.transform.TransformDirection(_inputService.Axis);
                movementVector.y = 0;
                movementVector.Normalize();

                transform.forward = movementVector;
            }

            movementVector += Physics.gravity;

            _characterController.Move(_movementSpeed * movementVector * Time.deltaTime);
        }
    }
}