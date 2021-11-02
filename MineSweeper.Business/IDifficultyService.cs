using Minesweeper.Model;

namespace MineSweeper.Business
{
    public interface IDifficultyService
    {
        public Difficulty GetDifficultyById(int id);
    }
}