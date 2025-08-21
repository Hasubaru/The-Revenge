using UnityEngine;
using Game.Gameplay.Enemies;

namespace Game.Gameplay.Player
{
    public class WeaponDriver : MonoBehaviour
    {
        [Header("Projectile")]
        public GameObject projectilePrefab;
        public float baseDamage = 10f;
        public float cooldown = 0.6f; // giây/viên (base)
        public float projectileSpeed = 16f;
        public float projectileLife = 3f;
        public int pierce = 0;           // pierce cơ bản (không tính buff)
        public float knockback = 0f;

        private PlayerStats _stats;
        private float _cd;

        void Awake() { _stats = GetComponent<PlayerStats>(); }

        void Update()
        {
            _cd -= Time.deltaTime; if (_cd > 0f) return;
            var target = EnemyRegistry.ClosestTo(transform.position);
            if (target == null) return;

            Vector2 dir = (target.position - transform.position).normalized;

            // Tính sát thương & thuộc tính đạn sau khi áp buff từ PlayerStats
            float dmg = baseDamage * (_stats ? _stats.damageMult : 1f);
            float life = projectileLife;
            int totalPierce = pierce + (_stats ? _stats.pierceBonus : 0);

            var pool = Game.Core.SimplePoolService.Instance;
            GameObject go = pool ? pool.Get(projectilePrefab, transform.position, Quaternion.identity)
                                 : Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            var proj = go.GetComponent<Projectile>();
            if (proj) proj.Launch(dir, projectileSpeed, life, dmg, totalPierce, dir * knockback, projectilePrefab);

            float cd = cooldown * (_stats ? _stats.cooldownMult : 1f);
            _cd = Mathf.Max(0.05f, cd);
        }
    }
}