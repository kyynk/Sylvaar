using UnityEngine;

namespace Interactable
{
    public class ItemInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] private MeshRenderer sphereMeshRenderer;
        [SerializeField] private Material whiteMaterial;
        [SerializeField] private Material grayMaterial;
        private bool isColorWhite = true;

        private void SetColorWhite()
        {
            sphereMeshRenderer.material = whiteMaterial;
        }

        private void SetColorGray()
        {
            sphereMeshRenderer.material = grayMaterial;
        }

        private void ToggleColor()
        {
            isColorWhite = !isColorWhite;
            if (isColorWhite)
            {
                SetColorGray();
            }
            else
            {
                SetColorWhite();
            }
        }

        public void Interact()
        {
            ToggleColor();
        }

        public string GetInteractText()
        {
            return "Change color";
        }

        public Transform GetTransform()
        {
            return transform;
        }
    }
}