using System.Collections.Generic;

namespace core.soilparams.Models
{
    public class BrooksAndCorey : BaseSoilModel
    {
        public BrooksAndCorey(Sample sample, List<double> initialGuess) : base("Brooks and Corey", sample, initialGuess) {}

        public override Dictionary<string, double> GetSoilParameters()
        {
            Dictionary<string, double> soilParameters = new Dictionary<string, double>();
            soilParameters.Add("Ksat", 0.0);
            soilParameters.Add("Porosity", 0.0);
            soilParameters.Add("FieldCapacity", 0.0);
            soilParameters.Add("WiltingPoint", 0.0);
            soilParameters.Add("BulkDensity", 0.0);
            soilParameters.Add("PoreSizeDistribution", 0.0);
            return soilParameters;
        }
    }
}