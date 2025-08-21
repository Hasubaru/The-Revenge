using UnityEngine;
using UnityEngine.UI;
using Game.Core;

public class ResultsPanel : MonoBehaviour
{
    public CanvasGroup group; public Text timeText; public Text levelText; public Text coinsText; public Button restartButton;

    void Awake() { Hide(); if (restartButton) restartButton.onClick.AddListener(Restart); }
    void OnEnable() { Game.Core.EventBus.OnBossDefeated += OnBossDefeated; Game.Core.EventBus.OnPlayerDied += OnPlayerDied; }
    void OnDisable() { Game.Core.EventBus.OnBossDefeated -= OnBossDefeated; Game.Core.EventBus.OnPlayerDied -= OnPlayerDied; }

    void OnPlayerDied()
    {
        float t = Game.Core.TimerService.Instance ? Game.Core.TimerService.Instance.Elapsed : 0f;
        var pxp = FindObjectOfType<Game.Gameplay.Player.PlayerExperience>();
        var wallet = FindObjectOfType<Game.Meta.Wallet>();
        if (timeText) timeText.text = FormatTime(t);
        if (levelText) levelText.text = pxp ? ($"Lv. {pxp.level}") : "Lv. —";
        if (coinsText) coinsText.text = wallet ? wallet.coins.ToString() : "0";
        if (wallet) Game.Meta.MetaSave.AddCoins(wallet.coins);
        Time.timeScale = 0f; Show();
    }

    void OnBossDefeated()
    {
        Time.timeScale = 0f;
        float t = Game.Core.TimerService.Instance ? Game.Core.TimerService.Instance.Elapsed : 0f;
        var pxp = FindObjectOfType<Game.Gameplay.Player.PlayerExperience>();
        var wallet = FindObjectOfType<Game.Meta.Wallet>();
        if (timeText) timeText.text = FormatTime(t);
        if (levelText) levelText.text = pxp ? ($"Lv. {pxp.level}") : "Lv. —";
        if (coinsText) coinsText.text = wallet ? wallet.coins.ToString() : "0";
        Show();
    }

    string FormatTime(float sec) { int m = Mathf.FloorToInt(sec / 60f); int s = Mathf.FloorToInt(sec % 60f); return $"{m:00}:{s:00}"; }
    void Restart() { Time.timeScale = 1f; UnityEngine.SceneManagement.SceneManager.LoadScene("Boot"); }
    void Show() { if (group) { group.alpha = 1; group.blocksRaycasts = true; group.interactable = true; } }
    void Hide() { if (group) { group.alpha = 0; group.blocksRaycasts = false; group.interactable = false; } }
}