using System;
using System.Collections.Generic;
using System.Linq;
using CenterSpace.NMath.Core;
using core.soilparams.Interfaces;

namespace core.soilparams.Models
{
    public class BaseSoilModel : ICalculate
    {
        public string Name { get; set; }
        public Sample sample { get; set; }
        public List<double> InitialGuess { get; set; }
        public Dictionary<string, double> SoilParameters { get; set; }


        public BaseSoilModel(string name, Sample sample, List<double> initialGuess)
        {
            Name = name;
            this.sample = sample;
            InitialGuess = initialGuess;
        }

        // BaseModel assumes van Genuchten as default model
        public virtual Dictionary<string, double> GetSoilParameters()
        {
            var xValues = new DoubleVector(sample.PressureHeads.Select(x => (double)x).ToArray());
            var yValues = new DoubleVector(sample.MeasuredWaterContents.Select(x => (double)x).ToArray());
            var start   = new DoubleVector(InitialGuess.ToArray());

            Func<DoubleVector, double, double> fdelegate = delegate( DoubleVector p, double x )
            {
                if ( p.Length != 4 )
                {
                    throw new InvalidArgumentException( "Incorrect number of function parameters to ThreeParameterExponential: " + p.Length );
                }
                return p[0] + (p[1] - p[0]) / Math.Pow(1 + Math.Pow(p[3]*x, p[2]), 1-1/p[2]);
            };
            
            var fitter = new OneVariableFunctionFitter<TrustRegionMinimizer>(fdelegate);

            DoubleVector solution = fitter.Fit( xValues, yValues, start );
            
            SoilParameters = new Dictionary<string, double>();
            SoilParameters.Add("ThetaR", solution[0]);
            SoilParameters.Add("alpha",  solution[1]);
            SoilParameters.Add("ThetaS", solution[2]);
            SoilParameters.Add ("n",     solution[3]);

            CalculatePredictedWaterContents(fdelegate);

            return SoilParameters;
        }

        private void CalculatePredictedWaterContents(Func<DoubleVector, double, double> fdelegate)
        {
            var predictedWaterContents = new List<double>();
            foreach (var pressureHead in sample.PressureHeads)
            {
                predictedWaterContents.Add(fdelegate(new DoubleVector(SoilParameters.Values.ToArray()), (double)pressureHead));
            }
            sample.PredictedWaterContents = predictedWaterContents;
        }
    }
}