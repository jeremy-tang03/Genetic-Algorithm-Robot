using System.Collections.Generic;
using System;

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

        public long GenerationCount { get; private set; }

        public int? Seed { get; }

        public IGeneration CurrentGeneration { get; private set; }

        public FitnessEventHandler FitnessCalculation { get; }

        public GeneticAlgorithm(int populationSize, int numberOfGenes, int lengthOfGene, double mutationRate, double eliteRate, int numberOfTrials, FitnessEventHandler fitnessCalculation, int? seed = null)
        {
            if (populationSize <= 0 || numberOfGenes <= 0 || lengthOfGene <= 0 || mutationRate < 0 || eliteRate < 0 || numberOfTrials < 0)
            {
                throw new ArgumentException();
            }
            else
            {
                this.PopulationSize = populationSize;
                this.NumberOfGenes = numberOfGenes;
                this.LengthOfGene = lengthOfGene;
                this.MutationRate = mutationRate;
                this.EliteRate = eliteRate;
                this.NumberOfTrials = numberOfTrials;
            }
            if (seed != null) // not sure if this check is necessary
            {
                this.Seed = (int)seed;
            }
            else
            {
                this.Seed = seed;
            }
            this.GenerationCount = 0;
            this.CurrentGeneration = GenerateGeneration(populationSize, numberOfGenes, lengthOfGene, mutationRate, eliteRate, numberOfTrials, fitnessCalculation, seed);
            this.FitnessCalculation = fitnessCalculation;
        }

        // TODO: this
        public IGeneration GenerateGeneration(int populationSize, int numberOfGenes, int lengthOfGene, double mutationRate, double eliteRate, int numberOfTrials, FitnessEventHandler fitnessCalculation, int? seed = null)
        {
            throw new System.NotImplementedException();
            // not sure how to create a generation using these parameters
        }

        public IGeneration GenerateGeneration()
        {
            if (this.GenerationCount == 0)
            {
                Chromosome[] chromosomes = new Chromosome[200];
                for (int i = 0; i < chromosomes.Length; i++)
                {
                    chromosomes[i] = new Chromosome(200, 7L, this.Seed);
                }
                return new GenerationDetails(chromosomes, this);
            }
            else
            {
                return GenerateNextGeneration();
            }
        }

        private IGeneration GenerateNextGeneration()
        {
            // TODO: check fitness?
            List<Chromosome> newChromosomesList = new List<Chromosome>();
            const int top = 20; // if we get the top 20, this is temporary
            for (int i = 0; i < this.CurrentGeneration.NumberOfChromosomes / 2; i++)
            {
                Chromosome spouse = (Chromosome)this.CurrentGeneration[(i+1)%(top-1)];
                // TODO: fix cast error
                Chromosome[] chromosomeChildren = this.CurrentGeneration[i % (top - 1)].Reproduce(spouse, this.MutationRate);
                newChromosomesList.Add(chromosomeChildren[0]);
                newChromosomesList.Add(chromosomeChildren[1]);
            }
            Chromosome[] newChromosomes = newChromosomesList.ToArray();
            IGeneration nextGeneration = GenerationDetails(newChromosomes, this);
            this.CurrentGeneration = nextGeneration;
            this.GenerationCount++;
            return nextGeneration;
        }
    }
}