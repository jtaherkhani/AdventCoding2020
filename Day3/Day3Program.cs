﻿using System;

namespace Day3
{
    class Day3Program
    {
        private static int TobogganHillX { get; set; }
        private static int TobogganHillY { get; set; }
        private static char[,] TobogganHill { get; set; }

        private const int RowRepetition = 100;
        private const char Tree = '#';

        static void Main(string[] args)
        {
            TobogganHill = CreateTobogganHill();
            TobogganHillX = TobogganHill.GetUpperBound(0);
            TobogganHillY = TobogganHill.GetUpperBound(1);

            decimal oneByOne = TraverseToBottomCountTrees(new Position(), 1, 1);
            decimal threeByOne = TraverseToBottomCountTrees(new Position(), 1, 3);
            decimal fiveByOne = TraverseToBottomCountTrees(new Position(), 1, 5);
            decimal sevenByOne = TraverseToBottomCountTrees(new Position(), 1, 7);
            decimal oneByTwo = TraverseToBottomCountTrees(new Position(), 2, 1);
            Console.WriteLine(string.Format("Sum of trees found {0}", (oneByOne* threeByOne * fiveByOne * sevenByOne * oneByTwo)));
        }

        static decimal TraverseToBottomCountTrees(Position position, int xToMove, int yToMove, int treesFound = 0)
        {
            if (TobogganHill[position.X, position.Y] == Tree)
            {
                treesFound++;
            }

            position.Move(xToMove, yToMove);

            if (TryTraverse(position))
            {
                return TraverseToBottomCountTrees(position, xToMove, yToMove, treesFound);
            }

            return Convert.ToDecimal(treesFound);
        }

        private static char[,] CreateTobogganHill()
        {
            var rows = TobogganHillString.Split('\n');

            int xIndex = rows.Length;
            int yIndex = rows[0].Length * RowRepetition;
            var tobbogganHill = new char[xIndex, yIndex];

            for (int x = 0; x < xIndex; x++)
            {
                string row = rows[x].Trim();

                for (int y = 0; y < yIndex; y++)
                {
                    var stringValueIndex = y % row.Length;
                    tobbogganHill[x, y] = row[stringValueIndex];
                }
            }

            return tobbogganHill;
        }

        private static bool TryTraverse(Position newPosition)
        {
            if (newPosition.Y > TobogganHillY) // since the 2d array is not jagged we can assume the first row is going to hold.
            {
                throw new Exception("The rows are not repeated enough and we've gone off the side of the hill instead of reaching the bottom");
            }

            return TobogganHillX >= newPosition.X;
        }

