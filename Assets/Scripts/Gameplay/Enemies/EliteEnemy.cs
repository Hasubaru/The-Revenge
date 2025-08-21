using UnityEngine;
using Game.Gameplay.Combat;

namespace Game.Gameplay.Enemies
{
    public class EliteEnemy : MonoBehaviour, IDamageable
    {
        public float maxHP = 200f;
        [SerializeField] float _hp = -1f;
        public float moveSpeed = 1.7f;
        [Header("Drops")] public GameObject chestPrefab; public GameObject xpGemPrefab; public int xpAmount = 3;

        void OnEnable() { _hp = maxHP; EnemyRegistry.Register(transform); }
        void OnDisable() { EnemyRegistry.Unregister(transform); }

        void Update()
        {
            var p = Game.Gameplay.Player.PlayerSingleton.Instance ? Game.Gameplay.Player.PlayerSingleton.Instance.transform : null;
            if (!p) return; Vector2 dir = (p.position - transform.position).normalized;
            transform.position += (Vector3)(dir * moveSpeed * Time.deltaTime);
        }

        public void ApplyHit(in Hit hit) { _hp -= hit.damage; if (_hp <= 0f) Die(); }

        void Die()
        {
            // Drop Chest
            if (chestPrefab)
            {
                var pool = Game.Core.SimplePoolService.Instance;
                var go = pool ? pool.Get(chestPrefab, transform.position, Quaternion.identity)
                              : Object.Instantiate(chestPrefab, transform.position, Quaternion.identity);
                var chest = go.GetComponent<Game.Gameplay.Pickups.ChestPickup>(); if (chest) chest.prefabRef = chestPrefab;
            }
            // Drop ít XP
            if (xpGemPrefab)
            {
                var pool = Game.Core.SimplePoolService.Instance;
                var go = pool ? pool.Get(xpGemPrefab, transform.position, Quaternion.identity)
                              : Object.Instantiate(xpGemPrefab, transform.position, Quaternion.identity);
                var gem = go.GetComponent<Game.Gameplay.Pickups.XpGem>(); if (gem) { gem.amount = Mathf.Max(1, xpAmount); gem.prefabRef = xpGemPrefab; }
            }
            gameObject.SetActive(false);
        }
    }
}