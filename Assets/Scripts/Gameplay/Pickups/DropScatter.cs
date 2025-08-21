using UnityEngine;

namespace Game.Gameplay.Pickups
{
    public static class DropScatter
    {
        // Trả về vị trí lệch ngẫu nhiên quanh tâm, với khoảng cách [minDist, maxDist]
        public static Vector3 Offset(Vector3 center, float minDist = 0.6f, float maxDist = 1.2f)
        {
            Vector2 dir = Random.insideUnitCircle.normalized;
            float dist = Random.Range(minDist, maxDist);
            return center + (Vector3)(dir * dist);
        }
    }
}