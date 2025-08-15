using UnityEngine;
using Game.Gameplay.Combat;

[RequireComponent(typeof(Collider2D))]
public class Projectile : MonoBehaviour
{
    Vector2 _dir; float _speed; float _life; float _damage; int _pierce; Vector2 _knock; GameObject _prefabRef;

    public void Launch(Vector2 dir, float speed, float life, float damage, int pierce, Vector2 knock, GameObject prefabRef)
    {
        _dir = dir; _speed = speed; _life = life; _damage = damage; _pierce = pierce; _knock = knock; _prefabRef = prefabRef;
    }

    void Update()
    {
        transform.position += (Vector3)(_dir * _speed * Time.deltaTime);
        _life -= Time.deltaTime; if (_life <= 0f) Recycle();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var dmg = other.GetComponent<IDamageable>();
        if (dmg != null)
        {
            dmg.ApplyHit(new Hit(_damage, _knock));
            _pierce--; if (_pierce < 0) { Recycle(); }
        }
    }

    void Recycle()
    {
        var pool = Game.Core.SimplePoolService.Instance;
        if (pool != null && _prefabRef != null) pool.Recycle(_prefabRef, gameObject); else Destroy(gameObject);
    }
}