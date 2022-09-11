﻿using CodeBase.Infrastructure.Services;
using System.Threading.Tasks;

namespace CodeBase.UI.Services
{
    public interface IUIFactory : IService
    {
        public void CreateShop();

        Task CreateUIRootAsync();
    }
}