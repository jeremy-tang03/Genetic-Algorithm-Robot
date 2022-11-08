namespace GeneticAlgorithm{
    public class GenerationDetails : IGenerationDetails {

        private IChromosome[] chromosome = new IChromosome[200];
        private IGeneticAlgorithm iGenAlgorithm;
        private FitnessEventHandler fitnessHandle;
        private string seed;


        public GenerationDetails(IGeneticAlgorithm iGenAlgorithm, FitnessEventHandler fitnessHandle, string seed){
            this.iGenAlgorithm = iGenAlgorithm;
            this.fitnessHandle = fitnessHandle;
            this.seed = seed;
        }

        public GenerationDetails(GenerationDetails gd){
            this.iGenAlgorithm = gd.iGenAlgorithm;
            this.fitnessHandle = gd.fitnessHandle;
            this.seed = gd.seed;
        }



        public double AverageFitness{
            get{
                double fitnessSum = 0;

                for(int i=0; i< chromosome.Length; i++){
                    fitnessSum = fitnessSum + chromosome[i].Fitness;
                }

                return fitnessSum/200;
            }
        }

        public double MaxFitness{
            get{
                double maxFitness = chromosome[0].Fitness;

                foreach(IChromosome chrom in chromosome){
                    if(chrom.Fitness > maxFitness){
                        maxFitness = chrom.Fitness;
                    }
                }

                return maxFitness;
            }
        }


        public long NumberOfChromosomes{

            get{
                return this.chromosome.Length;
            }
        }

        public IChromosome this[int index]{

            get{
                return this[index];
            }
        }

        //IGenerationDetails
        // public IChromosome SelectParent(){

            //Sort first to first 20
            //Random random = new Random[20]; Randomly select based on the these.

            // double bestFitness = chromosome[0].Fitness;
            //200 ->> 300

            // double oldFitness;

            // for(int i =0; i<chromosome.Length; i++){
            //     double bestFitness = chromosome[i].Fitness;
                
            //     for(int j=i; j<chromosome.Length;j++){
            //         if(bestFitness < chromosome[j].Fitness){
            //             bestFitness = chromosome[j].Fitness;
                        
            //         }
            //     }

            // }
        // }

        // public  void EvaluateFitnessOfPopulation(){

        // }
        
    }

}
