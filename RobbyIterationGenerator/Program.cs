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
            IRobbyTheRobot _robby = getInputAndCreateRobby();

            string _filePath = getInputAndCreateFilePath();

            /*using (FileStream fs = File.Create(filePath))     
            {    
                // Add some text to file    
                byte[] robbyInformationByte = new UTF8Encoding(true).GetBytes(robbyInformation);        

                fs.Write(robbyInformationByte, 0, robbyInformationByte.Length);      
            }   */
        }

        private static IRobbyTheRobot getInputAndCreateRobby(){
            Console.WriteLine("Enter number of generations");
            string numberOfGenerations = Console.ReadLine();
            while (!numberOfGenerations.All(char.IsDigit) || numberOfGenerations == ""){
                Console.WriteLine("Invalid number of generation, re-enter a number");
                numberOfGenerations = Console.ReadLine();
            }

            Console.WriteLine("Enter the population size");
            string populationSize = Console.ReadLine();
            while (!populationSize.All(char.IsDigit) || populationSize == ""){
                Console.WriteLine("Invalid population size, re-enter a number");
                populationSize = Console.ReadLine();
            }

            Console.WriteLine("Enter number of trials");
            string numberOfTrials = Console.ReadLine();
            while (!numberOfTrials.All(char.IsDigit) || numberOfTrials == ""){
                Console.WriteLine("Invalid number of trials, re-enter a number");
                numberOfTrials = Console.ReadLine();
            }

            return Robby.createRobby(Int16.Parse(numberOfGenerations), Int16.Parse(populationSize), Int16.Parse(numberOfTrials), 20);

        }

        private static string getInputAndCreateFilePath(){
            Console.WriteLine("Please enter preferred file location, we suggest \"../Iterations/\"");
            string fileLocation = Console.ReadLine();

            DateTime now = DateTime.Now;
            string fileName = now.Year+"."+now.Month+"."+now.Day+"-"+now.Hour+"."+now.Minute+"."+now.Second+".txt";
            return fileLocation+fileName;
        }
    }
}
