using Microsoft.VisualStudio.TestTools.UnitTesting;
using RobbyTheRobot;
using System;

namespace Tests
{
    [TestClass]
    public class RobbyTheRobotTests
    {
        [TestMethod]
        public void TestConstructorValidParams(){
            var r = Robby.createRobby(1, 2, 3, 0.4, 4.5, 5, 6, 7, 8);
            Assert.AreEqual(1, r.NumberOfActions);
            Assert.AreEqual(2, r.NumberOfTestGrids);
            Assert.AreEqual(3, r.NumberOfGenerations);
            Assert.AreEqual(0.4, r.MutationRate);
            Assert.AreEqual(4.5, r.EliteRate);
        }

        [TestMethod]
        public void TestConstructorInvalidParamsArgumentException(){
            try
            {
                var r = Robby.createRobby(-1, 2, 3, 0.4, 4.5, 5, 6, 7, 8); // incorrect num of actions
                Assert.Fail(); // If it gets to this line, no exception was thrown
            }
            catch (ArgumentException) { }
            try
            {
                var r = Robby.createRobby(1, -2, 3, 0.4, 4.5, 5, 6, 7, 8); // incorrect num of test grids
                Assert.Fail(); // If it gets to this line, no exception was thrown
            }
            catch (ArgumentException) { }
            try
            {
                var r = Robby.createRobby(1, 2, -3, 0.4, 4.5, 5, 6, 7, 8); // incorrect num of generations
                Assert.Fail(); // If it gets to this line, no exception was thrown
            }
            catch (ArgumentException) { }

            // the rest of the params are also handled in geneticAlgorithm
            try
            {
                var r = Robby.createRobby(1, 2, 3, 400, 4.5, 5, 6, 7, 8); // incorrect mutation rate
                Assert.Fail(); // If it gets to this line, no exception was thrown
            }
            catch (ArgumentException) { }
            try
            {
                var r = Robby.createRobby(1, 2, 3, 0.4, 450, 5, 6, 7, 8); // incorrect elite rate
                Assert.Fail(); // If it gets to this line, no exception was thrown
            }
            catch (ArgumentException) { }
            try
            {
                var r = Robby.createRobby(1, 2, 3, 0.4, 4.5, -5, 6, 7, 8); // incorrect population size
                Assert.Fail(); // If it gets to this line, no exception was thrown
            }
            catch (ArgumentException) { }
            try
            {
                var r = Robby.createRobby(1, 2, 3, 0.4, 4.5, 5, -6, 7, 8); // incorrect num of genes
                Assert.Fail(); // If it gets to this line, no exception was thrown
            }
            catch (ArgumentException) { }
            try
            {
                var r = Robby.createRobby(1, 2, 3, 0.4, 4.5, 5, 6, -7, 8); // incorrect len of genes
                Assert.Fail(); // If it gets to this line, no exception was thrown
            }
            catch (ArgumentException) { }
            try
            {
                var r = Robby.createRobby(1, 2, 3, 0.4, 4.5, 5, 6, 7, -8); // incorrect num of trials
                Assert.Fail(); // If it gets to this line, no exception was thrown
            }
            catch (ArgumentException) { }
        }

        [TestMethod]
        public void GenerateRandomTestGridTest(){
            ContentsOfGrid[,] grid = Robby.createRobby(1, 1, 1, 1, 1, 5, 10, 7, 1).GenerateRandomTestGrid();

            int canCount = 0;
            int emptyCount = 0;
            Console.Write("Grid for visualization: \n");
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] == ContentsOfGrid.Can)
                    {
                        canCount++;
                    } else {
                        emptyCount++;
                    }
                    Console.Write("{0} ", grid[i, j]);
                }
                Console.Write("\n");
            }
            Console.WriteLine("cans: " + canCount + ", empty: " + emptyCount + ", grid area: " + grid.Length);
            Assert.AreEqual(canCount, grid.Length/2);
            Assert.AreEqual(emptyCount, grid.Length/2);
        }

        [TestMethod]
        public void TestGeneratePossibleSolutions(){
            string folderPath = "..\\test.txt";
            Robby.createRobby(1, 1, 100, 0.5, 0, 50, 250, 7, 1).GeneratePossibleSolutions(folderPath);
            string readText = System.IO.File.ReadAllText(folderPath);
            Console.WriteLine(readText);
            Assert.AreNotEqual("", readText);
        }
    }
}