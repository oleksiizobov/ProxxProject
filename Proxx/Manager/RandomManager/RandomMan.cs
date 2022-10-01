using System;
using System.Collections.Generic;
using System.Text;

namespace Proxx.Manager.RandomManager
{
    public class RandomMan : IRandomMan
    {
        private Random _random;
        private object _lock = new object();
        public RandomMan()
        {
            if (_random == null)
            {
                lock (_lock)
                {
                    if (_random == null)
                    {
                        _random = new Random();
                    }
                }
            }
        }
        public int Next(int minValue, int maxValue)
        {
            return _random.Next(minValue, maxValue);
        }
    }
}
