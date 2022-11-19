using System;

namespace RobbyTheRobot
{
    public class Robby
    {
        public static IRobbyTheRobot createRobby(int numberOfActions, int numberOfTestGrids, int numberOfGenerations, double mutationRate, double eliteRate, int populationSize, int numberOfGenes, int lengthOfGene, int numberOfTrials, int gridSize = 10, int? seed = null){
            return new RobbyTheRobot(numberOfTestGrids, numberOfGenerations, mutationRate, eliteRate, populationSize, numberOfGenes, lengthOfGene, numberOfTrials, gridSize, seed, numberOfActions);
        }
    }
}
