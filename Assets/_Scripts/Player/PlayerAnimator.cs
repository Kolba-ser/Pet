using Pet.Logic;
using System;
using UnityEngine;

namespace Pet.Player
{
    public class PlayerAnimator : MonoBehaviour, IAnimationStateReader
    {
        [SerializeField] private Animator Animator;
        [SerializeField] private CharacterController CharacterController;

        private readonly int _moveHash = Animator.StringToHash("Walking");
        private readonly int _attackHash = Animator.StringToHash("AttackNormal");
        private readonly int _hitHash = Animator.StringToHash("Hit");

        private readonly int _idleStateHash = Animator.StringToHash("Idle");
        private readonly int _walkingStateHash = Animator.StringToHash("Run");
        private readonly int _deathStateHash = Animator.StringToHash("Die");
        private readonly int _deadFlagHash = Animator.StringToHash("IsDead");

        public event Action<AnimatorState> StateEntered;

        public event Action<AnimatorState> StateExited;

        public AnimatorState State
        {
            get; private set;
        }

        private void Update()
        {
            Animator.SetFloat(_moveHash, CharacterController.velocity.magnitude, 0.1f, Time.deltaTime);
        }

        public bool IsAttacking => State == AnimatorState.Attack;

        public void PlayHit() => Animator.SetTrigger(_hitHash);

        public void PlayAttack() => Animator.SetTrigger(_attackHash);

        public void PlayDeath()
        {
            Animator.SetBool(_deadFlagHash, true);
            Animator.SetTrigger(_deathStateHash);
        }

        public void ResetToIdle() => Animator.Play(_idleStateHash, -1);

        public void EnteredState(int stateHash)
        {
            State = StateFor(stateHash);
            StateEntered?.Invoke(State);
        }

        public void ExitedState(int stateHash) =>
          StateExited?.Invoke(StateFor(stateHash));

        private AnimatorState StateFor(int stateHash)
        {
            AnimatorState state;
            if (stateHash == _idleStateHash)
                state = AnimatorState.Idle;
            else if (stateHash == _attackHash)
                state = AnimatorState.Attack;
            else if (stateHash == _walkingStateHash)
                state = AnimatorState.Walking;
            else if (stateHash == _deathStateHash)
                state = AnimatorState.Died;
            else
                state = AnimatorState.Unknown;

            return state;
        }
    }
}