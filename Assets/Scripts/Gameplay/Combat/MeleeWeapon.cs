using UnityEngine;
using Game.Gameplay.Enemies;

namespace Game.Gameplay.Combat
{
    public class MeleeWeapon : MonoBehaviour
    {
        public GameObject slashPrefab;
        public float baseDamage = 20f;
        public float cooldown = 1.0f;
        public float life = 0.12f;
        public float range = 1.4f;
        public float angleDeg = 0f;
        public int maxConcurrent = 1;            // <- tối đa bao nhiêu slash cùng lúc

        Game.Gameplay.Player.PlayerStats _stats;
        float _cd;
        int _activeSlashes = 0;                  // <- đang có bao nhiêu slash sống

        void Awake() { _stats = GetComponent<Game.Gameplay.Player.PlayerStats>(); }

        void Update()
        {
            _cd -= Time.deltaTime;
            if (_cd > 0f || _activeSlashes >= maxConcurrent) return;

            var target = EnemyRegistry.ClosestTo(transform.position);
            if (target == null) return;

            Vector2 dir = (target.position - transform.position).normalized;
            float scale = _stats ? _stats.areaMult : 1f;
            Vector3 spawnPos = transform.position + (Vector3)(dir * range * scale);
            float rot = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + angleDeg;

            var pool = Game.Core.SimplePoolService.Instance;
            var go = pool ? pool.Get(slashPrefab, spawnPos, Quaternion.Euler(0, 0, rot))
                          : Object.Instantiate(slashPrefab, spawnPos, Quaternion.Euler(0, 0, rot));

            var slash = go.GetComponent<MeleeSlash>();
            float dmg = baseDamage * (_stats ? _stats.damageMult : 1f);
            if (slash)
            {
                _activeSlashes++;
                slash.OnRecycled = () => { _activeSlashes = Mathf.Max(0, _activeSlashes - 1); };
                slash.Activate(dmg, life, slashPrefab, scale);
            }

            float cd = cooldown * (_stats ? _stats.cooldownMult : 1f);
            _cd = Mathf.Max(0.05f, cd);
        }
    }
}
    