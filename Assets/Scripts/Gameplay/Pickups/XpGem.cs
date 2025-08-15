using UnityEngine;

namespace Game.Gameplay.Pickups
{
    [RequireComponent(typeof(Collider2D))]
    public class XpGem : MonoBehaviour
    {
        public int amount = 1; public GameObject prefabRef; // gán chính prefab khi spawn bằng Pool
        void Awake() { var col = GetComponent<Collider2D>(); col.isTrigger = true; }
        void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            var xp = other.GetComponent<Game.Gameplay.Player.PlayerExperience>();
            if (xp != null) xp.Gain(amount);
            var pool = Game.Core.SimplePoolService.Instance;
            if (pool != null && prefabRef != null) pool.Recycle(prefabRef, gameObject); else Destroy(gameObject);
        }
    }
}