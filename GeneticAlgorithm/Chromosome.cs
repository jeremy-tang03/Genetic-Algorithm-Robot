using System.Diagnostics.CodeAnalysis;

namespace GeneticAlgorithm
{
    public class Chromosome : IChromosome
    {
        public int this[int index] {
            get{
                return Genes[index];
            }
        }

        public double Fitness { get; }

        public int[] Genes { get; }

        public long Length { get; }

        public int CompareTo([AllowNull] IChromosome other)
        {
            throw new System.NotImplementedException();
        }

        public IChromosome[] Reproduce(IChromosome spouse, double mutationProb)
        {
            throw new System.NotImplementedException();
        }
    }
}