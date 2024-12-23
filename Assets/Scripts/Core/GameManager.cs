using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } 

    [SerializeField] private WeaponFactory weaponFactory; 

    private GameObject currentWeapon; 

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
    }


    private void Update()
    {
    }

    public void EquipWeapon(string weaponName)
    {
        weaponFactory.DestroyCurrentWeapon();
        currentWeapon = weaponFactory.CreateWeapon(weaponName);
    }

}
