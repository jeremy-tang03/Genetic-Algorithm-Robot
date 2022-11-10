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
            if (seed != null) // not sure if this is necessary
            {
                this.Seed = (int)seed;
            }
            else
            {
                this.Seed = seed;
            }
            this.GenerationCount = 0;
            this.CurrentGeneration = GenerateGeneration();
            this.FitnessCalculation = fitnessCalculation;
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
            List<Chromosome> newChromosomesList = new List<Chromosome>();
            GenerationDetails gen = this.CurrentGeneration as GenerationDetails;
            for (int i = 0; i < this.CurrentGeneration.NumberOfChromosomes/2; i++)
            {
                Chromosome baseChromosome = (gen.SelectParent() as Chromosome);
                Chromosome spouse = (gen.SelectParent() as Chromosome);
                Chromosome[] chromosomeChildren = baseChromosome.Reproduce(spouse, this.MutationRate) as Chromosome[];
                newChromosomesList.Add(chromosomeChildren[0]);
                newChromosomesList.Add(chromosomeChildren[1]);
            }
            Chromosome[] newChromosomes = newChromosomesList.ToArray();
            IGeneration nextGeneration = new GenerationDetails(newChromosomes, this);
            this.CurrentGeneration = nextGeneration;
            this.GenerationCount++;
            return nextGeneration;
        }
    }
}