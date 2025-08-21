using UnityEngine;
using Game.Core;

namespace Game.Gameplay.Spawning
{
    public class BossDirector : MonoBehaviour
    {
        public GameObject bossPrefab;
        public float bossMinute = 10f; // phút xuất hiện
        bool _spawned;

        void OnEnable() { EventBus.OnMinuteChanged += OnMinute; }
        void OnDisable() { EventBus.OnMinuteChanged -= OnMinute; }

        void OnMinute(float minute) { if (!_spawned && minute >= bossMinute) Spawn(); }

        void Spawn()
        {
            var player = Game.Gameplay.Player.PlayerSingleton.Instance ? Game.Gameplay.Player.PlayerSingleton.Instance.transform : null;
            if (!player || bossPrefab == null) return;
            Vector2 dir = Random.insideUnitCircle.normalized;
            Vector3 pos = player.position + (Vector3)(dir * 12f);
            var pool = Game.Core.SimplePoolService.Instance;
            var go = pool ? pool.Get(bossPrefab, pos, Quaternion.identity)
                          : Object.Instantiate(bossPrefab, pos, Quaternion.identity);
            _spawned = true;
            // Nối Boss HP Bar nếu có
            /*var bar = Object.FindObjectOfType<BossHpBar>();
            if (bar) bar.Bind(go.GetComponent<Game.Gameplay.Enemies.Boss>());*/
        }
    }
}