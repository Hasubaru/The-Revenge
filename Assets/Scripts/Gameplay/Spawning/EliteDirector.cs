using UnityEngine;

namespace Game.Gameplay.Spawning
{
    public class EliteDirector : MonoBehaviour
    {
        public GameObject elitePrefab;
        public float radius = 12f;          // spawn cách player bao xa
        public int maxAlive = 2;            // tối đa Elite đang sống
        public int[] spawnAtMinutes = new int[] { 3, 6, 9 }; // mốc phút sẽ spawn

        private readonly System.Collections.Generic.List<GameObject> _alive = new();
        private readonly System.Collections.Generic.HashSet<int> _spawnedAt = new();

        void OnEnable() { Game.Core.EventBus.OnMinuteChanged += OnMinute; }
        void OnDisable() { Game.Core.EventBus.OnMinuteChanged -= OnMinute; }

        void Update()
        {
            // dọn danh sách _alive
            for (int i = _alive.Count - 1; i >= 0; i--)
            {
                var go = _alive[i];
                if (go == null || !go.activeInHierarchy) _alive.RemoveAt(i);
            }
        }

        void OnMinute(float minute)
        {
            if (elitePrefab == null) return;
            int m = Mathf.FloorToInt(minute);
            if (_spawnedAt.Contains(m)) return;
            if (System.Array.IndexOf(spawnAtMinutes, m) >= 0)
            {
                _spawnedAt.Add(m);
                if (_alive.Count < maxAlive) Spawn();
            }
        }

        void Spawn()
        {
            var player = Game.Gameplay.Player.PlayerSingleton.Instance ? Game.Gameplay.Player.PlayerSingleton.Instance.transform : null;
            if (!player) return;
            Vector2 dir = Random.insideUnitCircle.normalized;
            Vector3 pos = player.position + (Vector3)(dir * radius);
            var pool = Game.Core.SimplePoolService.Instance;
            var go = pool ? pool.Get(elitePrefab, pos, Quaternion.identity)
                          : Object.Instantiate(elitePrefab, pos, Quaternion.identity);
            _alive.Add(go);
        }
    }
}