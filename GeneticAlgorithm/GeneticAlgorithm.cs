using System.Collections.Generic;

namespace GeneticAlgorithm
{
    public class GeneticAlgorithm : IGeneticAlgorithm
    {
        public int PopulationSize { get; }

        public int NumberOfGenes { get; }

        public int LengthOfGene { get; }

        public double MutationRate { get; }

        public double EliteRate { get; }

        public int NumberOfTrials { get; }

        public long GenerationCount { get; }

        public int? Seed { get; }

        public IGeneration CurrentGeneration { get; private set; }

        public FitnessEventHandler FitnessCalculation  { get; }

        public GeneticAlgorithm(int populationSize, int numberOfGenes, int lengthOfGene, double mutationRate, double eliteRate, int numberOfTrials, FitnessEventHandler fitnessCalculation, int? seed = null)
        {
            this.PopulationSize = populationSize;
            this.NumberOfGenes = numberOfGenes;
            this.LengthOfGene = lengthOfGene;
            this.MutationRate = mutationRate;
            this.EliteRate = EliteRate;
            this.NumberOfTrials = numberOfTrials;
            this.FitnessCalculation = fitnessCalculation;
            this.Seed = seed;
            // return GenerateGeneration(populationSize, numberOfGenes, lengthOfGene, mutationRate, eliteRate, numberOfTrials, fitnessCalculation, seed);
        }

        public IGeneration GenerateGeneration(int populationSize, int numberOfGenes, int lengthOfGene, double mutationRate, double eliteRate, int numberOfTrials, FitnessEventHandler fitnessCalculation, int? seed = null)
        {
            throw new System.NotImplementedException();
        }

        public IGeneration GenerateGeneration()
        {
            if(this.GenerationCount == 0){
                Chromosome[] chromosomes = new Chromosome[200];
                for (int i = 0; i < chromosomes.Length; i++)
                {
                    chromosomes[i] = new Chromosome(200, 7L, this.Seed);
                }
                return new GenerationDetails(chromosomes, this, this.Seed);
            } else {
                return GenerateNextGeneration();
            }
        }

        private IGeneration GenerateNextGeneration()
        {
            // TODO: check fitness
            List<Chromosome> newChromosomesList = new List<Chromosome>();
            const int top = 20; // if top parents count is 20
            for (int i = 0; i < this.CurrentGeneration.NumberOfChromosomes/2; i++)
            {
                Chromosome spouse = (Chromosome) this.CurrentGeneration[i]; //this.CurrentGeneration[(i+1)%(top-1)]
                Chromosome[] chromosomeChildren = this.CurrentGeneration[i%(top-1)].Reproduce(spouse, this.MutationRate);
                newChromosomesList.Add(chromosomeChildren[0]);
                newChromosomesList.Add(chromosomeChildren[1]);
            }
            Chromosome[] newChromosomes  = newChromosomesList.ToArray();
            IGeneration nextGeneration = GenerationDetails(newChromosomes, this, this.Seed);
            this.CurrentGeneration = nextGeneration;
            return nextGeneration;
        }
    }
}