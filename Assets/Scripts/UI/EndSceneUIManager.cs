using System.Collections;
using Core;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace UI
{
    public class EndSceneUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject goodEndUI;
        [SerializeField] private GameObject badEndUI;
        [SerializeField] private VideoPlayer videoPlayer;
        [SerializeField] private RawImage videoDisplay;

        void Start()
        {
            
            videoPlayer.targetCamera = UnityEngine.Camera.main;

           
            if (videoPlayer == null)
            {
                Debug.LogError("No VideoPlayer component found on this GameObject.");
                return;
            }

            
            videoPlayer.loopPointReached += OnVideoEnd;

            
            bool isGoodEnd = GameManager.Instance.isGoodEnd;
            Play(isGoodEnd);

            
            goodEndUI.SetActive(false);
            badEndUI.SetActive(false);
        }

        public void Play(bool isGoodEnd)
        {
            string videoPath = isGoodEnd ? "Video/GoodEnding_v2" : "Video/BadEnding_v2";

            VideoClip videoClip = Resources.Load<VideoClip>(videoPath);

            if (videoClip != null)
            {
                Debug.Log($"Playing video clip: {videoClip.name}");
                videoPlayer.clip = videoClip;
                videoPlayer.Play();
            }
            else
            {
                Debug.LogError($"Video clip not found at Resources/{videoPath}");
            }
        }

        private void OnVideoEnd(VideoPlayer vp)
        {
            videoDisplay.gameObject.SetActive(false);
            if (GameManager.Instance.isGoodEnd)
            {
                goodEndUI.SetActive(true);
                badEndUI.SetActive(false);
            }
            else
            {
                goodEndUI.SetActive(false);
                badEndUI.SetActive(true);
            }

            Debug.Log("Video has ended. Displaying the ending UI.");
        }

        private void OnDestroy()
        {
            if (videoPlayer != null)
            {
                videoPlayer.loopPointReached -= OnVideoEnd;
            }
        }
    }
}
