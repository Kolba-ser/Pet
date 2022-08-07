using CodeBase.Infrastructure.AssetManagment;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {

        private readonly IAssets _assets;

        public GameFactory(IAssets assets)
        {
            _assets = assets;
        }

        public GameObject CreateHero(Vector3 at) => _assets.Instantiate(AssetPath.HERO_PATH, at);

        public GameObject CreateHUD() => _assets.Instantiate(AssetPath.HUD_PATH);
    }
}
