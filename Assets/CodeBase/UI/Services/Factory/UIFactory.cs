using CodeBase.Infrastructure;
using CodeBase.Infrastructure.AssetManagment;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.StaticData;
using System.Threading.Tasks;
using UnityEngine;

namespace CodeBase.UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        private IAssets _assetProvider;
        private IStaticDataService _staticDataService;
        private readonly IPersistentProgressService _persistentProgress;
        private Transform _rootCanvas;

        public UIFactory(IAssets assetProvider, IStaticDataService staticDataService, IPersistentProgressService persistentProgress)
        {
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
            _persistentProgress = persistentProgress;
        }

        public void CreateShop()
        {
            var config = _staticDataService.ForWindow(WindowId.Shop);
            WindowBase window = Object.Instantiate(config.Prefab, _rootCanvas);
            window.Construct(_persistentProgress);
        }

        public async Task CreateUIRootAsync()
        {

            GameObject root = await _assetProvider.Instantiate(AssetAddress.ROOT_CANVAS_PATH);
            _rootCanvas = root.transform;
        }
    }
}