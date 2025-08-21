using UnityEngine;

namespace Game.Gameplay.Enemies {
  [RequireComponent(typeof(Collider2D))]
  public class EnemyContactDamage : MonoBehaviour {
    public int damage = 1;
    public float interval = 0.35f; // mỗi bao lâu gây damage 1 lần khi còn chạm
    float _cd;

    void Reset(){ var c = GetComponent<Collider2D>(); if (c) c.isTrigger = true; }
    void Update(){ if (_cd > 0f) _cd -= Time.deltaTime; }

    void OnTriggerStay2D(Collider2D other){
      if (_cd > 0f) return;
      if (!other.CompareTag("Player")) return;
      var hp = other.GetComponent<Game.Gameplay.Player.PlayerHealth>();
      if (hp == null) return;
      hp.ApplyHit(new Game.Gameplay.Combat.Hit(damage, Vector2.zero));
      _cd = Mathf.Max(0.1f, interval);
    }
  }
}