using UnityEngine;

namespace Pet.Enemy
{
    [RequireComponent(typeof(EnemyAttack))]
    public class AttackRangeTrigger : MonoBehaviour
    {
        [SerializeField] private EnemyAttack _attack;
        [SerializeField] private TriggerObserver _triggerObserver;

        private void Start()
        {
            _attack.DisableAttack();
            _triggerObserver.TriggerEnter += TriggerEnter;
            _triggerObserver.TriggerExit += TriggerExit;
        }

        private void TriggerEnter(Collider obj) =>
            _attack.EnableAttack();

        private void TriggerExit(Collider obj) =>
            _attack.DisableAttack();
    }
}