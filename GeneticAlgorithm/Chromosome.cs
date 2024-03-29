using System;
using System.Diagnostics.CodeAnalysis;

namespace GeneticAlgorithm
{
    public class Chromosome : IChromosome
    {
        private long _length;
        private double _fitness;
        public int this[int index] {
            get
            {
                return Genes[index];
            }
            private set 
            {
                Genes[index] = value;
            }
        }

        public double Fitness { 
            get 
            {
                return _fitness;
            }
            set 
            {
                _fitness = value;
                FitnessIsInitialized = true;
            }
        }
        public bool FitnessIsInitialized { get; private set; } = false;
        private int[] _genes;
        public int[] Genes => _genes;

        public long Length { 
            get{
                return _length;
            }
        
        }

        public Chromosome(int numOfGenes, long lengthOfGene, int? seed){
            // Length = lengthOfGene;
            _length = lengthOfGene;

            _genes = new int[numOfGenes];

            if (seed is null)
            {
                Random random = new Random();
                for (int i = 0; i < Genes.Length; i++)
                {
                    Genes[i] = random.Next(0, (int)Length);
                }
            }
            else
            {
                Random random = new Random((int)seed);
                for (int i = 0; i < Genes.Length; i++)
                {
                    Genes[i] = random.Next(0, (int)Length);
                }
            }
        }

        public Chromosome(int[] genes, long lengthOfGene){
            // Length = lengthOfGene;
            _length = lengthOfGene;
            _genes = genes;
        }

        public Chromosome(IChromosome chromosome) {
            Fitness = chromosome.Fitness;
            // Length = chromosome.Length;
            _length = chromosome.Length;
            _genes = new int[chromosome.Genes.Length];

            int i = 0;
            foreach (int gene in chromosome.Genes)
            {
                Genes[i] = gene;
                i++;
            }
        }

        public int CompareTo([AllowNull] IChromosome other)
        {
            if ((other.FitnessIsInitialized == false) || (FitnessIsInitialized == false)){
                return 0;
            }
            
            if (Fitness < other.Fitness)
            {
                return -1;
            }        
            else if (Fitness > other.Fitness)
            {
                return 1;
            }
            else 
            {
                return 0;
            }
        }

        /// <summary>
        /// Uses a crossover function to create two offspring, then iterates through the
        /// two child Chromosomes genes, changing them to random values according to the mutation rate.
        /// </summary>
        /// <param name="spouse">The Chromosome to reproduce with</param>
        /// <param name="mutationProb">The rate of mutation needs to be a decimal between 0.01 - 0.99</param>
        /// <returns></returns>
        public IChromosome[] Reproduce(IChromosome spouse, double mutationProb)
        {
                Random random = new Random();
                int lowerBound = random.Next(0, Genes.Length-1);   
                int upperBound = random.Next(lowerBound+1, Genes.Length);    

                int[] child1Genes = new int[Genes.Length];
                int[] child2Genes = new int[Genes.Length];

                for (int i = 0; i < lowerBound; i++)
                {
                    child1Genes[i] = this.Genes[i];
                    child2Genes[i] = spouse.Genes[i];
                }
                for (int i = lowerBound; i <= upperBound; i++)
                {
                    child1Genes[i] = spouse.Genes[i];
                    child2Genes[i] = this.Genes[i];
                }
                for (int i = upperBound+1; i < Genes.Length; i++)
                {
                    child1Genes[i] = this.Genes[i];
                    child2Genes[i] = spouse.Genes[i];
                }

                double chanceOfMutation = 1 / mutationProb;
                int upperBoundMutation = (int)Math.Round(chanceOfMutation);
                for (int i = 0; i < child1Genes.Length; i++)
                {
                    int randNumber = random.Next(0, upperBoundMutation);
                    if (randNumber == 0)
                    {
                        child1Genes[i] = random.Next(0, (int)Length);
                        child2Genes[i] = random.Next(0, (int)Length);
                    }
                }
                
                Chromosome[] chromosomeChildren = {new Chromosome(child1Genes, Length), new Chromosome(child2Genes, Length)};
                
                return chromosomeChildren;
        }
    }
}