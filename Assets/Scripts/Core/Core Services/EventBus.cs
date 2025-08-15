using System;

namespace Game.Core
{
    public static class EventBus
    {
        public static event Action<int> OnLevelUp;
        public static event Action<int> OnCoinsChanged;
        public static event Action<float> OnMinuteChanged; // mỗi khi chuyển phút nguyên

        public static void LevelUp(int level) => OnLevelUp?.Invoke(level);
        public static void CoinsChanged(int coins) => OnCoinsChanged?.Invoke(coins);
        public static void MinuteChanged(float minute) => OnMinuteChanged?.Invoke(minute);
    }
}