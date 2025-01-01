using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private GameObject healthBarImage;
    public void UpdateHealthBar(float currentValue, float maxVale)
    {
        healthBarImage.transform.localScale = new Vector3(currentValue/maxVale ,1 ,1);
    }
    void Update()
    {

    }
}
