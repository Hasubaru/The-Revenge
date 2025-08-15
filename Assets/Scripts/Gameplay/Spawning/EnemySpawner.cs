using UnityEngine;

namespace Game.Gameplay.Spawning
{
    public class EnemySpawner : MonoBehaviour
    {
        public GameObject enemyPrefab;
        public float radius = 10f;
        public float enemiesPerSecond = 1.0f;
        private float _acc;

        private void Update()
        {
            _acc += Time.deltaTime * Mathf.Max(0.1f, enemiesPerSecond);
            while (_acc >= 1f) { _acc -= 1f; Spawn(); }
        }

        void Spawn()
        {
            var player = Game.Gameplay.Player.PlayerSingleton.Instance ? Game.Gameplay.Player.PlayerSingleton.Instance.transform : null;
            if (!player) return;
            Vector2 dir = Random.insideUnitCircle.normalized;
            Vector3 pos = player.position + (Vector3)(dir * radius);
            Game.Core.SimplePoolService.Instance.Get(enemyPrefab, pos, Quaternion.identity);
        }
    }
}