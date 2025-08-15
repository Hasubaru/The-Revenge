using UnityEngine;
using Game.Core;

namespace Game.Gameplay.Player
{
    public class PlayerExperience : MonoBehaviour
    {
        public int level = 1; public int exp = 0; public int next = 5;
        public void Gain(int x)
        {
            exp += x;
            while (exp >= next)
            {
                exp -= next; level++; next = Mathf.RoundToInt(next * 1.5f + 2);
                EventBus.LevelUp(level);
                Debug.Log($"Level Up! -> Lv.{level}");
            }
        }
    }
}