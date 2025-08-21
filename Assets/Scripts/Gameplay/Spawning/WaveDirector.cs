using UnityEngine;
using Game.Core;

namespace Game.Gameplay.Spawning
{
    public class WaveDirector : MonoBehaviour
    {
        public EnemySpawner spawner;

        void OnEnable() { EventBus.OnMinuteChanged += OnMinute; }
        void OnDisable() { EventBus.OnMinuteChanged -= OnMinute; }

        void OnMinute(float minute)
        {
            if (!spawner) return;
            if (minute < 2f) spawner.enemiesPerSecond = 1.0f;
            else if (minute < 5f) spawner.enemiesPerSecond = 2.0f;
            else if (minute < 8f) spawner.enemiesPerSecond = 3.0f;
            else spawner.enemiesPerSecond = 4.0f;
        }
    }
}