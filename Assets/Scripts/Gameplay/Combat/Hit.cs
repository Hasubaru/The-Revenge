using UnityEngine;

namespace Game.Gameplay.Combat
{
    public struct Hit
    {
        public float damage; public Vector2 knock; public bool isCrit; public int source;
        public Hit(float dmg, Vector2 knock, bool crit = false, int src = 0) { damage = dmg; this.knock = knock; isCrit = crit; source = src; }
    }
}