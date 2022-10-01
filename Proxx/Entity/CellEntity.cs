using System;
using System.Collections.Generic;
using System.Text;

namespace Proxx.Entity
{
    public class CellEntity
    {
        public IdObject Id { get; set; }
        public bool IsOpen { get; set; }
        public byte BlackHolesCount { get; set; }

    }
}
