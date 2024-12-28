using Entities.Item;
using UnityEngine;

namespace Tests
{
    public class TestGenerator : MonoBehaviour
    {
        [SerializeField] private ItemGenerator itemGenerator;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                itemGenerator.DestroyItems();
                itemGenerator.SpawnItems();
            }
        }
    }
}