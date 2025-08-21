using UnityEngine;

namespace Game.Meta
{
    public static class MetaSave
    {
        const string KEY_TOTAL_COINS = "TOTAL_COINS";
        public static int TotalCoins => PlayerPrefs.GetInt(KEY_TOTAL_COINS, 0);
        public static void AddCoins(int amount) { PlayerPrefs.SetInt(KEY_TOTAL_COINS, TotalCoins + Mathf.Max(0, amount)); PlayerPrefs.Save(); }
        public static void ResetCoins() { PlayerPrefs.DeleteKey(KEY_TOTAL_COINS); }
    }
}