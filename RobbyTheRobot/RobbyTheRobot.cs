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
        private GeneticAlgorithm.GeneticAlgorithm GA;

        public RobbyTheRobot(int numberOfActions, int numberOfTestGrids, int numberOfGenerations, double mutationRate, double eliteRate, int populationSize, int numberOfTrials, int gridSize = 10, int? seed = null)
        {
            if (numberOfActions <= 0 || numberOfTestGrids <= 0 || numberOfGenerations <= 0 || mutationRate < 0 || eliteRate < 0 || populationSize <= 0 || numberOfTrials <= 0)
            {
                throw new ArgumentException();
            }
            else
            {
                this.NumberOfGenerations = numberOfGenerations;
                this.NumberOfTestGrids = numberOfTestGrids;
                this.NumberOfGenerations = numberOfGenerations;
                this.MutationRate = mutationRate;
                this.EliteRate = eliteRate;
                this.GridSize = GridSize;

                GA = new GeneticAlgorithm.GeneticAlgorithm(3, 3, 3, 1, 5, 1, null, null); // temporary
            }
        }

        public void GeneratePossibleSolutions(string folderPath)
        {
            var topGene = GA.CurrentGeneration[0].Genes;
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