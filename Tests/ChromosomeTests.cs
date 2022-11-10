using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeneticAlgorithm;

namespace Tests
{
    [TestClass]
    public class ChromosomeTests
    {
        [TestMethod]
        public void ChromosomeGeneSeed()
        {
            Chromosome chromosome = new Chromosome(10, 7, 100);
            int[] correctGenes = {6,1,4,6,2,6,4,4,2,1};
            CollectionAssert.AreEqual(chromosome.Genes, correctGenes);
        }

        [TestMethod]
        public void ChromosomeDeepClone()
        {
            Chromosome chromosome1 = new Chromosome(10, 7, 100);
            Chromosome chromosome2 = new Chromosome(chromosome1);

            Assert.IsFalse(chromosome1.Equals(chromosome2));
            CollectionAssert.Equals(chromosome1.Genes, chromosome2.Genes);
            Assert.AreEqual(chromosome1.Length, chromosome2.Length);
        }

        [TestMethod]
        public void ChromosomeComparePosOne()
        {
            Chromosome chromosome1 = new Chromosome(10, 7, 100);
            Chromosome chromosome2 = new Chromosome(10, 7, 99);
            chromosome1.Fitness = 1;
            chromosome2.Fitness = 2;

            Assert.AreEqual(chromosome1.CompareTo(chromosome2), -1);
        }
        
        [TestMethod]
        public void ChromosomeCompareNegOne()
        {
            Chromosome chromosome1 = new Chromosome(10, 7, 100);
            Chromosome chromosome2 = new Chromosome(10, 7, 99);
            chromosome1.Fitness = 2;
            chromosome2.Fitness = 1;

            Assert.AreEqual(chromosome1.CompareTo(chromosome2), 1);
        }

        [TestMethod]
        public void ChromosomeCompareZeroEqual()
        {
            Chromosome chromosome1 = new Chromosome(10, 7, 100);
            Chromosome chromosome2 = new Chromosome(10, 7, 99);
            chromosome1.Fitness = 2;
            chromosome2.Fitness = 2;

            Assert.AreEqual(chromosome1.CompareTo(chromosome2), 0);
        }

       [TestMethod]
        public void ChromosomeCompareZeroEqualNull()
        {
            Chromosome chromosome1 = new Chromosome(10, 7, 100);
            Chromosome chromosome2 = new Chromosome(10, 7, 99);
            chromosome1.Fitness = 2;

            Assert.AreEqual(chromosome1.CompareTo(chromosome2), 0);
        }
    }
}
