using Minesweeper.Model;
using System.Collections.Generic;
using System.Linq;

namespace MineSweeper.Business
{
    public class DifficultyService : IDifficultyService
    {
        public readonly List<Difficulty> Difficulties = new()
        {
            new Difficulty()
            {
                Id = 1,
                Height = 8,
                Width = 8,
                Bombs = 10,
                Name = "Easy"
            },
            new Difficulty()
            {
                Id = 2,
                Height = 16,
                Width = 16,
                Bombs = 40,
                Name = "Medium"
            },
            new Difficulty()
            {
                Id = 3,
                Height = 20,
                Width = 20,
                Bombs = 100,
                Name = "Hard"
            }
        };

        public Difficulty GetDifficultyById(int id)
        {
            return Difficulties.SingleOrDefault(x => x.Id.Equals(id));
        }



    }
}