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

        public IGeneration CurrentGeneration { get; }

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
            Chromosome[] chromosomes  = new Chromosome[200];
            for (int i = 0; i < this.CurrentGeneration.NumberOfChromosomes; i++)
            {
                this.CurrentGeneration[i].Reproduce(this.CurrentGeneration[i+1]);
            }
            this.CurrentGeneration = 
        }
    }
}