using System;
using System.Collections.Generic;
using System.Text;

namespace Proxx.Entity
{
    public struct IdObject
    {
      
        public IdObject(int row, int column)
        {
            Row = row;
            Column = column;
            hash = (Row, Column).GetHashCode();
        }
        public readonly int Row { get; }
        public readonly int Column { get; }
        private readonly int hash;

        public override bool Equals(object obj) => obj is IdObject other && this.Equals(other);

        public bool Equals(IdObject other) => Row == other.Row && Column == other.Column;

        public override int GetHashCode() => hash;

        public static bool operator ==(IdObject lhs, IdObject rhs) => lhs.Equals(rhs);

        public static bool operator !=(IdObject lhs, IdObject rhs) => !(lhs == rhs);
    }
}
