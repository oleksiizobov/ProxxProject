using NUnit.Framework;
using Proxx.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProxxTest.GameRepositiryTests
{
    public class GetNewOpenedCellsTests : GameRepositoryBase
    {
        [Test]
        public void CheckValidaionSkipForOpenCell()
        {
            var gameRepo = GetGameRepository();
            var cells = new List<CellEntity>
            {
                new CellEntity {Id = new IdObject(0, 0), IsOpen = false },
                new CellEntity {Id = new IdObject(0, 1), IsOpen = true },
                new CellEntity {Id = new IdObject(1, 1), IsOpen = false, BlackHolesCount = 1 },
                new CellEntity {Id =new IdObject(1, 0), IsOpen = false, BlackHolesCount = 1 }
            };

            var boardEntity = new BoardEntity(3, 3);
            boardEntity.Cells = cells.ToDictionary(x => x.Id);

            var expectedRows = boardEntity.Cells.Where(x => !x.Value.IsOpen).Select(x => x.Key).ToList();

            var actualRows = gameRepo.GetNewOpenedCells(new IdObject(0, 0), boardEntity);

            Assert.AreEqual(actualRows.Count, expectedRows.Count);
            Assert.IsEmpty(actualRows.Where(x => !expectedRows.Contains(x.Id)));
        }

        [Test]
        public void CheckAdjacentCellWithoutBH()
        {
            var gameRepo = GetGameRepository();
            var boardEntity = new BoardEntity(4, 4);

            for (int row = 0; row < boardEntity.Rows; row++)
            {
                for (int col = 0; col < boardEntity.Columns; col++)
                {
                    var currentCellId = new IdObject(row, col);
                    boardEntity.Cells.Add(currentCellId, new CellEntity { Id = currentCellId, BlackHolesCount = 0 });
                }
            }
            boardEntity.BlackHoles.Add(new IdObject(3, 3));
            boardEntity.Cells.Remove(new IdObject(3, 3));
            boardEntity.Cells[new IdObject(2, 2)].BlackHolesCount = 1;
            boardEntity.Cells[new IdObject(2, 3)].BlackHolesCount = 1;
            boardEntity.Cells[new IdObject(3, 2)].BlackHolesCount = 1;

            var actualRows = gameRepo.GetNewOpenedCells(new IdObject(0, 0), boardEntity);

            Assert.AreEqual(actualRows.Count, boardEntity.Cells.Count);
            Assert.IsEmpty(actualRows.Where(x => !boardEntity.Cells.ContainsKey(x.Id)));
        }
    }
}
