using UnityEngine;

namespace Game.Core
{
    public class TimerService : MonoBehaviour
    {
        public static TimerService Instance { get; private set; }
        public float Elapsed { get; private set; }
        private float _lastWholeMinute = -1f;

        private void Awake()
        {
            if (Instance != null && Instance != this) { Destroy(gameObject); return; }
            Instance = this; DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            Elapsed += Time.deltaTime;
            float minute = Mathf.Floor(Elapsed / 60f);
            if (minute != _lastWholeMinute)
            {
                _lastWholeMinute = minute;
                EventBus.MinuteChanged(minute);
            }
        }

        public void ResetTimer() { Elapsed = 0f; _lastWholeMinute = -1f; }
    }
}