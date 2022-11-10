namespace RobbyTheRobot
{
    internal class RobbyTheRobot : IRobbyTheRobot
    {
        public int NumberOfActions { get; }

        public int NumberOfTestGrids { get; }

        public int GridSize { get; }

        public int NumberOfGenerations { get; }

        public double MutationRate { get; }

        public double EliteRate { get; }

        public RobbyTheRobot(int numberOfGenerations, int populationSize, int numberOfTrials, int? seed){
            this.NumberOfGenerations = numberOfGenerations;
            //not sure if this is right
            this.NumberOfActions = numberOfTrials;
            // not sure for populationSize
            //not sure for seed
        }

        public void GeneratePossibleSolutions(string folderPath)
        {
            throw new System.NotImplementedException();
        }

        public ContentsOfGrid[,] GenerateRandomTestGrid()
        {
            throw new System.NotImplementedException();
        }
    }
}