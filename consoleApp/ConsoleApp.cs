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
            if (!hasArguments(_args))
            {
                Console.WriteLine("Usage: soilparams <input>");
                Console.WriteLine("Trying to run with default value: input.json\n");
                // return;
            }
            else
            {
                inputFile  = _args[0];
            }
    
            var samples = new Experiment(inputFile);
            samples.CalculateParams();

            return 0;
        }
    
        private static bool hasArguments(string[] args)
        {
            if (args.Length != 1)
            {
                return false;
            }
            return true;
        }
        }
    
    }
    
    
    
    