using CodeBase.Data;
using CodeBase.Enemy;
using CodeBase.Hero;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Services.Input;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase.Hero
{
    public class HeroAttack : MonoBehaviour, ISavedProgressReader
    {

        [SerializeField] private HeroAnimator _heroAnimator;
        [SerializeField] private CharacterController _characterController;

        private IInputService _inputService;
        private LayerMask _layerMask;
        private Collider[] _hits= new Collider[3];
        private Stats _heroStats;

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
            _layerMask = 1 << LayerMask.NameToLayer("Hittable");
        }

        private void Update()
        {
            if(_inputService.IsAttackButtonUp() && !_heroAnimator.IsAttacking)
                _heroAnimator.PlayAttack();
        }

        public void OnAttack()
        {
            for (int i = 0; i < Hit(); i++)
            {
                _hits[i].transform.GetComponentInParent<IHealth>().TakeDamage(_heroStats.Damage);
            }
        }

        private int Hit() => 
            Physics.OverlapSphereNonAlloc(StartPoint() + transform.forward, _heroStats.Radius, _hits, _layerMask);

        private Vector3 StartPoint() =>
            new Vector3(transform.position.x, _characterController.center.y / 2, transform.position.z);

        public void LoadProgress(PlayerProgress progress) => 
            _heroStats = progress.HeroStats;
    }
}
