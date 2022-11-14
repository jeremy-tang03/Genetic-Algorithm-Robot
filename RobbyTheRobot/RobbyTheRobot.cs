using System;

namespace RobbyTheRobot
{
    internal class RobbyTheRobot : IRobbyTheRobot
    {
        public int NumberOfActions { get; }

        public int NumberOfTestGrids { get; }

        public int GridSize { get; }

        public int NumberOfGenerations { get; }

        public double MutationRate { get; }

        public double EliteRate { get; }

        public RobbyTheRobot(int numberOfGenerations, int populationSize, int numberOfTrials, int? seed)
        {
            this.NumberOfGenerations = numberOfGenerations;
            //not sure if this is right
            this.NumberOfActions = numberOfTrials;
            // not sure for populationSize
            //not sure for seed
        }

        public void GeneratePossibleSolutions(string folderPath)
        {
            throw new System.NotImplementedException();
        }

        public ContentsOfGrid[,] GenerateRandomTestGrid()
        {
            // assuming grid is square
            int side = (int)Math.Floor(Math.Sqrt(this.GridSize));
            int gridArea = side*side;
            ContentsOfGrid[,] contents = new ContentsOfGrid[side, side];
            Random random = new Random();
            int canCount = 0;

            for (int i = 0; i < contents.GetLength(0); i++)
            {
                for (int j = 0; j < contents.GetLength(1); j++)
                {
                    int num = random.Next(2);
                    if (num == 0 && canCount <= Math.Floor((double)gridArea/2))
                    {
                        contents[i, j] = ContentsOfGrid.Can;
                        canCount++;
                    } else {
                        contents[i, j] = ContentsOfGrid.Empty;
                    }
                }
            }

            Console.WriteLine("cans: " + canCount + ", grid area: " + gridArea);
            for (int i = 0; i < contents.GetLength(0); i++)
            {
                for (int j = 0; j < contents.GetLength(1); j++)
                {
                    Console.Write("{0} ", contents[i, j]);
                }
                Console.Write("\n");
            }

            return contents;
        }
    }
}