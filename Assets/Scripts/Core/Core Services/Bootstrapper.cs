using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Core
{
    public class Bootstrapper : MonoBehaviour
    {
        [Header("Prefabs (optional)")]
        public SimplePoolService poolPrefab;
        public TimerService timerPrefab;

        [Header("Next Scene")]
        public string firstScene = "Sandbox";

        private void Start()
        {
            if (SimplePoolService.Instance == null && poolPrefab != null) Instantiate(poolPrefab);
            if (TimerService.Instance == null && timerPrefab != null) Instantiate(timerPrefab);
            SceneManager.LoadScene(firstScene);
        }
    }
}