        private static string TobogganHillString = 
@"....#.#..#.#.#.#......#....##.#
  ..##..#.#..#.##.....#.....#....
  ....#..#...#..#..####.##.#.##..
  ...............#.....##..##..#.
  ##...####..##.#..#...####...#.#
  ..#.#....##....##.........#...#
  .#..#.##..............#.....###
  ##..##..#.....#..#...#....#....
  .#.........#..#...#.#.#.....#..
  ......#...#..#.##..#.....#.#...
  .#...#.#.#.##.##.....###...#...
  ..........#.......#...#....#..#
  .....##..#.#...#...##.##.......
  ...#.###.#.#..##...#.#.........
  ###.###....#...###.#.##...#....
  ...........#....#.....##....###
  #..#.......#.....#.....##....#.
  .##.#....#...#....#......#..##.
  ..#....#..#..#......#..........
  #..#.........#.#....#.##...#.#.
  #....#.#.......#.#.#.#.......#.
  .#....#....#..##.##.#....#.#...
  ............#....#.#.#........#
  #..#..#.....#....#.##.##.#....#
  ....#......#..##..#....#...#...
  .............#.##....####...##.
  #.##..##..##.#.....#...........
  #.#...#......####.##..#.#......
  .......#.#..#...#.#.###.......#
  #..#..........#...#....#.......
  ###...#.....#....#...#..#...#..
  ...##..#.#.....#..#..#...#.#.##
  #.......#......##...##......#..
  .....#..#.....#......#.....##..
  ..#.....###......#.#..###....#.
  ....##........#...##.#..#..#...
  #...#.##...##..#####...##..##..
  ...#.#.#.#.....#.#.........##..
  .............#......#..##...#..
  .#..#...##....#......#......#..
  #.....#...##......#............
  .#.#....##.#.#..##..#####..##..
  ..#..#.....#..#.##..#.#......##
  .......#...#...#..#..##.#..##..
  ...#.##....#....#..#..#..#.....
  .....#......#.....#.....#..#.#.
  ..........#####.#..#....##...##
  ....#...#...#....#.......#.....
  #.......#........#.##.####..###
  .#........#...##...#.....#....#
  ...#.............#........#.#.#
  ..##........#..##..##.##...#.#.
  ......##......#####.........##.
  ...##.....#.#.##....#####......
  .#.#........####.......#.#....#
  .....#..#.#.#.......##...#...#.
  .....####.#...#.#..#...#..#...#
  #..#.....#...#..#..#.#....#..#.
  .....#.......#..#.##..#.#.....#
  .......#..##..###......#.......
  .......##.#.##..##.#.###...##..
  ..#.....#.....#....##.##.#..#.#
  ###.#...#..##..#....#.#.#..#.#.
  #...#.#.........##........#....
  #...#.#..###.....###..##.......
  .....##..#.#...#.....#....#...#
  ##...##..#.#.#..#.#.##..#....##
  .#.......#.#.........#.##..#...
  ....##.#............###.#...##.
  #.....##.###.#..#....##.....#..
  ....#.#......##.####....#....#.
  .....#......##.#...#....##.#...
  ##...#.............#...#.......
  ..#..#......#.#..#..#.....###..
  ....#...#.#...#...#.....#..###.
  .....#.......#....#...#...#...#
  ..####......#.#..###...........
  ..........#....###.#.#.....###.
  #.............#...#..#.###.#..#
  .......#...#.#.#.#...##...#..#.
  ...#.#..#..#...###.#.#.........
  #.###.#..#...#..#....#..#.#....
  ...#.#.##..#...#.#......###.##.
  .##..#..##..#.....##.##....#.##
  ..##.........####..............
  .#.#..###...#.........#...##.#.
  ....##.........#.........##...#
  ...#.#.......#..#...###.#.##.#.
  ..#....###......#.##...........
  .......#...#.....#.#..#.#...#..
  .##..#...#..#...#..#...#.#..##.
  .##...#..##..##.#.#...##.......
  .#.##.#...#..#..#..........#.#.
  #.#.#...#......##...##..##.....
  .##..#............#.##....#.#..
  .##.........##..#.....#...#...#
  ##.#.#.#.#...#.....##....##.#..
  #..##......#..##.........#.#...
  ...#....#.#.#.##.....##........
  ...#...#...#.##.#.#.#..#..#....
  .......#..#.......##...#....##.
  #.....#.#......#.......#.##.#.#
  .##..#.....#.....#.#####.#....#
  ......#.#....#..............##.
  ##..#...........#.#.#.......#..
  ..##...###...#.#.........#....#
  ..##..#.#....##.....#.#.......#
  ....###...#.###....###.......#.
  ..#.#.....#..#..#...........##.
  .###..#.#........#..#.....##.#.
  #.##........###.#..#.....#....#
  .#.#.....#.#.#..#...##.#...#...
  #.#.#...#.#........#..##..##...
  ..#.##....###.#.......#.#.#....
  .....#...##...................#
  #..####.....###.#..........#...
  #.##.........###.#........#..#.
  ..##........#.......#..###..#.#
  ##..##..#.#..#..#.....#.#..#...
  ....#......#....#...#.#.#..##.#
  .##....#.##.#.#..###..#......##
  ###....###.##....##......###..#
  .##....#..###..##..#.###...##..
  .#.......##..##.............##.
  .###..#.....#.....#.#.#..#...##
  ......##.###.#........#..#.....
  #####.....##.#.#...#...#.#.#...
  ##..##.####...##....#...#.#...#
  .#.##...#...#..#...............
  ##.##.#..#........#...#........
  ..#.##.#....#...#.#.###..#....#
  .......#.#..#.....##.#.#...#.#.
  ..#.##...#...#......#...#.#.#..
  .##.......##......#.....#......
  .#....................#.#...###
  ..#.....#..##.#......##..#....#
  .....#.#...#...........#.#...##
  ...#..#....#.#..#.......#..#..#
  .#..#.#...#.#.#.....###........
  .#.#.....#..#.##..#.#..##......
  ..##..#..#.....###.##..#.....##
  .#..#.#...#.....#..#......##.#.
  .##.##.#.#.#.#.#...###..##...#.
  ......#.##.#..#.##.#...#.#..#.#
  ..#.....#.##....#......#..#....
  .#.....#..###.............#...#
  .#.....#...#...#.#.#.#..#.#....
  .#.....#......##.....#...#.#..#
  .#.#......##...#......#......#.
  ##....#...#..##.#...#..#.......
  ....#.#......#.##...#.........#
  #.#.##.#..#......#....#.......#
  .#..#.##..#..#........#.#...#..
  ..#..#.#.#...#....#...#..#..###
  ....#....#..#......#..........#
  #.....#.......#..#....#.#.#..#.
  ....#.#..###.#.....#.####......
  ##.#.#....##.#.#........#..#..#
  #.#...#...#.#...##..#.#..#.#...
  ##.#......###.##.......#..#....
  #..#...#.......##....#.###.....
  .####.##....#..#..####.#....#.#
  #...#.#..#.....................
  ..###..#...##.....##...........
  ..#....#...###.#.........##.##.
  ......#.....#....#.#....##...##
  #..#.....#...#..##.....#....#.#
  ..#.#..#....##...###.#..##....#
  #....#..#..#..#.##.##.....#...#
  ......#.#..#..##.#.....#.#..###
  .....##...##..#...##..#...#....
  ##....#...#..#...##..#...###..#
  .##.####..#......#.#..#.##....#
  ..###........###..#....##.#....
  ...#.....##...##..##..##..#....
  .#..#.#..##..#..#..##.....#...#
  ##.#..##...#..#...........##...
  ....#..#..###.....#....#..#..##
  ......##..#....##........#.#.##
  .#.##....##.#......#..##..#..##
  .....##.#..#.#.##.##.##..#...#.
  .#..##.#.....#####...#.........
  ....#....#...#..##.#.#..##...#.
  ...#..#...#............#..#....
  ....#.#.#.##....##.###..#.#....
  .........#...###.........#..#.#
  ...........#...##.#..#.#.#..#..
  #..#.###..#.#..#..##.....#....#
  .#.#....#.#....#...............
  ...#.#..#.#..##.#.#.#.......#..
  .#......#...#.####......#..#...
  ..##..#.#...#..#.......###.....
  .#.....#.#..##....##.####.##.#.
  .............##.#.#.....#..#.#.
  #.....##.#...#.#.#.######.##...
  .##...........#..#..##.####....
  #.#............#....#.........#
  ..#.##.##.#..#....##....#..#.##
  #...#.##..##.##.#.....##.#....#
  ##.#..##.###..#.#.#..........#.
  ...##...#..#...#.#.#.###.###...
  #.....##......#...#.#...#......
  #.#.#.#.#.#...#..#....###...#..
  ...##.#...#.......#..#...##.#..
  ..#..#..##.....#......##...###.
  .............#.##...#.#.###..#.
  ..#.#.....##..#.##..#...##..#..
  ..#...#.##..###..........#..#..
  #.##.##...###...........#....#.
  #.....##...#.#..............#..
  ##..##.....#...#..####.#...##..
  ...........#......##.###..####.
  #...#..##.##.######.....#.....#
  #.##.........##.#.#....##...#..
  .##.#.......###.#.....#.....###
  ###.#.#.#.#.#.##......#..#..#.#
  ....#.###...#....#.##...##.##..
  ....#..#.....#.#.#..#..##.#....
  ....#..#..#.....#.#..##........
  ..........#..##..##......##..#.
  #...##.......#...##.#...###..#.
  ..#.#.##.....##....#..#.##...#.
  .#.#.....#.......##.....##...#.
  #......#.........#.#.........#.
  .......#...##......#.........#.
  ..##..........#....#..#.......#
  .......#............#..#.#...#.
  #..#....#.#..#....##..#........
  ....#..###.##..#.#..#.##..###..
  ....###............##.#....#.#.
  ..#..#.##...#....#..####...#...
  ..#....#...#...##...#.#.#..#...
  ..#.........#.#.......#........
  .........##.##.#..#.#...#.#..##
  #.....#.#....##.#####.......##.
  .#..#....#......#.##..#....#...
  ........#....#...........#...#.
  .......#......#..........#..##.
  .###.#......#..#.##..#...#.#...
  .....#..#..###...........#...#.
  ..#...##....###......#....#....
  ...#.#..#.#.#......#.##.###.#.#
  .##....#...#..#.#..#........#..
  ......##.###...##.#.#.........#
  .#...#..####..#.#..##........#.
  #..#...#..#..#.#...#..##...#..#
  ..###...###....#.#.#.##....#..#
  .#.#....#.#.#......##....#..#.#
  ##.#.#.####....#........#....#.
  ...#......#........#...........
  #.#............##......#.##....
  ..##.#...#.....#.#..#.#..#.#.#.
  #.......##.....##...#.#.#...###
  ............#..#..#....#......#
  .#.##...###...#...###..#.......
  ...............#....#...#.#.#..
  #..##..##.###...#..##...#.#.##.
  ..#..#.......#.##......#..#..#.
  #.....#............#......#...#
  .###.##......##.#...#.#.##..#..
  .#..##..#..#..#.............#..
  #...#...##..##........#........
  ...#........#..###...........#.
  #.#..#.#...................#.##
  #...#.#..#.......##...###..#.#.
  ..####......#....#.#....#..#..#
  ....#...........#...#..#.......
  ...#..#....#.#.##...#.#.#.#....
  #...##.#.##..#.......#.....#...
  .##....#...#.#....#....#.#...#.
  ##.#...#.#...##..#..##...##..#.
  #..#.#.........#.......#.......
  .....##.....#..#......##....#.#
  .###...##.#.#.#....#....#....#.
  #.#.#.............#.#..#.......
  #.......#..............#...#.##
  .#.#...#....#.........###...#.#
  ..##..###..#...#...#.#....##..#
  .#..#.#...#..#.....#....#..##..
  ##.......##....#....###..#.#..#
  #.#.#####..........#.#...##..##
  ......#..#..#...#...##...#....#
  #..#......#...#...#..###.......
  ...####.....#.......#.#...##.#.
  ......#..#.....##..#...........
  #........#..#...#.....#...#.#..
  ..#.....#..#......#.#.#.....#..
  ..#.........#..##...#...#...#..
  ##..##......#.........#........
  ..#..#....#.##.#....###.#..#.##
  ..##..#..#.......###....#..#...
  ...#.#...#.....####.#..........
  ........#..#..#.#.....#........
  ...##..........#.#.#.....#..#..
  ..#....#.......#...............
  .#..#.#.#.##..#..#.....#.......
  #.##.#.#..#..............#.....
  .#.#..#.....##..##....##.....#.
  .##.#..#........##.##...##.....
  #....##..#............#....#...
  ...............##.#...#..#.....
  ..#..##.##...#.#.....#.........
  .##..#.#.#.##.....#.#.#..#..##.
  ......#####.#...#..........##..
  ..........##.##...#.....#.#....
  ..##......#..#.###..#...#.##...
  .#...........#.....#.#........#
  .#...#................#......#.
  #...#.#..##.#####..##....#.....
  ...##...##..#.#..........#.....
  ##............#......##..##...#
  ###.#.......#..#...#..#..#...#.
  .#..##.....###.#.#............#
  ##.###.#.........#.......#.#..#
  ...#..##..#.....#.......#......
  ......#.#..#.....##..#..##.....
  ...#........##..###.#....#..#..
  ..#...##.##....#.##..###......#
  ..#...#.....#.####.....#...#.##
  ..........##....###..#...#####.
  ....#.#.#.#.#.##.............##
  .#.#.#.##......#......#....#.#.
  .##...##....#...#....#..###.#.#";

        public class Position
        {
            public int X { get; set; } = 0;
            public int Y { get; set; } = 0;

            public void Move(int xToMove, int yToMove)
            {
                X += xToMove;
                Y += yToMove;
            }
        }
    }
}
