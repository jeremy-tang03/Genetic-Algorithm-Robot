using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeneticAlgorithm;

namespace Tests
{
    [TestClass]
    public class GenerationDetailsTests
    {

        [TestMethod]
        public void GenerationDetailsMaxFitness(){
            IChromosome[] chromosomes = FillChromosomes();
            GeneticAlgorithm.GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm.GeneticAlgorithm(1,2,3,4,5,6, delegateReturn, null);
            GenerationDetails genD = new GenerationDetails(chromosomes, geneticAlgorithm);

            Assert.AreEqual(genD.MaxFitness, 199);

        }

        [TestMethod]
        public void GenerationDetailsAverageFitness(){
            
            IChromosome[] chromosomes = FillChromosomes();
            GeneticAlgorithm.GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm.GeneticAlgorithm(1,2,3,4,5,6, delegateReturn, null);

            GenerationDetails genD = new GenerationDetails(chromosomes, geneticAlgorithm);
            Assert.AreEqual(genD.AverageFitness, 99.5);
        }



        [TestMethod]
        public void GenerationDetailsNumberOfChromosomes()
        {

            IChromosome[] chromosomes = FillChromosomes();
            GeneticAlgorithm.GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm.GeneticAlgorithm(1,2,3,4,5,6, delegateReturn, null);

            GenerationDetails genD = new GenerationDetails(chromosomes, geneticAlgorithm);

            Assert.AreEqual(200, genD.NumberOfChromosomes);
        }

        [TestMethod]
        public void GenerationDetailsSelectParent(){
            IChromosome[] chromosomes = FillChromosomes();
            GeneticAlgorithm.GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm.GeneticAlgorithm(1,2,3,4,5,6, delegateReturn, null);

            GenerationDetails genD = new GenerationDetails(chromosomes, geneticAlgorithm);

            IChromosome chromParent = genD.SelectParent();

            if(chromParent.Fitness < 169){
                Assert.Fail();
            }
            else{
                Assert.AreEqual(1,1);
            }


            // Assert.AreEqual(chromParent.Fitness, 1);
        }


        [TestMethod]
        public void GenerationDetailsEvaluateFitnessOfPopulation(){

            IChromosome[] chromosomes = FillChromosomes();
            FitnessEventHandler feh = (IChromosome chrom, IGeneration gen) => {
                return 6;

            };
            GeneticAlgorithm.GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm.GeneticAlgorithm(3,3,3,6,6,3, feh, null);
        
            GenerationDetails genD = new GenerationDetails(geneticAlgorithm, delegateReturn, null);

            genD.EvaluateFitnessOfPopulation();
            IChromosome[] chrom = genD.Chromomsomes;

            Assert.AreEqual(chrom[0].Fitness, 6);
        }



        //Returns method for the delegate
        private double delegateReturn(IChromosome chrom, IGeneration gen){
            return 2;
        }

        //Helper Method to fill the Chromosome Array
        private IChromosome[] FillChromosomes(){
            Chromosome[] chromosomes = new Chromosome[200];
            for(int i = 0; i <chromosomes.Length; i++){
                chromosomes[i] = new Chromosome((i+1),(i+2),3);
                chromosomes[i].Fitness = i;
            }

            return chromosomes;
        }
       

    }
}
