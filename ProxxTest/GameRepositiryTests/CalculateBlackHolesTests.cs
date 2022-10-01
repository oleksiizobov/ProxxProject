using NUnit.Framework;
using Proxx.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProxxTest.GameRepositiryTests
{
    public class CalculateBlackHolesTests : GameRepositoryBase
    {
        [Test]
        public void CheckAllCellsAreBlackHoles()
        {
            var gameRepo = GetGameRepository();
            var boardEntity = new BoardEntity(3,3)
            {
                BlackHoles = new HashSet<IdObject> 
                {
                    new IdObject(0,0), new IdObject(0,1), new IdObject(0, 2),
                    new IdObject(1, 0),  new IdObject(1, 2),
                    new IdObject(2,0), new IdObject(2,1), new IdObject(2, 2)
                }
            };

            gameRepo.CalculateAdjacentBlackHoles(boardEntity);
            Assert.AreEqual(boardEntity.Cells.Count, 1);
            Assert.AreEqual(boardEntity.Cells[new IdObject(1,1)].BlackHolesCount, boardEntity.BlackHoles.Count);
        }
        [Test]
        public void CheckGetAdjacentCells()
        {
            var gameRepo = GetGameRepository();
            var boardEntity = new BoardEntity(3, 3)
            {
                BlackHoles = new HashSet<IdObject>
                {
                    new IdObject(1,1)
                }
            };

            gameRepo.CalculateAdjacentBlackHoles(boardEntity);
            Assert.AreEqual(boardEntity.Cells.Count, 8);
        }
    }
}
