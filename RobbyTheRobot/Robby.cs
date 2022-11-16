using System;

namespace RobbyTheRobot
{
    public class Robby
    {
        public static IRobbyTheRobot createRobby(int numberOfActions, int numberOfTestGrids, int numberOfGenerations, double mutationRate, double eliteRate, int populationSize, int numberOfTrials){
            return new RobbyTheRobot(numberOfActions, numberOfTestGrids, numberOfGenerations, mutationRate, eliteRate, populationSize, numberOfTrials);
        }
    }
}
