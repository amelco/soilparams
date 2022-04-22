using System;
using System.Collections.Generic;
using System.IO;
using core.soilparams.Enums;
using core.soilparams.Interfaces;

namespace core.soilparams.Models
{
    public class Experiment : IOutputToFiles
    {
        public List<Sample> Samples { get; set; }
        public string  InputFile { get; set; }
        private string statsContent = "";

        public Experiment(string inputFile)
        {
            InputFile  = inputFile;
            Samples = getSampleList(InputFile);
        }

        private List<Sample> getSampleList(string inputFile)
        {
            var result = new List<Sample>();
            try
            {
                result = System.Text.Json.JsonSerializer.Deserialize<List<Sample>>(System.IO.File.ReadAllText(inputFile));
            }
            catch (Exception e)
            {
                Console.WriteLine("Error reading input file.\n" + e.Message);
            }
            return result;
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
                    this.WriteToFile(sample, OutputFileType.CSV);
                }
                Console.WriteLine("");
            }
        }

        public void WriteToFile(Sample sample, OutputFileType outputType)
        {
            if (outputType == OutputFileType.CSV)
            {
                string fileName = $"{sample.Title} - {sample.GetModelName()}.csv";
                using (StreamWriter file = new(fileName))
                {
                    file.WriteLine("PressureHead,MeasuredWaterContent,PredictedWaterContent,difference");
                    for (int i = 0; i < sample.PressureHeads.Count; i++)
                    {
                        double difference = sample.MeasuredWaterContents[i] - sample.PredictedWaterContents[i];
                        file.WriteLine($"{sample.PressureHeads[i]},{sample.MeasuredWaterContents[i]},{sample.PredictedWaterContents[i]},{difference}");
                    }
                }
            }

            statsContent += 
$@" -== {sample.Title} ==-
Description: {sample.Description}
Model:       {sample.GetModelName()}

# MEASURED
  Standard Deviation: {sample.chosenModel.MeasuredStandardDeviation}
  Standard Error:     {sample.chosenModel.MeasuredStandardError}

# PREDICTED
  Standard Deviation: {sample.chosenModel.PredictedStandardDeviation}
  Standard Error:     {sample.chosenModel.PredictedStandardError}

# Pearson correlation coefficient: {sample.chosenModel.Correlation}
# R-Squared:                       {sample.chosenModel.Rsquared}

";

        File.WriteAllText("statistics.txt", statsContent);
        }
    }
}