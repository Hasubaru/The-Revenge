using System;

namespace Game.Core
{
    public static class EventBus
    {
        public static event Action<int> OnLevelUp;
        public static event Action<int> OnCoinsChanged;
        public static event Action<float> OnMinuteChanged; // mỗi khi chuyển phút nguyên
        public static event System.Action OnBossDefeated;
        public static event System.Action OnEnemyKilled;
        public static event System.Action OnPlayerDied;
        public static event System.Action OnChestOpened;

        public static void BossDefeated() => OnBossDefeated?.Invoke();
        public static void EnemyKilled() => OnEnemyKilled?.Invoke();

        public static void PlayerDied() => OnPlayerDied?.Invoke();
        public static void ChestOpened() => OnChestOpened?.Invoke();

        public static void LevelUp(int level) => OnLevelUp?.Invoke(level);
        public static void CoinsChanged(int coins) => OnCoinsChanged?.Invoke(coins);
        public static void MinuteChanged(float minute) => OnMinuteChanged?.Invoke(minute);

    }
}