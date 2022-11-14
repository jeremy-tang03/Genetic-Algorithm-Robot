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
            this.GridSize = 10;
        }

        public void GeneratePossibleSolutions(string folderPath)
        {
            throw new System.NotImplementedException();
        }

        public ContentsOfGrid[,] GenerateRandomTestGrid()
        {
            ContentsOfGrid[,] contents = new ContentsOfGrid[this.GridSize, this.GridSize];
            Random random = new Random();
            int canCount = 0;
            int emptyCount = 0;
            int half = contents.Length / 2;

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