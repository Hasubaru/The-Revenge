using UnityEngine;
using Game.Gameplay.Combat;

namespace Game.Gameplay.Enemies
{
    public class Boss : MonoBehaviour, IDamageable
    {
        public float maxHP = 800f; float _hp;
        public float moveSpeed = 1.1f;
        [Header("Drops")] public GameObject xpGemPrefab; public int xpAmount = 5;
        public GameObject coinPrefab; public int coinAmount = 10;

        void OnEnable() { _hp = maxHP; }

        void Update()
        {
            var player = Game.Gameplay.Player.PlayerSingleton.Instance ? Game.Gameplay.Player.PlayerSingleton.Instance.transform : null;
            if (!player) return;
            Vector2 dir = (player.position - transform.position).normalized;
            transform.position += (Vector3)(dir * moveSpeed * Time.deltaTime);
        }

        public void ApplyHit(in Hit hit) { _hp -= hit.damage; if (_hp <= 0f) Die(); }
        public float HealthPct => Mathf.Clamp01(_hp / Mathf.Max(1f, maxHP));

        void Die()
        {
            // Drop XP (lệch)
            if (xpGemPrefab)
            {
                var pool = Game.Core.SimplePoolService.Instance;
                Vector3 posGem = Game.Gameplay.Pickups.DropScatter.Offset(transform.position, 0.8f, 1.6f);
                var go = pool ? pool.Get(xpGemPrefab, posGem, Quaternion.identity)
                              : Object.Instantiate(xpGemPrefab, posGem, Quaternion.identity);
                var gem = go.GetComponent<Game.Gameplay.Pickups.XpGem>();
                if (gem) { gem.amount = Mathf.Max(1, xpAmount); gem.prefabRef = xpGemPrefab; }
            }
            // Drop Coin (lệch khác)
            if (coinPrefab && coinAmount > 0)
            {
                var pool = Game.Core.SimplePoolService.Instance;
                Vector3 posCoin = Game.Gameplay.Pickups.DropScatter.Offset(transform.position, 0.8f, 1.6f);
                var go = pool ? pool.Get(coinPrefab, posCoin, Quaternion.identity)
                              : Object.Instantiate(coinPrefab, posCoin, Quaternion.identity);
                var coin = go.GetComponent<Game.Gameplay.Pickups.CoinPickup>();
                if (coin) { coin.amount = Mathf.Max(1, coinAmount); coin.prefabRef = coinPrefab; }
            }
            Game.Core.EventBus.BossDefeated();
            gameObject.SetActive(false);
        }
    }
}