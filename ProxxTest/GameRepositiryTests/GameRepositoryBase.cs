using Proxx.Manager;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProxxTest.GameRepositiryTests
{
    public class GameRepositoryBase
    {
        public GameRepository GetGameRepository()
        {
            return new GameRepository();
        }
    }
}
