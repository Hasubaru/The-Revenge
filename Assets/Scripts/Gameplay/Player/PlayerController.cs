using UnityEngine;

namespace Game.Gameplay.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] float _moveSpeed = 5f;
        private Rigidbody2D _rb;
        private Vector2 _input;

        private void Awake() { _rb = GetComponent<Rigidbody2D>(); _rb.gravityScale = 0; }
        private void Update() { _input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized; }
        private void FixedUpdate() { _rb.linearVelocity = _input * _moveSpeed; }
    }
}