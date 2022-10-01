using Proxx.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proxx.Repositiry
{
    internal interface IGameRepository
    {
        HashSet<IdObject> GetBlackHoles(int rows, int columns, int blackHoles);
        void CalculateAdjacentBlackHoles(BoardEntity board);
        List<CellEntity> GetNewOpenedCells(IdObject currentCellId, BoardEntity board);
    }
}
