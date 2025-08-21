using UnityEngine;
using Game.Core;

namespace Game.Meta
{
    public class Wallet : MonoBehaviour
    {
        public int coins = 0;
        public void Add(int amount) { coins += Mathf.Max(0, amount); EventBus.CoinsChanged(coins); }
    }
}