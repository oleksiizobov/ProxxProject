using System;
using System.Collections.Generic;
using System.Text;

namespace Proxx.Manager.RandomManager
{
    public interface IRandomMan
    {
        int Next(int minValue, int maxValue);
    }
}
