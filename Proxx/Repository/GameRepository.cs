﻿using Proxx.Entity;
using Proxx.Manager.RandomManager;
using Proxx.Repositiry;
using System.Collections.Generic;

namespace Proxx.Manager
{
    public class GameRepository : IGameRepository
    {
        public IRandomMan RandomMan  { get; set; }
        public GameRepository()
        {
            RandomMan = new RandomMan();
        }

        //Part2
        public HashSet<IdObject> GetBlackHoles(int rows, int columns, int blackHoles)
        {
            var result = new HashSet<IdObject>();

            //this check helps to avoid infinity loop
            var maxBlackHoles = rows * columns - 1;           
            if (maxBlackHoles < blackHoles)
            {
                blackHoles = maxBlackHoles;
            }
            int rowBuff, colBuff;
            while (result.Count < blackHoles)
            {
                // RandomMan is a wrapper over Random. It is good approach for unit tests.
                rowBuff = RandomMan.Next(0, rows);
                colBuff = RandomMan.Next(0, columns);
                result.Add(new IdObject(rowBuff, colBuff));
            }
            return result;
        }

        //Part3
        public void CalculateAdjacentBlackHoles(BoardEntity board)
        {
            for (int row = 0; row < board.Rows; row++)
            {
                for (int col = 0; col < board.Columns; col++)
                {
                    var currentCellId = new IdObject(row, col);
                    if (board.BlackHoles.Contains(currentCellId))
                    {
                        continue;
                    }

                    if (!board.Cells.TryGetValue(currentCellId, out CellEntity currentCell))
                    {
                        currentCell = new CellEntity { Id = currentCellId };
                        board.Cells.Add(currentCellId, currentCell);
                    }

                    var adjacentCells = GetAdjacentCells(currentCellId, board);

                    foreach (var adjacentCell in adjacentCells)
                    {
                        if (board.BlackHoles.Contains(adjacentCell))
                        {
                            currentCell.BlackHolesCount++;
                        }
                    }
                }
            }
        }


        //Part4
        public List<CellEntity> GetNewOpenedCells(IdObject currentCellId, BoardEntity board)
        {
            List<CellEntity> openedCellIds = new List<CellEntity>();
            if (board.BlackHoles.Contains(currentCellId))
            {
                return openedCellIds;
            }
            if (!board.Cells.TryGetValue(currentCellId, out CellEntity currentCell))
            {
                return openedCellIds;
            }
            if (currentCell.IsOpen)
            {
                return openedCellIds;
            }

            openedCellIds.Add(currentCell);
            currentCell.IsOpen = true;

            if (currentCell.BlackHolesCount == 0)
            {
                var adjacentCells = GetAdjacentCells(currentCellId, board);
                foreach (var adjacentCell in adjacentCells)
                {
                    openedCellIds.AddRange(GetNewOpenedCells(adjacentCell, board));
                }
            }

            return openedCellIds;
        }



        private List<IdObject> GetAdjacentCells(IdObject currentCell, BoardEntity board)
        {
            List<IdObject> result = new List<IdObject>();
            int? nextCol = currentCell.Column + 1 < board.Columns ? currentCell.Column + 1 : default(int?);
            int? nextRow = currentCell.Row + 1 < board.Rows ? currentCell.Row + 1 : default(int?);
            int? previousCol = currentCell.Column - 1 >= 0 ? currentCell.Column - 1 : default(int?);
            int? previousRow = currentCell.Row - 1 >= 0 ? currentCell.Row - 1 : default(int?);
            if (nextCol.HasValue)
            {
                result.Add(new IdObject(currentCell.Row, nextCol.Value));
            }
            if (previousCol.HasValue)
            {
                result.Add(new IdObject(currentCell.Row, previousCol.Value));
            }
            if (nextRow.HasValue)
            {
                result.Add(new IdObject(nextRow.Value, currentCell.Column));
            }
            if (previousRow.HasValue)
            {
                result.Add(new IdObject(previousRow.Value, currentCell.Column));
            }

            if (previousRow.HasValue && nextCol.HasValue)
            {
                var cellFirtsQuartile = new IdObject(previousRow.Value, nextCol.Value);
                result.Add(cellFirtsQuartile);
            }
            if (previousRow.HasValue && previousCol.HasValue)
            {
                var cellSecondQuartile = new IdObject(previousRow.Value, previousCol.Value);
                result.Add(cellSecondQuartile);
            }

            if (nextRow.HasValue && previousCol.HasValue)
            {
                var cellThirdQuartile = new IdObject(nextRow.Value, previousCol.Value);
                result.Add(cellThirdQuartile);
            }

            if (nextRow.HasValue && nextCol.HasValue)
            {
                var cellFourthQuartile = new IdObject(nextRow.Value, nextCol.Value);
                result.Add(cellFourthQuartile);
            }
            return result;

        }


    }
}
