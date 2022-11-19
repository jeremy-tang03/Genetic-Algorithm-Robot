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
        public event FileWritten FileWrittenEvent;
        private int _percentHelper = 0;

        public RobbyTheRobot(int numberOfTestGrids, int numberOfGenerations, double mutationRate, double eliteRate, int populationSize, int numberOfGenes, int lengthOfGene, int numberOfTrials, int gridSize = 10, int? seed = null, int numberOfActions = 200)
        {
            if (numberOfActions <= 0 || numberOfTestGrids <= 0 || numberOfGenerations <= 0 || mutationRate < 0 || mutationRate > 1 ||
                eliteRate < 0 || eliteRate > 100 || populationSize <= 0 || numberOfTrials <= 0 || gridSize <= 0)
            {
                throw new ArgumentException();
            }
            else
            {
                this.NumberOfActions = numberOfActions;
                this.NumberOfGenerations = numberOfGenerations;
                this.NumberOfTestGrids = numberOfTestGrids;
                this.MutationRate = mutationRate;
                this.EliteRate = eliteRate;
                this.GridSize = gridSize;

                GA = new GeneticAlgorithm.GeneticAlgorithm(populationSize, numberOfGenes, lengthOfGene, mutationRate, eliteRate, numberOfTrials, ComputeFitness, seed);
            }
        }

        /// <summary>
        /// Used to determine the score. Is responsible for generating a random grid and running robby through 
        /// the grid and scoring his moves using the RobbyHelper class to assist in the scoring part.
        /// </summary>
        public double ComputeFitness(IChromosome chromosome, IGeneration generation)
        {
            Random rand = new Random();
            int x = rand.Next(this.GridSize);
            int y = rand.Next(this.GridSize);
            int[] moves = chromosome.Genes;
            ContentsOfGrid[,] grid = this.GenerateRandomTestGrid();
            double score = 0;
            for (int i = 0; i < this.NumberOfActions; i++)
            {
                score += RobbyHelper.ScoreForAllele(moves, grid, rand, ref x, ref y);
            }

            return score;
        }

        /// <summary>
        /// Generates a series of possible solutions based on the generations and saves them to disk.
        /// The text files generated must contain a comma separated list of the max score, number of moves to display in the gui and all the actions robby will take (i.e the genes in the Chromosome).
        /// The top candidate of the 1st, 20th, 100, 200, 500 and 1000th generation will be saved.
        /// </summary>
        public void GeneratePossibleSolutions(string folderPath)
        {
            string result = "";
            while (true)
            {
                var count = GA.GenerationCount;
                // 1st, 20th, 100, 200, 500 and 1000th generation
                if (count == 1 || count == 20 || count == 100 || count == 200 || count == 500 || count == 1000)
                {
                    GenerationDetails gen = GA.CurrentGeneration as GenerationDetails;
                    gen.EvaluateFitnessOfPopulation();
                    string genes = "";
                    for (int j = 0; j < gen[0].Genes.Length; j++)
                    {
                        genes += gen[0].Genes[j];
                    }
                    result += gen.MaxFitness + "," + gen[0].Genes.Length + "," + genes + "\r\n";
                    FileWrittenEvent?.Invoke("Generation " + count + ": genes are written.");
                }
                filePercentDone((int)count);
                if (count == this.NumberOfGenerations)
                    break;
                else
                    GA.GenerateGeneration();
            }
            // Write string to file
            System.IO.File.WriteAllText(folderPath, result);
            string time = DateTime.Now.ToString("t");
            string date = DateTime.Now.ToString("d");
            FileWrittenEvent?.Invoke("File written at " + time + " on " + date + " in: " + folderPath);
        }

        /// <summary>
        /// Used to generate a single test grid filled with cans in random locations. Half of 
        /// the grid (rounded down) will be filled with cans. Use the GridSize to determine the size of the grid
        /// </summary>
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

        /// <summary>
        /// Used to shuffle the content of a grid
        /// </summary>
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

        /// <summary>
        /// Used to get status of file writing process in percentage
        /// </summary>
        private void filePercentDone(int count)
        {
            int percentComplete = (int)Math.Round((double)(100 * count) / this.NumberOfGenerations);
            if (percentComplete >= 25 && percentComplete < 50)
            {
                if (_percentHelper == 0)
                {
                    FileWrittenEvent?.Invoke("25% done!");
                    _percentHelper = 25;
                }
            }
            if (percentComplete >= 50 && percentComplete < 75)
            {
                if (_percentHelper == 25)
                {
                    FileWrittenEvent?.Invoke("50% done!");
                    _percentHelper = 50;
                }
            }
            if (percentComplete >= 75 && percentComplete < 100)
            {
                if (_percentHelper == 50)
                {
                    FileWrittenEvent?.Invoke("75% done!");
                    _percentHelper = 75;
                }
            }
            if (percentComplete == 100)
            {
                if (_percentHelper == 75)
                {
                    FileWrittenEvent?.Invoke("100% done!");
                    _percentHelper = 0;
                }
            }
        }
    }
}