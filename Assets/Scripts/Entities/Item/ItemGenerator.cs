using UnityEngine;

namespace Entities.Item
{
    public class ItemGenerator : MonoBehaviour
    {
        [Header("Spawn settings")]
        [SerializeField] private GameObject itemPrefab;
        [SerializeField] private float spawnChance;
        [Header("Raycast setup")]
        [SerializeField] private float distanceBetweenObjects;
        [SerializeField] private float heightCheck;
        [SerializeField] private float rangeCheck;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private Vector2 positivePosition;
        [SerializeField] private Vector2 negativePosition;

        private void Start()
        {
            SpawnItems();
        }

        public void SpawnItems()
        {
            for (float x = negativePosition.x; x < positivePosition.x; x += distanceBetweenObjects)
            {
                for (float z = negativePosition.y; z < positivePosition.y; z += distanceBetweenObjects)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(new Vector3(x, heightCheck, z), Vector3.down, out hit, rangeCheck, layerMask))
                    {
                        if (spawnChance > Random.Range(0f, 101f))
                        {
                            Instantiate(itemPrefab, hit.point, Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)), transform);
                        }
                    }
                }
            }
        }

        public void DestroyItems()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}