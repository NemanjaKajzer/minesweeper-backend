using System.Collections.Generic;
using Minesweeper.Model;

namespace MineSweeper.Business
{
    public interface IMinefieldService
    {
        public MineSweeperField GetMineSweeperField();

        public void CreateMinefieldByDifficulty(Difficulty difficulty);

        public List<Cell> OpenCell(Cell cell);

        public void ToggleMarkAsBomb(Cell cell);
    }
}