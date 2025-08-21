using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Core;
using Game.Gameplay.Player;

public class LevelUpPanel : MonoBehaviour
{
    [System.Serializable] public class OptionWidgets { public Button button; public Text title; public Text desc; }

    public CanvasGroup group;                    // điều khiển hiển/ẩn
    public OptionWidgets[] options = new OptionWidgets[3];
    public Button rerollButton;                  // tuỳ chọn: có thể để null

    PlayerStats _stats; readonly List<string> _pool = new() { "DMG20", "CD10", "AREA20" };
    string[] _current = new string[3];

    void Awake() { _stats = FindObjectOfType<PlayerStats>(); Hide(); }
    void OnEnable() { EventBus.OnLevelUp += OnLevelUp; }
    void OnDisable() { EventBus.OnLevelUp -= OnLevelUp; }

    void OnLevelUp(int level)
    {
        Time.timeScale = 0f;
        for (int i = 0; i < 3; i++) { _current[i] = _pool[Random.Range(0, _pool.Count)]; SetupButton(i, _current[i]); }
        if (rerollButton)
        {
            rerollButton.onClick.RemoveAllListeners();
            rerollButton.interactable = (_stats && _stats.rerolls > 0);
            rerollButton.onClick.AddListener(() => {
                if (_stats && _stats.rerolls > 0) { _stats.rerolls--; for (int i = 0; i < 3; i++) { _current[i] = _pool[Random.Range(0, _pool.Count)]; SetupButton(i, _current[i]); } }
                rerollButton.interactable = (_stats && _stats.rerolls > 0);
            });
        }
        Show();
    }

    void SetupButton(int i, string id)
    {
        var w = options[i];
        if (w == null || w.button == null) return;
        string title, desc; GetTexts(id, out title, out desc);
        if (w.title) w.title.text = title; if (w.desc) w.desc.text = desc;
        w.button.onClick.RemoveAllListeners();
        w.button.onClick.AddListener(() => { Apply(id); Hide(); Time.timeScale = 1f; });
    }

    void Apply(string id)
    {
        if (_stats == null) return;
        switch (id)
        {
            case "DMG20": _stats.AddDamagePercent(0.20f); break;
            case "CD10": _stats.ReduceCooldownPercent(0.10f); break;
            case "AREA20": _stats.AddAreaPercent(0.20f); break;
        }
    }

    void GetTexts(string id, out string title, out string desc)
    {
        switch (id)
        {
            case "DMG20": title = "+20% Damage"; desc = "Tăng sát thương cho mọi vũ khí"; break;
            case "CD10": title = "-10% Cooldown"; desc = "Giảm thời gian hồi chiêu cho mọi vũ khí"; break;
            case "AREA20": title = "+20% Area"; desc = "Tăng kích thước hitbox/aura"; break;
            default: title = id; desc = ""; break;
        }
    }

    void Show() { if (group) { group.alpha = 1f; group.blocksRaycasts = true; group.interactable = true; } }
    void Hide() { if (group) { group.alpha = 0f; group.blocksRaycasts = false; group.interactable = false; } }
}