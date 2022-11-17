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
            if (populationSize <= 0 || numberOfGenes <= 0 || lengthOfGene <= 0 || numberOfTrials < 0)
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

        private IGeneration GenerateNextGeneration()
        {
            List<Chromosome> newChromosomesList = new List<Chromosome>();
            int topAmount = 0;

            if (this._eliteRate != 0)
            {
                double tempTopAmount = (this._currentGeneration.NumberOfChromosomes * this._eliteRate) / 100;
                topAmount = (int)Math.Floor(tempTopAmount);
                for (int i = 0; i < topAmount; i++)
                    newChromosomesList.Add(new Chromosome(this._currentGeneration[i]));
            }

            GenerationDetails gen = this._currentGeneration as GenerationDetails;
            bool reproducing = true;
            while (reproducing)
            {
                if (newChromosomesList.Count == this._currentGeneration.NumberOfChromosomes)
                {
                    // Console.WriteLine("BREAK");
                    reproducing = false;
                    break;
                }
                Chromosome baseChromosome = (gen.SelectParent() as Chromosome);
                Chromosome spouse = (gen.SelectParent() as Chromosome);
                Chromosome[] chromosomeChildren = baseChromosome.Reproduce(spouse, this._mutationRate) as Chromosome[];
                if (newChromosomesList.Count == this._currentGeneration.NumberOfChromosomes - 1) // space for 1 new only
                {
                    // Console.WriteLine("SPACE FOR 1");
                    newChromosomesList.Add(chromosomeChildren[0]);
                }
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