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
            this.GridSize = 100; // TODO: temporary, for testing
        }

        public void GeneratePossibleSolutions(string folderPath)
        {
            throw new System.NotImplementedException();
        }

        public ContentsOfGrid[,] GenerateRandomTestGrid()
        {
            // assuming grid is square
            int side = (int)Math.Floor(Math.Sqrt(this.GridSize));
            int gridArea = side * side;
            ContentsOfGrid[,] contents = new ContentsOfGrid[side, side];
            Random random = new Random();
            int canCount = 0;
            int emptyCount = 0;
            int half = (int)Math.Floor((double)gridArea / 2);

            for (int i = 0; i < contents.GetLength(0); i++)
            {
                for (int j = 0; j < contents.GetLength(1); j++)
                {
                    if (canCount >= half)
                    {
                        contents[i, j] = ContentsOfGrid.Empty;
                        emptyCount++;
                    }
                    else if (emptyCount >= half)
                    {
                        contents[i, j] = ContentsOfGrid.Can;
                        canCount++;
                    }
                    else
                    {
                        int num = random.Next(2);
                        if (num == 0)
                        {
                            contents[i, j] = ContentsOfGrid.Can;
                            canCount++;
                        }
                        else
                        {
                            contents[i, j] = ContentsOfGrid.Empty;
                            emptyCount++;
                        }
                    }
                }
            }
            return contents;
        }
    }
}