using System;
using System.IO;
using System.Text;
using RobbyTheRobot;
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
            Console.WriteLine("Enter the number generations");
            string numberOfGenerations = Console.ReadLine();
            while (!numberOfGenerations.All(char.IsDigit) || numberOfGenerations == ""){
                Console.WriteLine("Invalid number generations, re-enter a number");
                numberOfGenerations = Console.ReadLine();
            }

            Console.WriteLine("Enter mutation rate (as a percentage EX: 5 = 5%)");
            string mutationRate = Console.ReadLine();
            while (!mutationRate.All(char.IsDigit) || mutationRate == "" || Int16.Parse(mutationRate) > 99 || Int16.Parse(mutationRate) < 1){
                Console.WriteLine("Invalid mutation rate, re-enter a number");
                mutationRate = Console.ReadLine();
            }
            double realMutationRate = Double.Parse(mutationRate) / 100;

            Console.WriteLine("Enter elite rate (as a percentage EX: 5 = 5%)");
            string eliteRate = Console.ReadLine();
            while (!eliteRate.All(char.IsDigit) || eliteRate == ""){
                Console.WriteLine("Invalid elite rate, re-enter a number");
                eliteRate = Console.ReadLine();
            }

            Console.WriteLine("Enter the population size");
            string populationSize = Console.ReadLine();
            while (!populationSize.All(char.IsDigit) || populationSize == ""){
                Console.WriteLine("Invalid population size, re-enter a number");
                populationSize = Console.ReadLine();
            }

            Console.WriteLine("Enter the number genes");
            string numberOfGenes = Console.ReadLine();
            while (!numberOfGenes.All(char.IsDigit) || numberOfGenes == ""){
                Console.WriteLine("Invalid number genes, re-enter a number");
                numberOfGenes = Console.ReadLine();
            }

            Console.WriteLine("Enter the length of gene");
            string lengthOfGene = Console.ReadLine();
            while (!lengthOfGene.All(char.IsDigit) || lengthOfGene == ""){
                Console.WriteLine("Invalid length of gene, re-enter a number");
                lengthOfGene = Console.ReadLine();
            }

            Console.WriteLine("Enter number of trials");
            string numberOfTrials = Console.ReadLine();
            while (!numberOfTrials.All(char.IsDigit) || numberOfTrials == ""){
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
                Robby.createRobby(
                    200,
                    1,
                    Int16.Parse(numberOfGenerations),
                    Int16.Parse(mutationRate),
                    Int16.Parse(eliteRate),
                    Int16.Parse(populationSize),
                    Int16.Parse(lengthOfGene),
                    Int16.Parse(numberOfGenes),
                    Int16.Parse(numberOfTrials),
                    Int16.Parse(seed)
                    ).GeneratePossibleSolutions(_filePath);
            }
            else{
                Console.WriteLine("Robby is being created!");
                Robby.createRobby(
                    200,
                    1,
                    Int16.Parse(numberOfGenerations),
                    realMutationRate,
                    Int16.Parse(eliteRate),
                    Int16.Parse(populationSize),
                    Int16.Parse(lengthOfGene),
                    Int16.Parse(numberOfGenes),
                    Int16.Parse(numberOfTrials)
                    ).GeneratePossibleSolutions(_filePath);
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
