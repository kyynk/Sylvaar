using Entities.Player;
using UnityEngine;
using Weapons;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        public GameObject PlayerHandler;
        public enum GameState
        {
            MainMenu,
            InGame,
            Cutscene,
            Paused,
            GameOver
        }

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
            PlayerHandler.GetComponent<PlayerController>().EquipWeapon(currentWeapon.GetComponent<IWeapon>());
        }
    }
}
