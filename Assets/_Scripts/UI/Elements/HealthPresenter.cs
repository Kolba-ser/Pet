using Pet.Enemy;
using UnityEngine;

namespace Pet.UI
{
    public class HealthPresenter : MonoBehaviour
    {
        [SerializeField] private HpBar hpBar;

        private IDamagable _health;

        private void OnDestroy()
        {
            if (_health != null)
                _health.OnHealthChange -= UpdateHpBar;
        }

        public void Construct(IDamagable health)
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