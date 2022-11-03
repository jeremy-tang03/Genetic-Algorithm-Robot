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

        public IGeneration CurrentGeneration { get; }

        public FitnessEventHandler FitnessCalculation  { get; }

        public GeneticAlgorithm(int populationSize, int numberOfGenes, int lengthOfGene, double mutationRate, double eliteRate, int numberOfTrials, FitnessEventHandler fitnessCalculation, int? seed = null)
        {
            throw new System.NotImplementedException();
            //return GenerateGeneration(populationSize, numberOfGenes, lengthOfGene, mutationRate, eliteRate, numberOfTrials, fitnessCalculation, seed);
        }

        public IGeneration GenerateGeneration(int populationSize, int numberOfGenes, int lengthOfGene, double mutationRate, double eliteRate, int numberOfTrials, FitnessEventHandler fitnessCalculation, int? seed = null)
        {
            throw new System.NotImplementedException();
        }

        public IGeneration GenerateGeneration()
        {
            if(this.GenerationCount == 0){
                Chromosome[] chromosomes = new Chromosome[200];
                for (int i = 0; i < chromosome.Length; i++)
                {
                    chromosomes[i] = new Chromosome(200, 7L, null);
                }
                return new Generation(chromosomes);
            } else {
                return GenerateNextGeneration();
            }
        }

        private IGeneration GenerateNextGeneration()
        {
            throw new System.NotImplementedException();
        }
    }
}