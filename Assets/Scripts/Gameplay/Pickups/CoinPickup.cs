using UnityEngine;

namespace Game.Gameplay.Pickups
{
    [RequireComponent(typeof(Collider2D))]
    public class CoinPickup : MonoBehaviour
    {
        public int amount = 1; public GameObject prefabRef;
        void Awake() { GetComponent<Collider2D>().isTrigger = true; }
        void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            var w = other.GetComponent<Game.Meta.Wallet>();
            if (w != null) w.Add(amount);
            var pool = Game.Core.SimplePoolService.Instance;
            if (pool != null && prefabRef != null) pool.Recycle(prefabRef, gameObject); else Destroy(gameObject);
        }
    }
}