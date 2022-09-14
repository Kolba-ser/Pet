using Pet.Infrastructure;
using Pet.AssetManagment;
using Pet.Services.Progress;
using Pet.StaticData;
using System.Threading.Tasks;
using UnityEngine;

namespace Pet.UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        private IAssetsProvider _assetProvider;
        private ISettingsDataRegistry _staticDataService;
        private readonly IProgressHolderService _persistentProgress;
        private Transform _rootCanvas;

        public UIFactory(IAssetsProvider assetProvider, ISettingsDataRegistry staticDataService, IProgressHolderService persistentProgress)
        {
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
            _persistentProgress = persistentProgress;
        }

        public void CreateShop()
        {
            var config = _staticDataService.ForWindow(WindowType.Shop);
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