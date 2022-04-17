using System;
using soilparams.Enums;
using soilparams.Models;

namespace soilparams
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputFile  = "input.json";
            string outputFile = "output.txt";
            if (!hasArguments(args))
            {
                Console.WriteLine("Usage: soilparams <input> <output>");
                Console.WriteLine("Trying to run with default values: input.json, output.txt\n");
                // return;
            }
            else
            {
                inputFile = args[0];
                outputFile = args[1];
            }

            var samples = new Experiment(inputFile);
            samples.CalculateParams();
        }

        private static bool hasArguments(string[] args)
        {
            if (args.Length != 2)
            {
                return false;
            }
            return true;
        }


    }
}
