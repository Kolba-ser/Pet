using UnityEngine;

namespace CodeBase.Infrastructure.AssetManagment
{
    public class AssetProvider : IAssets
    {
        public GameObject Instantiate(string path, Vector3 at) =>
            Object.Instantiate(Resources.Load<GameObject>(path), at, Quaternion.identity);
        public GameObject Instantiate(string path) =>
           Object.Instantiate(Resources.Load<GameObject>(path));
    }
}
