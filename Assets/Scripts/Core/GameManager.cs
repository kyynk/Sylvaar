using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // 單例模式實例

    [SerializeField] private WeaponFactory weaponFactory; // 武器工廠

    private GameObject currentWeapon; // 當前裝備的武器

    private void Awake()
    {
        // 設置單例模式
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // 防止跨場景銷毀
    }

    private void Start()
    {
    }


    // 全局輸入處理（切換武器測試）
    private void Update()
    {
    }

    public void EquipWeapon(string weaponName)
    {
        weaponFactory.DestroyCurrentWeapon();
        currentWeapon = weaponFactory.CreateWeapon(weaponName);
    }

}
