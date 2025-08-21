using UnityEngine;

namespace Game.Gameplay.Pickups
{
    [RequireComponent(typeof(Collider2D))]
    public class ChestPickup : MonoBehaviour
    {
        public GameObject prefabRef;
        void Awake() { GetComponent<Collider2D>().isTrigger = true; }
        void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            var panel = Object.FindObjectOfType<UI.ChestPanel>();
            if (panel) panel.Open();
            Game.Core.EventBus.ChestOpened();
            var pool = Game.Core.SimplePoolService.Instance;
            if (pool != null && prefabRef != null) pool.Recycle(prefabRef, gameObject); else Destroy(gameObject);
        }
    }
}