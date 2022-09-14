using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Pet.AssetManagment
{
    public class AssetProvider : IAssetsProvider
    {
        private Dictionary<string, AsyncOperationHandle> _completedCaches;
        private Dictionary<string, List<AsyncOperationHandle>> _handles;

        public AssetProvider()
        {
            _completedCaches = new Dictionary<string, AsyncOperationHandle>();
            _handles = new Dictionary<string, List<AsyncOperationHandle>>();
        }

        public Task<GameObject> Instantiate(string path, Vector3 at) =>
            Addressables.InstantiateAsync(path, at, Quaternion.identity).Task;

        public Task<GameObject> Instantiate(string path) =>
           Addressables.InstantiateAsync(path).Task;

        public void Initialize() =>
            Addressables.InitializeAsync();

        public async Task<T> Load<T>(AssetReference assetRef) where T : class
        {
            if (_completedCaches.TryGetValue(assetRef.AssetGUID, out AsyncOperationHandle completedHandle))
                return completedHandle.Result as T;

            return await RunWithCacheOnComplete(
                         Addressables.LoadAssetAsync<T>(assetRef),
                         cachedKey: assetRef.AssetGUID);
        }

        public async Task<T> Load<T>(string address) where T : class
        {
            if (_completedCaches.TryGetValue(address, out AsyncOperationHandle completedHandle))
                return completedHandle.Result as T;

            return await RunWithCacheOnComplete(
                         Addressables.LoadAssetAsync<T>(address),
                         cachedKey: address);
        }

        private async Task<T> RunWithCacheOnComplete<T>(AsyncOperationHandle<T> handle, string cachedKey) where T : class
        {
            if (!_handles.ContainsKey(cachedKey))
                handle.Completed += operation =>
                {
                    _completedCaches.Add(cachedKey, operation);
                };

            AddHandle(cachedKey, handle);

            return await handle.Task;
        }

        public void CleanUp()
        {
            foreach (List<AsyncOperationHandle> resourceHandles in _handles.Values)
                foreach (AsyncOperationHandle handle in resourceHandles)
                    Addressables.Release(handle);

            _completedCaches.Clear();
            _handles.Clear();
        }

        private void AddHandle<T>(string key, AsyncOperationHandle<T> handle) where T : class
        {
            if (!_handles.TryGetValue(key, out List<AsyncOperationHandle> resourceHandles))
            {
                resourceHandles = new List<AsyncOperationHandle>();
                _handles[key] = resourceHandles;
            }

            resourceHandles.Add(handle);
        }
    }
}