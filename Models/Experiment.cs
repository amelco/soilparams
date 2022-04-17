using System;
using System.Collections.Generic;

namespace soilparams.Models
{
    public class Experiment
    {
        public List<Sample> Samples { get; set; }

        public Experiment(string inputFile)
        {
            Samples = getSampleList(inputFile);
        }

        private List<Sample> getSampleList(string inputFile)
        {
            return System.Text.Json.JsonSerializer.Deserialize<List<Sample>>(System.IO.File.ReadAllText(inputFile));
        }

        public void CalculateParams()
        {
            foreach (Sample sample in Samples)
            {
                Console.WriteLine(sample.Title);
                foreach (var model in sample.Models)
                {
                    sample.SetModel(model);
                    Console.WriteLine($"Calculating parameters from model '{sample.GetModelName()}'");
                    var soilParams = sample.chosenModel.GetSoilParameters();
                    foreach (var param in soilParams)
                    {
                        Console.WriteLine(param.Key + ": " + param.Value);
                    }
                    Console.WriteLine("");
                }
                Console.WriteLine("");
            }
        }
    }
}