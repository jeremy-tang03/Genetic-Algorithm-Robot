using Microsoft.VisualStudio.TestTools.UnitTesting;
using RobbyTheRobot;

namespace Tests
{
    [TestClass]
    public class RobbyTheRobotTests
    {
        [TestMethod]
        public void GenerateRandomTestGridTest(){
            Robby.createRobby(1, 100, 1, null);
        }
    }
}