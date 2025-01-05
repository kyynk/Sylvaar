using Entities.Player;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
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
            GoodEnd,
            BadEnd
        }

        public static GameManager Instance { get; private set; } 

        [SerializeField] private WeaponFactory weaponFactory;
        private GameObject currentWeapon;
        public bool isGoodEnd = false;

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
            LoadSceneBasedOnState(GameState.MainMenu);
        }

        private void Update()
        {
        }

        public void EquipWeapon(string weaponName)
        {
            PlayerController playerController = PlayerHandler.GetComponent<PlayerController>();
            currentWeapon = weaponFactory.CreateWeapon(weaponName);
            PlayerHandler.GetComponent<PlayerController>().EquipWeapon(currentWeapon);
        }

        public void GameStateChange(GameState gameState)
        {
            LoadSceneBasedOnState(gameState);
        }

        public void LoadPlayerHandler(GameObject playerHandler)
        {
            PlayerHandler = playerHandler;
        }

        private void LoadSceneBasedOnState(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.MainMenu:
                    SceneManager.LoadScene("GameStartScene");
                    break;

                case GameState.InGame:
                    SceneManager.LoadScene("InGameScene");
                    //Debug.Log("InGameScene loaded");
                    break;

                case GameState.GoodEnd:
                    isGoodEnd = true;
                    SceneManager.LoadScene("GameEndScene");
                    break;
                case GameState.BadEnd:
                    isGoodEnd = false;
                    SceneManager.LoadScene("GameEndScene");
                    break;
                default:
                    //Debug.LogError("Unknown GameState: " + gameState);
                    break;
            }
        }
    }
}
