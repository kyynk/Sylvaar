using UnityEngine;

namespace Entities.Item
{
    public class ItemGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject itemPrefab;

        private void Update()
        {
            if (Input.GetKeyDown("p"))
            {
                Vector3 randomPosition = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
                print(randomPosition);
                Instantiate(itemPrefab, randomPosition, Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)));
            }
        }
    }
}