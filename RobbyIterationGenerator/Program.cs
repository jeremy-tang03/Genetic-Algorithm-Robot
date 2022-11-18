using System;
using System.IO;
using System.Text;
using RobbyTheRobot;
using System.Diagnostics;
using System.Linq;

namespace RobbyIterationGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            getInputAndCreateRobby();
        }

        private static void getInputAndCreateRobby(){
            try{
            var timer = new Stopwatch();
            Console.WriteLine("Enter the number generations");
            string numberOfGenerations = Console.ReadLine();
            while (!numberOfGenerations.All(char.IsDigit) || numberOfGenerations == "" || Int16.Parse(numberOfGenerations) < 1){
                Console.WriteLine("Invalid number generations, re-enter a number");
                numberOfGenerations = Console.ReadLine();
            }

            Console.WriteLine("Enter mutation rate (EX: 5 = 5%)");
            string mutationRate = Console.ReadLine();
            while (mutationRate == "" || Double.Parse(mutationRate) > 100 || Double.Parse(mutationRate) < 0){
                Console.WriteLine("Invalid mutation rate, re-enter a number (input will be converted to %)");
                mutationRate = Console.ReadLine();
            }
            double realMutationRate = Double.Parse(mutationRate) / 100;

            Console.WriteLine("Enter elite rate (EX: 5 = 5%)");
            string eliteRate = Console.ReadLine();
            while (!eliteRate.All(char.IsDigit) || eliteRate == "" || Double.Parse(eliteRate) < 0 || Double.Parse(eliteRate) > 100){
                Console.WriteLine("Invalid elite rate, re-enter a number");
                eliteRate = Console.ReadLine();
            }

            Console.WriteLine("Enter the population size");
            string populationSize = Console.ReadLine();
            while (!populationSize.All(char.IsDigit) || populationSize == "" || Int16.Parse(populationSize) < 1){
                Console.WriteLine("Invalid population size, re-enter a number");
                populationSize = Console.ReadLine();
            }

            Console.WriteLine("Enter the number of genes");
            string numberOfGenes = Console.ReadLine();
            while (!numberOfGenes.All(char.IsDigit) || numberOfGenes == "" || Int16.Parse(numberOfGenes) < 220){
                Console.WriteLine("Invalid number genes, re-enter a number (please enter 220 or more)");
                numberOfGenes = Console.ReadLine();
            }

            Console.WriteLine("Enter the length of gene (7)");
            string lengthOfGene = Console.ReadLine();
            while (!lengthOfGene.All(char.IsDigit) || lengthOfGene == "" || Int16.Parse(lengthOfGene) < 0 || Int16.Parse(lengthOfGene) > 7){
                Console.WriteLine("Invalid length of gene, re-enter a number");
                lengthOfGene = Console.ReadLine();
            }

            Console.WriteLine("Enter number of trials");
            string numberOfTrials = Console.ReadLine();
            while (!numberOfTrials.All(char.IsDigit) || numberOfTrials == "" || Int16.Parse(numberOfTrials) < 1){
                Console.WriteLine("Invalid number of trials, re-enter a number");
                numberOfTrials = Console.ReadLine();
            }

            string _filePath = getInputAndCreateFilePath();

            Console.WriteLine("Do you want a seed? (y or n)");
            string answer = Console.ReadLine();
            while (answer != "y" && answer != "n")
            {
                Console.WriteLine("Invalid invalid answer, re-enter y or n");
                answer = Console.ReadLine();
            }
            if (answer == "y")
            {
                Console.WriteLine("What is the seed that you want?");
                string seed = Console.ReadLine();
                while (!seed.All(char.IsDigit) || seed == ""){
                    Console.WriteLine("Invalid seed, re-enter a number");
                    seed = Console.ReadLine();
                }

                Console.WriteLine("Robby is being created!");
                timer.Start();
                Robby.createRobby(
                    200,
                    1,
                    Int16.Parse(numberOfGenerations),
                    realMutationRate,
                    Double.Parse(eliteRate),
                    Int16.Parse(populationSize),
                    Int16.Parse(numberOfGenes),
                    Int16.Parse(lengthOfGene),
                    Int16.Parse(numberOfTrials),
                    10,
                    Int16.Parse(seed)
                    ).GeneratePossibleSolutions(_filePath);
                timer.Stop();
            }
            else{
                Console.WriteLine("Robby is being created!");
                timer.Start();
                Robby.createRobby(
                    200,
                    1,
                    Int16.Parse(numberOfGenerations),
                    realMutationRate,
                    Int16.Parse(eliteRate),
                    Int16.Parse(populationSize),
                    Int16.Parse(numberOfGenes),
                    Int16.Parse(lengthOfGene),
                    Int16.Parse(numberOfTrials)
                    ).GeneratePossibleSolutions(_filePath);
                timer.Stop();
            }
                // Get the elapsed time as a TimeSpan value.
                TimeSpan ts = timer.Elapsed;

                // Format and display the TimeSpan value.
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}s",
                ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                Console.WriteLine("RunTime " + elapsedTime);
            } catch (Exception e) {
                Console.WriteLine("Error: " + e);
            }
        }

        private static string getInputAndCreateFilePath(){
            Console.WriteLine("Please enter preferred file location, we suggest \"../Iterations/\"");
            string fileLocation = Console.ReadLine();

            /*DateTime now = DateTime.Now;
            string fileName = now.Year+"."+now.Month+"."+now.Day+"-"+now.Hour+"."+now.Minute+"."+now.Second+".txt";*/
            return fileLocation+"geneticAlgorithm.txt";
        }
    }
}
