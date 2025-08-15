using System;

namespace Game.Core
{
    public sealed class RngService
    {
        private readonly Random _drop, _levelup, _spawn;
        public RngService(int seed)
        {
            _drop = new Random(seed ^ 0xA5A5);
            _levelup = new Random(seed ^ 0x5A5A);
            _spawn = new Random(seed ^ 0xC3C3);
        }
        public float Next01Drop() => (float)_drop.NextDouble();
        public float Next01Level() => (float)_levelup.NextDouble();
        public float Next01Spawn() => (float)_spawn.NextDouble();
        public int RangeSpawn(int min, int max) => _spawn.Next(min, max);
    }
}