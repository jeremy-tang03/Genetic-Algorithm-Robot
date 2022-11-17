using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeneticAlgorithm;

namespace Tests
{
    [TestClass]
    public class GenerationDetailsTests
    {
        // [TestMethod]
        // public void GenerationDetailsConstr()
        // {
        //     IChromosome chromosome = new Chromosome();
        //     IGeneration generation = new GenerationDetails();
        //     FitnessEventHandler fitnessHandle;
            
        //     GenerationDetails genD = new GenerationDetails();
        //     int[] correctGenes = {6,1,4,6,2,6,4,4,2,1};
        //     CollectionAssert.AreEqual(chromosome.Genes, correctGenes);
        // }

        // [TestMethod]
        // public void GenerationDetailsConstructor()
        // {
        //     IChromosome chromosome = new Chromosome(10,20,10);
        //     GenerationDetails genD = new GenerationDetails();
        //     FitnessEventHandler fitnessHandle = (chromosome, genD) => {
        //         return 10;
        //     }
            // IGeneticAlgorithm iGenAlg = new GeneticAlgorithm(200, 2, 4, 2, 1, 4, fitnessHandle, 20)

            // using hh = GeneticAlgorithm.GeneticAlgorithm;

            // GeneticAlgorithm gnn = new GeneticAlgorithm();
     
        // }


        [TestMethod]
        public void GenerationDetailsAverageFitness(){
            
            IChromosome[] chromosomes = FillChromosomes();

            GenerationDetails genD = new GenerationDetails(chromosomes);
            Assert.AreEqual(genD.AverageFitness, 1);
        }


        [TestMethod]
        public void GenerationDetailsSelectParent(){
            IChromosome[] chromosomes = FillChromosomes();

            GenerationDetails genD = new GenerationDetails(chromosomes);

            IChromosome chromParent = genD.SelectParent();
            Assert.AreEqual(chromParent.Fitness, 1);
        }


        [TestMethod]
        public void GenerationDetailsEvaluateFitnessOfPopulation(){

            IChromosome[] chromosomes = FillChromosomes();
            FitnessEventHandler feh = (IChromosome chrom, IGeneration gen) => {
                return 2;

            };
            GeneticAlgorithm.GeneticAlgorithm genA = new GeneticAlgorithm.GeneticAlgorithm(3,3,3,6,6,6, feh, null);
        
            GenerationDetails genD = new GenerationDetails(chromosomes, genA, feh);
            IChromosome[] chrom = genD.Chrom;

            Assert.AreEqual(chrom[0].Fitness, 2);
        }

        [TestMethod]
        public void SortTest(){
            IChromosome[] chromosomes = FillChromosomes();

            GenerationDetails genD = new GenerationDetails(chromosomes);

            IChromosome chromParent = genD.SelectParent();
            IChromosome[] chrms = genD.Chrom;
        
            Assert.AreEqual(chrms[99].Fitness, 100);
        }
        

        [TestMethod]
        public void TestTest(){

            IChromosome[] chromosomes = new IChromosome[200];
          

          

            Chromosome cc = new Chromosome(3,3,3);
            Chromosome ca = new Chromosome(3,3,3);

            cc.Fitness = 100;
            ca.Fitness = 0;
            
            chromosomes[0] = cc;
            chromosomes[1] = ca;
            

            GenerationDetails genD = new GenerationDetails(chromosomes);

            Assert.AreEqual(genD.AverageFitness, 50);
            // Assert.AreEqual(genD.Chrom.Length, 200);
            // Assert.AreEqual(cc.Fitness, 100);

        }

        [TestMethod]
        public void GenerationDetailsNumberOfChromosomes()
        {

            IChromosome[] chromosomes = FillChromosomes();

            GenerationDetails genD = new GenerationDetails(chromosomes);

            Assert.AreEqual(200, genD.NumberOfChromosomes);
        }
     




        public IChromosome[] FillChromosomes(){
            Chromosome[] chromosomes = new Chromosome[200];
            for(int i = 0; i <chromosomes.Length; i++){
                chromosomes[i] = new Chromosome((i+2),(i+10),3);
                // chromosomes[i].Fitness = i;
            }

            return chromosomes;
        }
       

    }
}
