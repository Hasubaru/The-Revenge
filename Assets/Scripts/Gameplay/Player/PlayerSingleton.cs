using UnityEngine;

namespace Game.Gameplay.Player
{
    public class PlayerSingleton : MonoBehaviour
    {
        public static PlayerSingleton Instance { get; private set; }
        private void Awake() { if (Instance != null && Instance != this) { Destroy(gameObject); return; } Instance = this; }
    }
}