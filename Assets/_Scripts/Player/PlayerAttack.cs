﻿using Pet.Data;
using Pet.Enemy;
using Pet.Infrastructure.Services.Progress;
using Pet.Services.Input;
using UnityEngine;

namespace Pet.Player
{
    public class PlayerAttack : MonoBehaviour, ISavedProgressReader
    {
        [SerializeField] private PlayerAnimator _heroAnimator;
        [SerializeField] private CharacterController _characterController;

        private IInputService _inputService;
        private LayerMask _layerMask;
        private Collider[] _hits = new Collider[3];
        private Stats _heroStats;

        private void Awake()
        {
            _inputService = Infrastructure.Services.Services.Container.Single<IInputService>();
            _layerMask = 1 << LayerMask.NameToLayer("Hittable");
        }

        private void Update()
        {
            if (_inputService.IsAttackButtonUp() && !_heroAnimator.IsAttacking)
                _heroAnimator.PlayAttack();
        }

        public void OnAttack()
        {
            for (int i = 0; i < Hit(); i++)
            {
                _hits[i].transform.GetComponentInParent<IDamagable>().TakeDamage(_heroStats.Damage);
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