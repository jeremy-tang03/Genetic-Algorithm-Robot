using Microsoft.VisualStudio.TestTools.UnitTesting;
using GA = GeneticAlgorithm;

namespace Tests
{
    [TestClass]
    public class GeneticAlgorithmTests
    {
        [TestMethod]
        public void TestConstructorValidParams(){
            GA.GeneticAlgorithm algo = new GA.GeneticAlgorithm(200, 200, 200, 5, 10, 2, null, null);
            Assert.AreEqual(algo.PopulationSize, 200);
            Assert.AreEqual(algo.NumberOfGenes, 200);
            Assert.AreEqual(algo.LengthOfGene, 200);
            Assert.AreEqual(algo.MutationRate, 5);
            Assert.AreEqual(algo.EliteRate, 10);
            Assert.AreEqual(algo.NumberOfTrials, 2);
            Assert.AreEqual(algo.FitnessCalculation, null);
            Assert.AreEqual(algo.Seed, null);
        }

        [TestMethod]
        public void TestConstructorInvalidParams(){
            
        }
    }
}