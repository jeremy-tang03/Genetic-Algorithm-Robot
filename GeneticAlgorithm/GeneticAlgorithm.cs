using System.Collections.Generic;
using System;

namespace GeneticAlgorithm
{
    public class GeneticAlgorithm : IGeneticAlgorithm
    {
        private int _populationSize;
        private int _numberOfGenes;
        private int _lengthOfGene;
        private double _mutationRate;
        private double _eliteRate;
        private int _numberOfTrials;
        private long _generationCount;
        private int? _seed;
        private IGeneration _currentGeneration;
        private FitnessEventHandler _fitnessCalculation;

        public int PopulationSize => _populationSize;
        public int NumberOfGenes => _numberOfGenes;
        public int LengthOfGene => _lengthOfGene;
        public double MutationRate => _mutationRate;
        public double EliteRate => _eliteRate;
        public int NumberOfTrials => _numberOfTrials;
        public long GenerationCount => _generationCount;
        public int? Seed => _seed;
        public IGeneration CurrentGeneration => _currentGeneration;
        public FitnessEventHandler FitnessCalculation => _fitnessCalculation;

        public GeneticAlgorithm(int populationSize, int numberOfGenes, int lengthOfGene, double mutationRate, double eliteRate, int numberOfTrials, FitnessEventHandler fitnessCalculation, int? seed = null)
        {
            if (populationSize <= 0 || numberOfGenes <= 0 || lengthOfGene <= 0 || numberOfTrials < 1 || mutationRate < 0 ||
            mutationRate > 1 || eliteRate < 0 || eliteRate > 100 || populationSize <= 0)
            {
                throw new ArgumentException();
            }
            else
            {
                this._populationSize = populationSize;
                this._numberOfGenes = numberOfGenes;
                this._lengthOfGene = lengthOfGene;
                this._mutationRate = mutationRate;
                this._eliteRate = eliteRate;
                this._numberOfTrials = numberOfTrials;
                this._seed = seed;
            }
            this._generationCount = 0;
            this._fitnessCalculation = fitnessCalculation;
            this._currentGeneration = GenerateGeneration();
        }

        /// <summary>
        /// Generates a generation for the given parameters. If no generation has been created the initial one will be constructed. 
        /// If a generation has already been created, it will provide the next generation.
        /// </summary>
        public IGeneration GenerateGeneration()
        {
            if (this._generationCount == 0)
            {
                Chromosome[] chromosomes = new Chromosome[this._populationSize];
                for (int i = 0; i < chromosomes.Length; i++)
                    chromosomes[i] = new Chromosome(this._numberOfGenes, this._lengthOfGene, this._seed);
                this._generationCount++;
                return new GenerationDetails(chromosomes, this);
            }
            else
            {
                return GenerateNextGeneration();
            }
        }

        /// <summary>
        /// Provides the next generation by evaluating the fitness and selecting parents.
        /// Also takes into consideration the elite rate.
        /// </summary>
        private IGeneration GenerateNextGeneration()
        {
            GenerationDetails gen = this._currentGeneration as GenerationDetails;
            gen.EvaluateFitnessOfPopulation();
            List<Chromosome> newChromosomesList = new List<Chromosome>();
            int topAmount = 0;
            if (this._eliteRate > 0)
            {
                // count of chromosomes picked by elite rate
                double tempTopAmount = (this._currentGeneration.NumberOfChromosomes * this._eliteRate) / 100;
                topAmount = (int)Math.Floor(tempTopAmount);
                for (int i = 0; i < topAmount; i++)
                    newChromosomesList.Add(new Chromosome(this._currentGeneration[i]));
            }

            while (true)
            {
                if (newChromosomesList.Count == this._currentGeneration.NumberOfChromosomes)
                {
                    break;
                }
                Chromosome baseChromosome = (gen.SelectParent() as Chromosome);
                Chromosome spouse = (gen.SelectParent() as Chromosome);
                Chromosome[] chromosomeChildren = baseChromosome.Reproduce(spouse, this._mutationRate) as Chromosome[];
                // space for 1 new only
                if (newChromosomesList.Count == this._currentGeneration.NumberOfChromosomes - 1)
                {
                    newChromosomesList.Add(chromosomeChildren[0]);
                }
                // space for 2+
                else
                {
                    newChromosomesList.Add(chromosomeChildren[0]);
                    newChromosomesList.Add(chromosomeChildren[1]);
                }
            }
            Chromosome[] newChromosomes = newChromosomesList.ToArray();
            IGeneration nextGeneration = new GenerationDetails(newChromosomes, this);
            this._currentGeneration = nextGeneration;
            this._generationCount++;
            return nextGeneration;
        }
    }
}