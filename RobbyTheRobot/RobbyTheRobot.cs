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
        private GeneticAlgorithm.GeneticAlgorithm GA { get; } // TODO: add backing fields

        public RobbyTheRobot(int numberOfTestGrids, int numberOfGenerations, double mutationRate, double eliteRate, int populationSize, int numberOfGenes, int lengthOfGene, int numberOfTrials, int gridSize, int? seed, int numberOfActions = 200)
        {
            if (numberOfActions <= 0 || numberOfTestGrids <= 0 || numberOfGenerations <= 0 || mutationRate < 0 || mutationRate > 1 ||
                eliteRate < 0 || eliteRate > 100 || populationSize <= 0 || numberOfTrials <= 0 || gridSize <= 0)
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

                GA = new GeneticAlgorithm.GeneticAlgorithm(populationSize, numberOfGenes, lengthOfGene, mutationRate, eliteRate, numberOfTrials, ComputeFitness, seed);
            }
        }

        public double test(IChromosome chromosome, IGeneration generation){ //TODO: to remove
            Random r = new Random();
            return r.Next(10);
        }

        public double ComputeFitness(IChromosome chromosome, IGeneration generation) //TODO: to fix
        {
            Random rand = new Random();
            int x = rand.Next(this.GridSize);
            int y = rand.Next(this.GridSize);
            int[] moves = chromosome.Genes;
            // Array.ForEach(moves, Console.Write);
            ContentsOfGrid[,] grid = this.GenerateRandomTestGrid();
            double score = 0;
            for (int i = 0; i < this.NumberOfActions; i++)
            {
                score += RobbyHelper.ScoreForAllele(moves, grid, rand, ref x, ref y);
                // Console.WriteLine("position: " + x + "," + y);
            }
            Console.WriteLine("score: " + score);
            Console.WriteLine("AverageFitness: " + generation.AverageFitness);
            Console.WriteLine("----------------------------------------------------");

            return score;
        }

        public void GeneratePossibleSolutions(string folderPath) //TODO: loop next gens  
        {
            string result = "";
            while (true)
            {
                var count = GA.GenerationCount;
                if (count == 1 || count == 20 || count == 100 || count == 200 || count == 500 || count == 1000)
                {
                    GenerationDetails gen = GA.CurrentGeneration as GenerationDetails;
                    gen.EvaluateFitnessOfPopulation();
                    for (int i = 0; i < gen.NumberOfChromosomes; i++)
                    {
                        Console.WriteLine("chromosome "+i +" fitness: "+gen[i].Fitness);
                    }
                    string genes = "";
                    for (int j = 0; j < gen[0].Genes.Length; j++)
                    {
                        genes += gen[0].Genes[j];
                    }
                    result += gen.MaxFitness + "," + gen[0].Genes.Length + "," + genes + "\r\n";
                }
                if (count == 5)
                    break;
                else
                    GA.GenerateGeneration();
                    Console.WriteLine(GA.GenerationCount);
            }
            // Write string to file
            System.IO.File.WriteAllText(folderPath, result);
            //TODO: to remove, code for testing purposes
            // Open the file to read from. 
            string readText = System.IO.File.ReadAllText(folderPath);
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