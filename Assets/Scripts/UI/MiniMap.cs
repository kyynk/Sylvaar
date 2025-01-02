using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class MiniMap : MonoBehaviour
    {
        public RectTransform mapRect;
        public Transform playerTransform;
        public RectTransform playerIcon;

        public Transform[] npcTransforms;
        public RectTransform[] npcIcons;

        private Terrain terrain;
        private Vector3 terrainSize;

        void Start()
        {
            terrain = FindObjectOfType<Terrain>();

            if (terrain != null)
            {
                terrainSize = terrain.terrainData.size;
            }
            else
            {
                Debug.LogError("Terrain not found! Make sure your scene has a Terrain component.");
            }
        }

        void Update()
        {
            if (terrain != null)
            {
                UpdateIconPosition(playerTransform, playerIcon);

                for (int i = 0; i < npcTransforms.Length; i++)
                {
                    UpdateIconPosition(npcTransforms[i], npcIcons[i]);
                }
            }
        }

        private void UpdateIconPosition(Transform worldObject, RectTransform icon)
        {
            Vector3 worldPosition = worldObject.position;
            float normalizedX = (worldPosition.x - terrain.transform.position.x) / terrainSize.x;
            float normalizedZ = (worldPosition.z - terrain.transform.position.z) / terrainSize.z;
            float mapWidth = mapRect.rect.width;
            float mapHeight = mapRect.rect.height;

            float iconX = normalizedX * mapWidth;
            float iconY = normalizedZ * mapHeight;
            Vector2 mapCenterOffset = new Vector2(mapRect.rect.width * mapRect.pivot.x, mapRect.rect.height * mapRect.pivot.y);

            icon.anchoredPosition = new Vector2(iconX - mapCenterOffset.x, iconY - mapCenterOffset.y + 12f);

            float iconScale = Mathf.Min(mapWidth / terrainSize.x, mapHeight / terrainSize.z);
            icon.localScale = new Vector3(iconScale, iconScale, 1);
        }
    }

}
