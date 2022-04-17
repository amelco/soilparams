using System.Collections.Generic;
using soilparams.Interfaces;

namespace soilparams.Models
{
    public class BaseSoilModel : ICalculate
    {
        public string Name { get; set; }
        public Sample sample { get; set; }

        public BaseSoilModel(string name)
        {
            Name = name;
        }

        public virtual Dictionary<string, double> GetSoilParameters()
        {
            Dictionary<string, double> soilParameters = new Dictionary<string, double>();
            soilParameters.Add("alpha", 1.2);
            soilParameters.Add ("n", 1.5);
            soilParameters.Add("ThetaR", 0.142);
            soilParameters.Add("ThetaS", 0.491);
            return soilParameters;
        }
    }
}