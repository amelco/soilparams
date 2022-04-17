using System;
using soilparams.Models;

namespace soilparams
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!hasArguments(args))
            {
                Console.WriteLine("Usage: soilparams <input> <output>");
                return;
            }
            string inputFile = args[0];
            string outputFile = args[1];

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
