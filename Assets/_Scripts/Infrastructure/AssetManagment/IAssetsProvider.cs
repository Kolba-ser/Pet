using Pet.Infrastructure.Services;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Pet.Infrastructure.AssetManagment
{
    public interface IAssetsProvider : IService
    {
        public void CleanUp();

        public void Initialize();

        public Task<GameObject> Instantiate(string path);

        public Task<GameObject> Instantiate(string path, Vector3 at);

        public Task<T> Load<T>(AssetReference assetRef) where T : class;

        public Task<T> Load<T>(string address) where T : class;
    }
}