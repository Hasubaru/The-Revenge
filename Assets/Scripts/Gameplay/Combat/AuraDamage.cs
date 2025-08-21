using System.Collections.Generic;
using UnityEngine;
using Game.Gameplay.Combat;

[RequireComponent(typeof(Collider2D))]
public class AuraDamage : MonoBehaviour
{
    public float dps = 10f;         // damage per second tổng mỗi mục tiêu
    public float tickRate = 5f;     // số lần gây damage mỗi giây

    Game.Gameplay.Player.PlayerStats _stats;
    float _tickAcc;
    readonly List<IDamageable> _inside = new();
    Vector3 _baseScale;

    void Awake()
    {
        _stats = GetComponentInParent<Game.Gameplay.Player.PlayerStats>();
        var col = GetComponent<Collider2D>(); col.isTrigger = true;
        _baseScale = transform.localScale;
    }

    void OnEnable()
    {
        // refresh scale theo areaMult
        float s = _stats ? _stats.areaMult : 1f;
        transform.localScale = _baseScale * s;
    }

    void Update()
    {
        float tickInterval = 1f / Mathf.Max(1f, tickRate);
        _tickAcc += Time.deltaTime;
        if (_tickAcc >= tickInterval)
        {
            _tickAcc -= tickInterval;
            float dmg = (dps / Mathf.Max(1f, tickRate)) * (_stats ? _stats.damageMult : 1f);
            for (int i = _inside.Count - 1; i >= 0; i--)
            {
                var d = _inside[i];
                if (d == null) { _inside.RemoveAt(i); continue; }
                d.ApplyHit(new Hit(dmg, Vector2.zero));
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) { var d = other.GetComponent<IDamageable>(); if (d != null && !_inside.Contains(d)) _inside.Add(d); }
    void OnTriggerExit2D(Collider2D other) { var d = other.GetComponent<IDamageable>(); if (d != null) _inside.Remove(d); }
}