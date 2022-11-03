using System;
using System.Diagnostics.CodeAnalysis;

namespace GeneticAlgorithm
{
    public class Chromosome : IChromosome
    {
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

        public double Fitness { get; set; }

        public int[] Genes { get; }

        public long Length { get; }

        public Chromosome(int numOfGenes, long lengthOfGene, int? seed){
            Length = lengthOfGene;
            Genes = new int[numOfGenes];

            if (seed is null)
            {
                Random random = new Random();
                for (int i = 0; i < Genes.Length; i++)
                {
                    Genes[i] = random.Next(0, 7);
                }
            }
            else
            {
                Random random = new Random((int)seed);
                for (int i = 0; i < Genes.Length; i++)
                {
                    Genes[i] = random.Next(0, 7);
                }
            }
        }

        public Chromosome(int[] genes, long lengthOfGene){
            Length = lengthOfGene;
            Genes = genes;
        }

        public Chromosome(IChromosome chromosome) {
            Fitness = chromosome.Fitness;
            Length = chromosome.Length;
            Genes = new int[chromosome.Genes.Length];

            int i = 0;
            foreach (int gene in chromosome.Genes)
            {
                Genes[i] = chromosome.Genes[i];
                i++;
            }
        }

        public int CompareTo([AllowNull] IChromosome other)
        {
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

        public IChromosome[] Reproduce(IChromosome spouse, double mutationProb)
        {
                Random random = new Random();
                //No matter what we want it to have children which are slightly different
                //Maybe review this
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

                
                Chromosome[] chromosomeChildren = {new Chromosome(child1Genes, Length), new Chromosome(child2Genes, Length)};
                
                return chromosomeChildren;
        }
    }
}