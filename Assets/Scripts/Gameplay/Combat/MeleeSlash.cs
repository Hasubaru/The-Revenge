using System.Collections.Generic;
using UnityEngine;
using Game.Gameplay.Combat;

[RequireComponent(typeof(Collider2D))]
public class MeleeSlash : MonoBehaviour
{
    public System.Action OnRecycled;   // <- thêm dòng này
    float _life, _dmg; GameObject _prefabRef; float _scale = 1f; HashSet<int> _hit = new();

    public void Activate(float dmg, float life, GameObject prefabRef, float areaScale)
    {
        _dmg = dmg; _life = life; _prefabRef = prefabRef; _scale = areaScale;
        transform.localScale = Vector3.one * _scale;
        _hit.Clear();
    }

    void Update() { _life -= Time.deltaTime; if (_life <= 0f) Recycle(); }

    void OnTriggerEnter2D(Collider2D other)
    {
        var d = other.GetComponent<IDamageable>();
        if (d == null) return;
        int id = other.gameObject.GetInstanceID();
        if (_hit.Contains(id)) return;
        _hit.Add(id);
        d.ApplyHit(new Hit(_dmg, Vector2.zero));
    }

    void Recycle()
    {
        OnRecycled?.Invoke();            // <- thông báo về chủ để trừ đếm
        var pool = Game.Core.SimplePoolService.Instance;
        if (pool != null && _prefabRef != null) pool.Recycle(_prefabRef, gameObject); else Destroy(gameObject);
    }
}
