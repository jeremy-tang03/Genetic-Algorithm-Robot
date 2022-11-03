namespace GeneticAlgorithm{
    public class GenerationDetails : IGenerationDetails {

        private IChromosome[] chromosome = new IChromosome[];
        private IGeneticAlgorithm ig;
        private FitnessEventHandler feh;
        private string seed;


        public GenerationDetails(IGeneticAlgorithm ig, FitnessEventHandler feh, string seed){
            this.ig = ig;
            this.feh = feh;
            this.seed = seed;
        }

        public GenerationDetails(GenerationDetails gd){
            this.ig = gd.ig;
            this.feh = gd.feh;
            this.seed = gd.seed;
        }
        
    }

}
