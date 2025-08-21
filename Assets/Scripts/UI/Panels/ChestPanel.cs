using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Gameplay.Player;

namespace UI
{
    public class ChestPanel : MonoBehaviour
    {
        [System.Serializable] public class OptionWidgets { public Button button; public Text title; public Text desc; }
        public CanvasGroup group; public OptionWidgets[] options = new OptionWidgets[3];

        readonly List<string> _pool = new() { "DMG20", "CD10", "AREA20", "PIERCE+1", "REROLL+1", "HEAL20" };
        string[] _current = new string[3]; PlayerStats _stats; PlayerHealth _hp;

        void Awake() { _stats = FindObjectOfType<PlayerStats>(); _hp = FindObjectOfType<PlayerHealth>(); Hide(); }

        public void Open() { Time.timeScale = 0f; for (int i = 0; i < 3; i++) { _current[i] = _pool[Random.Range(0, _pool.Count)]; SetupButton(i, _current[i]); } Show(); }

        void SetupButton(int i, string id)
        {
            var w = options[i]; if (w == null || w.button == null) return; string title, desc; GetTexts(id, out title, out desc);
            if (w.title) w.title.text = title; if (w.desc) w.desc.text = desc;
            w.button.onClick.RemoveAllListeners(); w.button.onClick.AddListener(() => { Apply(id); Hide(); Time.timeScale = 1f; });
        }

        void Apply(string id)
        {
            if (id == "DMG20") _stats?.AddDamagePercent(0.20f);
            else if (id == "CD10") _stats?.ReduceCooldownPercent(0.10f);
            else if (id == "AREA20") _stats?.AddAreaPercent(0.20f);
            else if (id == "PIERCE+1") _stats?.AddPierce(1);
            else if (id == "REROLL+1") _stats?.AddReroll(1);
            else if (id == "HEAL20") _hp?.HealPercent(0.20f);
        }

        void GetTexts(string id, out string title, out string desc)
        {
            switch (id)
            {
                case "DMG20": title = "+20% Damage"; desc = "Tăng sát thương tổng"; break;
                case "CD10": title = "-10% Cooldown"; desc = "Bắn nhanh hơn"; break;
                case "AREA20": title = "+20% Area"; desc = "Hitbox & aura to hơn"; break;
                case "PIERCE+1": title = "+1 Pierce"; desc = "Đạn xuyên thêm 1 mục tiêu"; break;
                case "REROLL+1": title = "+1 Reroll"; desc = "Thêm lượt reroll ở LevelUp"; break;
                case "HEAL20": title = "Heal 20%"; desc = "Hồi 20% máu tối đa"; break;
                default: title = id; desc = ""; break;
            }
        }

        void Show() { if (group) { group.alpha = 1; group.blocksRaycasts = true; group.interactable = true; } }
        void Hide() { if (group) { group.alpha = 0; group.blocksRaycasts = false; group.interactable = false; } }
    }
}