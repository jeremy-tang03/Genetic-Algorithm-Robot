using System;

namespace RobbyTheRobot
{
    public class Robby
    {
        public static IRobbyTheRobot createRobby(int numberOfGenerations, int populationSize, int numberOfTrials, int? seed){
            return new RobbyTheRobot(numberOfGenerations, populationSize, numberOfTrials, seed);
        }
    }
}
