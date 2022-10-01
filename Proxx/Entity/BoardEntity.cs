using System;
using System.Collections.Generic;
using System.Text;

namespace Proxx.Entity
{
    public class BoardEntity
    {

        public int Rows { get; private set; }
        public int Columns { get; private set; }
        public BoardEntity(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
        }
        //this dictionary is a storage for cells that are not black holes
        public Dictionary<IdObject, CellEntity> Cells { get; set; } = new Dictionary<IdObject, CellEntity>();
        public HashSet<IdObject> BlackHoles { get; set; } = new HashSet<IdObject>();
    }
}
