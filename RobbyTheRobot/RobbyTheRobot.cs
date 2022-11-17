using System;
using GeneticAlgorithm;

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
        private GeneticAlgorithm.GeneticAlgorithm GA { get; }

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
                this.GridSize = gridSize;

                GA = new GeneticAlgorithm.GeneticAlgorithm(3, 3, 3, 1, 5, 1, null, seed); // temporary
            }
        }

        public void GeneratePossibleSolutions(string folderPath) //TODO: loop next gens  
        {
            string result = "";
            for (int i = 1; i <= 1000; i++)
            {
                if (i == 1 || i == 20 || i == 100 || i == 200 || i == 500 || 1 == 1000)
                {
                    GenerationDetails gen = GA.CurrentGeneration as GenerationDetails;
                    gen.EvaluateFitnessOfPopulation();
                    result += gen.MaxFitness + ", " + gen[0].Genes.Length + ", " + gen[0].Genes + "\r\n";
                }
                GA.GenerateGeneration(); // or maybe run this before the if?
            }
            // Write string to file
            System.IO.File.WriteAllText("C:\\Users\\Jeremy\\Desktop\\test.txt", result);
            //TODO: to remove, code for testing purposes
            // Open the file to read from. 
            string readText = System.IO.File.ReadAllText("C:\\Users\\Jeremy\\Desktop\\test.txt");
            Console.WriteLine(readText);
        }

        public ContentsOfGrid[,] GenerateRandomTestGrid()
        {
            ContentsOfGrid[,] contents = new ContentsOfGrid[this.GridSize, this.GridSize];
            Random random = new Random();
            int count = 0;
            int half = contents.Length / 2;

            for (int i = 0; i < contents.GetLength(0); i++)
            {
                for (int j = 0; j < contents.GetLength(1); j++)
                {
                    if (count >= half)
                        contents[i, j] = ContentsOfGrid.Empty;
                    else
                        contents[i, j] = ContentsOfGrid.Can;
                    count++;
                }
            }
            Shuffle(contents, random);
            return contents;
        }

        // shuffles the content of a grid
        private static void Shuffle(ContentsOfGrid[,] contents, Random random)
        {
            int rowLength = contents.GetLength(1);
            // using Fisher–Yates shuffle Algorithm
            for (int i = contents.Length - 1; i > 0; i--)
            {
                int i0 = i / rowLength;
                int i1 = i % rowLength;

                int j = random.Next(i + 1);
                int j0 = j / rowLength;
                int j1 = j % rowLength;

                ContentsOfGrid temp = contents[i0, i1];
                contents[i0, i1] = contents[j0, j1];
                contents[j0, j1] = temp;
            }
        }
    }
}