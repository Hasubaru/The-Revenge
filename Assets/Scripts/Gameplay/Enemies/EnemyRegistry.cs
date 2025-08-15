using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Enemies
{
    public static class EnemyRegistry
    {
        static readonly List<Transform> _all = new();
        public static void Register(Transform t) { if (!_all.Contains(t)) _all.Add(t); }
        public static void Unregister(Transform t) { _all.Remove(t); }
        public static Transform ClosestTo(Vector3 p)
        {
            Transform best = null; float d = float.MaxValue;
            for (int i = 0; i < _all.Count; i++)
            {
                var t = _all[i]; if (!t) continue; float dd = (t.position - p).sqrMagnitude; if (dd < d) { d = dd; best = t; }
            }
            return best;
        }
    }
}