using Microsoft.VisualStudio.TestTools.UnitTesting;
using RobbyTheRobot;
using System;

namespace Tests
{
    [TestClass]
    public class RobbyTheRobotTests
    {
        [TestMethod]
        public void GenerateRandomTestGridTest(){
            ContentsOfGrid[,] grid = Robby.createRobby(1, 100, 1, null).GenerateRandomTestGrid();

            int canCount = 0;
            int emptyCount = 0;
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
    }
}