using System;
using core.soilparams.Models;

namespace soilparams.consoleApp
{
    public class ConsoleApp
    {
        readonly string[] _args;
        public ConsoleApp(string[] args)
        {
            _args = args;
        }
        public int Run()
        {
            string inputFile  = "input.json";
            string outputFile = "output.txt";
            if (!hasArguments(_args))
            {
                Console.WriteLine("Usage: soilparams <input> <output>");
                Console.WriteLine("Trying to run with default values: input.json, output.txt\n");
                // return;
            }
            else
            {
                inputFile  = _args[0];
                outputFile = _args[1];
            }
    
            var samples = new Experiment(inputFile);
            samples.CalculateParams();

            return 0;
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
    
    
    
    