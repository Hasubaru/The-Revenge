using UnityEngine;

namespace Game.Gameplay.Player
{
    public class PlayerStats : MonoBehaviour
    {
        [Header("Multipliers (x)")]
        public float damageMult = 1f;     // sát thương tổng = base * damageMult
        public float cooldownMult = 1f;   // cooldown thực = base * cooldownMult (nhỏ hơn = bắn nhanh hơn)
        public float areaMult = 1f;       // kích thước hitbox/aura = base * areaMult
        public int pierceBonus = 0;
        public int rerolls = 0;

        public void AddPierce(int n) { pierceBonus += Mathf.Max(0, n); }
        public void AddReroll(int n) { rerolls += Mathf.Max(0, n); }
        public void AddDamagePercent(float p)
        { // p=0.2 => +20%
            damageMult *= (1f + p);
        }
        public void ReduceCooldownPercent(float p)
        { // p=0.1 => -10%
            cooldownMult *= Mathf.Clamp01(1f - p);
            cooldownMult = Mathf.Max(0.1f, cooldownMult);
        }
        public void AddAreaPercent(float p)
        { // p=0.2 => +20%
            areaMult *= (1f + p);
        }
    }
}