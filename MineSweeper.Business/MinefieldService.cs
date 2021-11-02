using System.Collections.Generic;
using Minesweeper.Model;

namespace MineSweeper.Business
{
    public class MinefieldService : IMinefieldService
    {
        private static MineSweeperField _field;

        public MineSweeperField GetMineSweeperField()
        {
            return _field;
        }

        public void CreateMinefieldByDifficulty(Difficulty difficulty)
        {
            _field = new MineSweeperField(difficulty.Width, difficulty.Height, difficulty.Bombs);
        }

        public List<Cell> OpenCell(Cell cell)
        {
            return _field[cell.X, cell.Y];
        }

        public void ToggleMarkAsBomb(Cell cell)
        {
            _field.ToggleMarkAsBomb(cell.X, cell.Y);
        }
    }
}