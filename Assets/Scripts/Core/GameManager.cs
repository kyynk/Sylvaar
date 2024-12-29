using Entities.Player;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        [SerializeField] private GameEndVideoPlayer gameEndVideoPlayer;

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

        private void LoadSceneBasedOnState(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.MainMenu:
                    SceneManager.LoadScene("GameStartScene");
                    break;

                case GameState.InGame:
                    SceneManager.LoadScene("InGameScene");
                    break;

                case GameState.GoodEnd:
                    SceneManager.LoadScene("GameEndScene");
                    //Play the video of the good end, the path us resources/GoodEnd.mp4
                    //gameEndVideoPlayer.Play(true);
                    break;
                case GameState.BadEnd:
                    SceneManager.LoadScene("GameEndScene");
                    //gameEndVideoPlayer.Play(false);
                    break;
                default:
                    Debug.LogError("Unknown GameState: " + gameState);
                    break;
            }
        }
    }
}
