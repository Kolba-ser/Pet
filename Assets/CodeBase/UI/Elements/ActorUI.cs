using CodeBase.Enemy;
using CodeBase.Hero;
using UnityEngine;

namespace Assets.CodeBase.UI
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private HpBar hpBar;

        private IHealth _health;

        private void OnDestroy()
        {
            if(_health != null)
                _health.OnHealthChange -= UpdateHpBar;
        }

        public void Construct(IHealth health)
        {
            this._health = health;
            _health.OnHealthChange += UpdateHpBar; 
        }


        public void UpdateHpBar()
        {
            hpBar.SetValue(_health.Current, _health.Max);
        }
    }
}
