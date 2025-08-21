using UnityEngine;
using Game.Gameplay.Combat;

namespace Game.Gameplay.Enemies
{
    public class Enemy : MonoBehaviour, IDamageable
    {
        public float maxHP = 20f; public float moveSpeed = 2f; private float _hp;
        [Header("Drops")] public GameObject xpGemPrefab; 
        public int xpAmount = 1;
        public GameObject coinPrefab; public int coinAmount = 0;

        private void OnEnable() { _hp = maxHP; EnemyRegistry.Register(transform); }
        private void OnDisable() { EnemyRegistry.Unregister(transform); }

        private void Update()
        {
            var player = Game.Gameplay.Player.PlayerSingleton.Instance ? Game.Gameplay.Player.PlayerSingleton.Instance.transform : null;
            if (!player) return; Vector2 dir = (player.position - transform.position).normalized;
            transform.position += (Vector3)(dir * moveSpeed * Time.deltaTime);
        }

        public void ApplyHit(in Hit hit) { TakeDamage(hit.damage); }

        public void TakeDamage(float dmg) { _hp -= dmg; if (_hp <= 0f) Die(); }

        void Die()
        {
            // Drop XP (lệch vị trí)
            if (xpGemPrefab)
            {
                var pool = Game.Core.SimplePoolService.Instance;
                Vector3 posGem = Game.Gameplay.Pickups.DropScatter.Offset(transform.position);
                var go = pool ? pool.Get(xpGemPrefab, posGem, Quaternion.identity)
                              : Instantiate(xpGemPrefab, posGem, Quaternion.identity);
                var gem = go.GetComponent<Game.Gameplay.Pickups.XpGem>();
                if (gem) { gem.amount = Mathf.Max(1, xpAmount); gem.prefabRef = xpGemPrefab; }
            }
            // Drop Coin (lệch vị trí khác)
            if (coinPrefab && coinAmount > 0)
            {
                var pool = Game.Core.SimplePoolService.Instance;
                Vector3 posCoin = Game.Gameplay.Pickups.DropScatter.Offset(transform.position);
                var go = pool ? pool.Get(coinPrefab, posCoin, Quaternion.identity)
                              : Instantiate(coinPrefab, posCoin, Quaternion.identity);
                var coin = go.GetComponent<Game.Gameplay.Pickups.CoinPickup>();
                if (coin) { coin.amount = Mathf.Max(1, coinAmount); coin.prefabRef = coinPrefab; }
            }
            Game.Core.EventBus.EnemyKilled();
            gameObject.SetActive(false);
        }
    }
}