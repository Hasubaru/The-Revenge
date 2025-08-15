using UnityEngine;
using Game.Gameplay.Enemies;

namespace Game.Gameplay.Player
{
    public class WeaponDriver : MonoBehaviour
    {
        [Header("Projectile")]
        public GameObject projectilePrefab;
        public float baseDamage = 10f;
        public float cooldown = 0.6f; // giây/viên
        public float projectileSpeed = 16f;
        public float projectileLife = 3f;
        public int pierce = 0;
        public float knockback = 0f;

        float _cd;

        void Update()
        {
            _cd -= Time.deltaTime; if (_cd > 0f) return;
            var target = EnemyRegistry.ClosestTo(transform.position);
            if (target == null) return;
            Vector2 dir = (target.position - transform.position).normalized;

            var pool = Game.Core.SimplePoolService.Instance;
            GameObject go = pool ? pool.Get(projectilePrefab, transform.position, Quaternion.identity)
                                 : Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            var proj = go.GetComponent<Projectile>();
            if (proj) proj.Launch(dir, projectileSpeed, projectileLife, baseDamage, pierce, dir * knockback, projectilePrefab);
            _cd = Mathf.Max(0.05f, cooldown);
        }
    }
}