using System;

namespace Minesweeper.Model
{
    public sealed class Cell
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int AdjacentBombs { get; set; }

        public bool IsBomb { get; set; }

        public bool IsMarkedByPlayer { get; set; }

        public bool IsOpenedByPlayer { get; set; }

        public override int GetHashCode() => HashCode.Combine<int, int>(this.X, this.Y);

        public override bool Equals(object other) => this.Equals(other as Cell);

        public bool Equals(Cell other) => this.X == other.X && this.Y == other.Y;

        public override string ToString() => string.Format("({0}, {1}) = {2}", (object)this.X, (object)this.Y, (object)this.AdjacentBombs);
    }
}