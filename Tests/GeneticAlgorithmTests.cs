using Microsoft.VisualStudio.TestTools.UnitTesting;
using GA = GeneticAlgorithm;
using System;

namespace Tests
{
    [TestClass]
    public class GeneticAlgorithmTests
    {
        [TestMethod]
        public void TestConstructorValidParams()
        {
            GA.GeneticAlgorithm algo = new GA.GeneticAlgorithm(1, 10, 7, 0.5, 10, 2, null, 100);
            GA.Chromosome[] chroms = { new GA.Chromosome(algo.NumberOfGenes, algo.LengthOfGene, algo.Seed) };
            GA.GeneticAlgorithm algo1 = new GA.GeneticAlgorithm(1, 10, 7, 0.5, 10, 2, null, 100);
            GA.Chromosome[] chroms1 = { new GA.Chromosome(algo1.NumberOfGenes, algo1.LengthOfGene, algo1.Seed) };

            Assert.AreEqual(1, algo.PopulationSize);
            Assert.AreEqual(10, algo.NumberOfGenes);
            Assert.AreEqual(7, algo.LengthOfGene);
            Assert.AreEqual(0.5, algo.MutationRate);
            Assert.AreEqual(10, algo.EliteRate);
            Assert.AreEqual(2, algo.NumberOfTrials);
            Assert.AreEqual(null, algo.FitnessCalculation);
            Assert.AreEqual(100, algo.Seed);
            CollectionAssert.AreEqual(algo.CurrentGeneration[0].Genes, algo1.CurrentGeneration[0].Genes);
        }

        [TestMethod]
        public void TestConstructorInvalidParamsArgumentException()
        {
            GA.GeneticAlgorithm algo;
            try
            {
                algo = new GA.GeneticAlgorithm(-1, 10, 7, 0.5, 10, 2, null, 100); // incorrect population size
                Assert.Fail(); // If it gets to this line, no exception was thrown
            }
            catch (ArgumentException) { }
            try
            {
                algo = new GA.GeneticAlgorithm(10, -1, 7, 0.5, 10, 2, null, 100); // incorrect num of genes
                Assert.Fail(); // If it gets to this line, no exception was thrown
            }
            catch (ArgumentException) { }
            try
            {
                algo = new GA.GeneticAlgorithm(10, 10, -1, 0.5, 10, 2, null, 100); // incorrect len of genes
                Assert.Fail(); // If it gets to this line, no exception was thrown
            }
            catch (ArgumentException) { }
            try
            {
                algo = new GA.GeneticAlgorithm(10, 10, 7, 100, 10, 2, null, 100); // incorrect mutation rate
                Assert.Fail(); // If it gets to this line, no exception was thrown
            }
            catch (ArgumentException) { }
            try
            {
                algo = new GA.GeneticAlgorithm(10, 10, 7, 0.5, 10000, 2, null, 100); // incorrect elite rate
                Assert.Fail(); // If it gets to this line, no exception was thrown
            }
            catch (ArgumentException) { }
            try
            {
                algo = new GA.GeneticAlgorithm(10, 10, 7, 0.5, 10, 0, null, 100); // incorrect num of trials
                Assert.Fail(); // If it gets to this line, no exception was thrown
            }
            catch (ArgumentException) { }
        }

        [TestMethod]
        public void TestGenerateGeneration()
        {
            GA.GeneticAlgorithm algo = new GA.GeneticAlgorithm(5, 10, 7, 0.5, 0, 2, null, 100);
            long chromCount = algo.CurrentGeneration.NumberOfChromosomes;
            int[] gen1Genes = algo.CurrentGeneration[0].Genes;
            algo.GenerateGeneration();
            Assert.AreEqual(2, algo.GenerationCount);
            Assert.AreEqual(chromCount, algo.CurrentGeneration.NumberOfChromosomes);
            CollectionAssert.AreNotEqual(gen1Genes, algo.CurrentGeneration[0].Genes);
        }

        [TestMethod]
        public void TestGenerateGenerationFitness()
        {
            GA.GeneticAlgorithm algo = new GA.GeneticAlgorithm(50, 10, 7, 0.5, 0, 2, delegateReturn, 100);
            (algo.CurrentGeneration as GA.GenerationDetails).EvaluateFitnessOfPopulation();
            // for (int i = 0; i < algo.CurrentGeneration.NumberOfChromosomes; i++)
            // {
            //     Console.Write(algo.CurrentGeneration[i].Fitness);
            //     Console.WriteLine();
            // }
            var gen1Fitness = algo.CurrentGeneration.AverageFitness;
            Console.WriteLine(algo.GenerationCount + ": " + gen1Fitness);
            for (int i = 0; i < 10; i++)
            {
                algo.GenerateGeneration();
            }
            (algo.CurrentGeneration as GA.GenerationDetails).EvaluateFitnessOfPopulation();
            // for (int i = 0; i < algo.CurrentGeneration.NumberOfChromosomes; i++)
            // {
            //     Console.Write(algo.CurrentGeneration[i].Fitness);
            //     Console.WriteLine();
            // }
            var gen11Fitness = algo.CurrentGeneration.AverageFitness;
            Console.WriteLine(algo.GenerationCount + ": " + gen11Fitness);
            Assert.AreNotEqual(gen1Fitness, gen11Fitness);
        }

        [TestMethod]
        public void TestGenerateGenerationEliteRate()
        {
            GA.GeneticAlgorithm algo = new GA.GeneticAlgorithm(50, 10, 7, 0.5, 100, 1, null, 100); //eliteRate = 100
            Random rand = new Random();
            int num = rand.Next(algo.PopulationSize);
            var gen1 = algo.CurrentGeneration;
            // Array.ForEach(gen1[20].Genes, Console.Write); Console.WriteLine();
            for (int i = 0; i < 10; i++)
            {
                algo.GenerateGeneration();
            }
            var gen11 = algo.CurrentGeneration;
            // Array.ForEach(gen11[20].Genes, Console.Write); Console.WriteLine();
            CollectionAssert.AreEqual(gen1[num].Genes, gen11[num].Genes);
        }

        //Returns method for the delegate for testing purposes
        private double delegateReturn(GA.IChromosome chrom, GA.IGeneration gen)
        {
            Random r = new Random();
            return r.Next(10);
        }
    }
}