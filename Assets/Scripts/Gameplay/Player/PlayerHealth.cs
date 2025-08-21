using UnityEngine;
using Game.Gameplay.Combat;


namespace Game.Gameplay.Player
{
    public class PlayerHealth : MonoBehaviour, IDamageable
    {
        public int maxHP = 5;
        public int hp = 5;
        public float invulnSeconds = 0.6f; // i-frames sau khi dính đòn
        float _inv;


        void Awake() { hp = Mathf.Clamp(hp, 1, maxHP); }
        void Update() { if (_inv > 0f) _inv -= Time.unscaledDeltaTime; }


        public void ApplyHit(in Hit hit)
        {
            if (_inv > 0f) return;
            int dmg = Mathf.Max(1, Mathf.RoundToInt(hit.damage));
            hp -= dmg;
            _inv = Mathf.Max(0.05f, invulnSeconds);
            if (hp <= 0) { hp = 0; Die(); }
        }


        public void HealPercent(float p) { int add = Mathf.CeilToInt(maxHP * Mathf.Clamp01(p)); hp = Mathf.Clamp(hp + add, 0, maxHP); }


        void Die() { Game.Core.EventBus.PlayerDied(); Time.timeScale = 0f; }
    }
}