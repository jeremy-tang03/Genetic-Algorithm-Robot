namespace GeneticAlgorithm{

    public class GenerationDetails : IGenerationDetails {

        private IChromosome[] chromosomes = new IChromosome[200];
        private IGeneticAlgorithm iGenAlgorithm;
        private FitnessEventHandler fitnessHandle;
        private int? seed;


        public GenerationDetails(IGeneticAlgorithm iGenAlgorithm, FitnessEventHandler fitnessHandle, int? seed){
            this.iGenAlgorithm = iGenAlgorithm;
            this.fitnessHandle = fitnessHandle;
            this.seed = seed;
        }

        // public GenerationDetails(GenerationDetails gd){
        //     this.iGenAlgorithm = gd.iGenAlgorithm;
        //     this.fitnessHandle = gd.fitnessHandle;
        //     this.seed = gd.seed;
        // }

        public GenerationDetails(IChromosome[] chromosomes, GeneticAlgorithm genAlgorithm){
            this.chromosomes = chromosomes;
            this.iGenAlgorithm = genAlgorithm;
            this.fitnessHandle = genAlgorithm.FitnessCalculation;
            this.seed = genAlgorithm.seed;
        }


        public double AverageFitness{
            get{
                double fitnessSum = 0;

                for(int i=0; i< chromosomes.Length; i++){
                    fitnessSum = fitnessSum + chromosomes[i].Fitness;
                }

                return fitnessSum/200;
            }
        }

        public double MaxFitness{
            get{
                double maxFitness = chromosomes[0].Fitness;

                foreach(IChromosome chrom in chromosomes){
                    if(chrom.Fitness > maxFitness){
                        maxFitness = chrom.Fitness;
                    }
                }

                return maxFitness;
            }
        }


        public long NumberOfChromosomes{

            get{
                return this.chromosomes.Length;
            }
        }

        public IChromosome this[int index]{

            get{
                return this[index];
            }
        }

        //IGenerationDetails
        public IChromosome SelectParent(){

            //Sort first to first 20
            //Random random = new Random[20]; Randomly select based on the these.
            //Select a bunch randomly and get the highest out of that -- Alternative
            // double[] sortedArray = new double[200];

            // IChromosome[] sorted = sortArray();
    
            chromosomes = sortArray();
            System.Random random = new System.Random();
            int num = random.Next(31);
    
            // return sorted[num];
            return chromosomes[num];

        }

        public void EvaluateFitnessOfPopulation(){
            
            // Fire num of trials for average avlue for a chromosome

            //Compute the fitness
            foreach(Chromosome chrom in chromosomes){
                
                chrom.Fitness = fitnessHandle(chrom, this);
                
                if(iGenAlgorithm.NumberOfTrials > 1){
                    chrom.Fitness = iGenAlgorithm.NumberOfTrials;
                }
            }


            //Sorting the values from descending order
            chromosomes = sortArray();
            
        }

        private IChromosome[] sortArray(){
            //Sorts all the chromosomes based on best fitness

            IChromosome[] sorted = new IChromosome[200];
            int bestFitness = 0;
            for(int i =0; i<chromosomes.Length; i++){

                bestFitness = i;

                for(int j = i; j<chromosomes.Length;j++){

                    if(chromosomes[i].Fitness < chromosomes[j].Fitness){

                        bestFitness = j;
                    }
                }
                sorted[i] = chromosomes[bestFitness]; 
            }

            return sorted;
        }
        
    }

}
