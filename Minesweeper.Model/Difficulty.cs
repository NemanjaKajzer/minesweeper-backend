﻿namespace Minesweeper.Model
{
    public class Difficulty
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public int Bombs { get; set; }
    }
}
