using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.UI.Services
{
    public interface IUIFactory : IService
    {
        public void CreateShop();
        Transform CreateUIRoot();
    }
}