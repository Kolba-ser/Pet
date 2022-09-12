using Pet.Infrastructure.SaveLoad;
using UnityEngine;

namespace Pet.Logic
{
    [RequireComponent(typeof(BoxCollider))]
    public class SaveTrigger : MonoBehaviour
    {
        [SerializeField] private BoxCollider boxCollider;

        private ISaveLoadService _saveLoadService;

        private void Awake()
        {
            boxCollider = boxCollider ?? GetComponent<BoxCollider>();
            _saveLoadService = Infrastructure.Services.Services.Container.Single<ISaveLoadService>();
        }

        private void OnTriggerEnter(Collider other)
        {
            _saveLoadService.SaveProgress();

            "Progress saved".Log();
            gameObject.SetActive(false);
        }

        private void OnDrawGizmos()
        {
            if (!boxCollider)
                return;
            Color color = Color.green;
            color.a = 90;
            Gizmos.color = color;
            Gizmos.DrawCube(transform.position + boxCollider.center, boxCollider.size);
        }
    }
}