using System;

namespace GeneticAlgorithm
{

    public class GenerationDetails : IGenerationDetails
    {

        private IChromosome[] chromosomes;
        private IGeneticAlgorithm iGenAlgorithm;
        private FitnessEventHandler fitnessHandle;
        private int? seed;


        public GenerationDetails(IGeneticAlgorithm iGenAlgorithm, FitnessEventHandler fitnessHandle, int? seed)
        {
            this.chromosomes = new IChromosome[iGenAlgorithm.PopulationSize];
            this.iGenAlgorithm = iGenAlgorithm;
            this.fitnessHandle = fitnessHandle;
            this.seed = seed;
        }

        public GenerationDetails(IChromosome[] chromosomes, GeneticAlgorithm genAlgorithm)
        {
            this.chromosomes = chromosomes;
            this.iGenAlgorithm = genAlgorithm;
            this.fitnessHandle = genAlgorithm.FitnessCalculation;
            this.seed = genAlgorithm.Seed;
        }
        

        public IChromosome[] Chromomsomes{
            get{
                return this.chromosomes;
            }
        }

        public IChromosome[] Chromomsomes
        {
            get
            {
                return this.chromosomes;
            }
        }

        public double AverageFitness
        {
            get
            {
                double fitnessSum = 0;

                for (int i = 0; i < chromosomes.Length; i++)
                {
                    fitnessSum += chromosomes[i].Fitness;
                }

                return fitnessSum / chromosomes.Length;
            }
        }

        public double MaxFitness
        {
            get
            {
                double maxFitness = chromosomes[0].Fitness;

                foreach (IChromosome chrom in chromosomes)
                {
                    if (chrom.Fitness > maxFitness)
                    {
                        maxFitness = chrom.Fitness;
                    }
                }

                return maxFitness;
            }
        }


        public long NumberOfChromosomes
        {

            get
            {
                return this.chromosomes.Length;
            }
        }

        public IChromosome this[int index]
        {

            get
            {
                return this.chromosomes[index];
            }
        }

        //IGenerationDetails
        public IChromosome SelectParent()
        {

            //Sort first to first 20
            //Random random = new Random[20]; Randomly select based on the these.
            //Select a bunch randomly and get the highest out of that -- Alternative
            // double[] sortedArray = new double[200];

            // IChromosome[] sorted = sortArray();

            if (chromosomes.Length > 1)
            {
                chromosomes = sortArray();
                System.Random random = new System.Random();
                int num = 0;
                if (chromosomes.Length >= 20)
                {
                    int thirdOfPop = (int)Math.Ceiling((decimal)chromosomes.Length / 3);
                    num = random.Next(thirdOfPop*2); // 2/3 of pop
                }
                else
                {
                    num = random.Next(chromosomes.Length);
                }
                // Console.WriteLine(num);

                // return sorted[num];
                return chromosomes[num];
            }
            else
                return chromosomes[0];
        }


        public void EvaluateFitnessOfPopulation()
        {

            // Fire num of trials for average avlue for a chromosome

            //Compute the fitness
            foreach (Chromosome chrom in chromosomes)
            {
                double averageFitness = 0;
                double temp = fitnessHandle(chrom, this);
                // Console.WriteLine("Temp: "+temp);

                chrom.Fitness = temp;
                // Console.WriteLine("Fitness in chrom: "+chrom.Fitness);

                if (iGenAlgorithm.NumberOfTrials > 1)
                {

                    for (int i = 0; i < iGenAlgorithm.NumberOfTrials; i++)
                    {
                        averageFitness = averageFitness + fitnessHandle(chrom, this);
                    }

                    chrom.Fitness = (averageFitness / iGenAlgorithm.NumberOfTrials);

                }
            }

            //Sorting the values from descending order
            chromosomes = sortArray();

        }

        //Helper Method to sort the array
        private IChromosome[] sortArray()
        {
            IChromosome[] sorted = chromosomes;

            int bestFitness;
            for (int i = 0; i < sorted.Length; i++)
            {
                bestFitness = i;

                for (int j = i; j < sorted.Length; j++)
                {
                    if (sorted[i].Fitness < sorted[j].Fitness)
                    {

                        IChromosome temp = sorted[i];
                        sorted[i] = sorted[j];
                        sorted[j] = temp;
                    }

                }
            }
            return sorted;

        }
    }

}
