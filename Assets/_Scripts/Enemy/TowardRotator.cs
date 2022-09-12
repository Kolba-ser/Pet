using UnityEngine;
using UnityEngine.AI;

namespace Pet.Enemy
{
    public class TowardRotator : Follower
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private float _speed;

        private Transform _heroTransform;

        private Vector3 _positionToLook;

        public void Construct(Transform hero) =>
            _heroTransform = hero.transform;

        private bool Initialized() =>
            _heroTransform != null;

        private void Update()
        {
            if (Initialized())
                RotateTowards();
        }

        private void RotateTowards()
        {
            UpdatePositionLookAtTarget();

            transform.rotation = SmoothRotation(transform.rotation, _positionToLook);
        }

        private Quaternion SmoothRotation(Quaternion rotation, Vector3 positionToLook)
        {
            return Quaternion.Lerp(rotation, TargetRotation(positionToLook), SpeedFactor());
        }

        private Quaternion TargetRotation(Vector3 positionToLook) =>
            Quaternion.LookRotation(positionToLook);

        private float SpeedFactor() =>
            _speed * Time.deltaTime;

        private void UpdatePositionLookAtTarget()
        {
            Vector3 difference = _heroTransform.position - transform.position;
            _positionToLook = new Vector3(difference.x, transform.position.y, difference.z);
        }
    }
}