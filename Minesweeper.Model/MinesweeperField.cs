using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Minesweeper.Model
{
    public sealed class MineSweeperField
    {
        public const int MaxWidth = 1000;
        public const int MaxHeight = 1000;
        public const int MinWidth = 8;
        public const int MinHeight = 8;
        private Cell[,] _mineField;

        public MineSweeperField(int width, int height, int bombs)
        {
            Width = width;
            Height = height;
            Bombs = bombs;
            if (Width < MinWidth)
                throw new ArgumentOutOfRangeException(nameof(width));
            if (Width > MaxWidth)
                throw new ArgumentOutOfRangeException(nameof(width));
            if (Height < MinHeight)
                throw new ArgumentOutOfRangeException(nameof(height));
            if (Height > MaxHeight)
                throw new ArgumentOutOfRangeException(nameof(height));
            if (Bombs < 1)
                throw new ArgumentOutOfRangeException(nameof(bombs));
            if (Bombs >= Width * Height * 0.8)
                throw new ArgumentOutOfRangeException(nameof(bombs), "too many bombs");
        }

        public int Width { get; }

        public int Height { get; }

        public int Bombs { get; }

        public List<Cell> this[int x, int y]
        {
            get
            {
                if (!IsValidCoordinate(x, y))
                    throw new ArgumentOutOfRangeException();
                if (_mineField == null)
                    InitializeField(x, y);
                var cell = _mineField[x, y];
                if (cell.IsBomb)
                {
                    throw new Exception("You've hit a bomb! Thanks for playing...");
                }

                var openCells = new List<Cell>();
                AddOpenCells(cell, openCells);
                if (cell.AdjacentBombs == 0)
                    Debug.Assert(openCells.Count > 1);
                else
                    Debug.Assert(openCells.Count == 1);
                return openCells;
            }
        }

        public void ToggleMarkAsBomb(int x, int y)
        {
            var cell = _mineField[x, y];
            cell.IsMarkedByPlayer = cell.IsMarkedByPlayer != true;
        }

        public bool IsFinished => _mineField.Cast<Cell>().Count((Func<Cell, bool>)(it => it.IsOpenedByPlayer)) == GetNumberOfNonBombFields();

        private int GetNumberOfNonBombFields()
        {
            return Height * Width - Bombs;
        }

        private void InitializeField(int initialGuessX, int initialGuessY)
        {
            _mineField = new Cell[Width, Height];
            for (int index1 = 0; index1 < Width; ++index1)
            {
                for (int index2 = 0; index2 < Height; ++index2)
                    _mineField[index1, index2] = new Cell
                    {
                        X = index1,
                        Y = index2
                    };
            }
            var random = new Random();
            var num = 0;
            while (num < Bombs)
            {
                int index3 = random.Next(0, Width);
                int index4 = random.Next(0, Height);
                if ((index3 != initialGuessX || index4 != initialGuessY) && !_mineField[index3, index4].IsBomb)
                {
                    PlaceBomb(_mineField[index3, index4]);
                    ++num;
                }
            }
            Debug.Assert(_mineField.Cast<Cell>().Count((Func<Cell, bool>)(it => it.IsBomb)) == Bombs);
        }

        private void PlaceBomb(Cell cell)
        {
            Debug.Assert(!cell.IsBomb);
            cell.IsBomb = true;
            foreach (var neighbor in GetNeighbors(cell.X, cell.Y))
                ++neighbor.AdjacentBombs;
        }

        private void AddOpenCells(Cell cell, List<Cell> openCells)
        {
            openCells.Add(cell);
            cell.IsOpenedByPlayer = true;
            if ((uint)cell.AdjacentBombs > 0U)
                return;
            foreach (var neighbor in GetNeighbors(cell.X, cell.Y))
            {
                if (!openCells.Contains(neighbor))
                    AddOpenCells(neighbor, openCells);
            }
        }

        private IEnumerable<Cell> GetNeighbors(int x, int y)
        {
            var valueTupleArray = new[]
            {
        (x - 1, y - 1),
        (x, y - 1),
        (x + 1, y - 1),
        (x - 1, y),
        (x + 1, y),
        (x - 1, y + 1),
        (x, y + 1),
        (x + 1, y + 1)
            };
            foreach (var t in valueTupleArray)
            {
                var (x3, y3) = t;
                if (IsValidCoordinate(x3, y3))
                    yield return _mineField[x3, y3];
            }
        }

        private bool IsValidCoordinate(int x, int y) => x >= 0 && x <= Width - 1 && y >= 0 && y <= Height - 1;

        public List<Cell> GetBombCells()
        {
            var result = new List<Cell>();

            for (var index1 = 0; index1 < this.Height; ++index1)
            {
                for (var index2 = 0; index2 < this.Width; ++index2)
                {
                    result.Add(_mineField[index2, index1]);
                }
            }

            return result;
        }

        public string Display()
        {
            var sb = new StringBuilder();
            for (var index1 = 0; index1 < this.Height; ++index1)
            {
                for (var index2 = 0; index2 < this.Width; ++index2)
                {
                    var cell = _mineField[index2, index1];
                    sb.Append(cell.IsBomb ? "B " : (cell.IsOpenedByPlayer ? "O " : cell.AdjacentBombs + " "));
                }

                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }

    }
}
