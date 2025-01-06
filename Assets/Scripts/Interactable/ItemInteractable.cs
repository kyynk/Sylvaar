using System.IO;
using UnityEngine;

namespace Interactable
{
    public class ItemInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] private string itemName;
        [SerializeField] private string interactText;
        [SerializeField] private int itemQuantity;

        private void GainItem()
        {
            try
            {
                var path = Path.Combine(Application.streamingAssetsPath, "PlayerItems/items.csv");
                var lines = File.ReadAllLines(path);
                var headers = lines[0].Split(',');
                var itemsQuantity = lines[1].Split(',');
                for (int i = 0; i < headers.Length; i++)
                {
                    if (headers[i] == itemName)
                    {
                        itemsQuantity[i] = (int.Parse(itemsQuantity[i]) + itemQuantity).ToString();
                        break;
                    }
                }

                lines[1] = string.Join(",", itemsQuantity);
                File.WriteAllLines(path, lines);
            }
            catch (IOException e)
            {
                Debug.LogError(e.Message);
            }
        }

        public void Interact()
        {
            GainItem();
            gameObject.SetActive(false);
        }

        public string GetInteractText()
        {
            return interactText;
        }

        public Transform GetTransform()
        {
            return transform;
        }
    }
}