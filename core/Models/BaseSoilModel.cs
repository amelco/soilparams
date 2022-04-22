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
        public double MeasuredStandardDeviation { get; set; }
        public double MeasuredStandardError { get; set; }
        public double PredictedStandardDeviation { get; set; }
        public double PredictedStandardError { get; set; }
        public double Correlation { get; set;}
        public double Rsquared { get; private set; }

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

            Func<DoubleVector, double, double> fdelegate = delegate( DoubleVector p, double h )
            {
                if ( p.Length != 4 )
                {
                    throw new InvalidArgumentException( "Incorrect number of function parameters: " + p.Length );
                }
                return p[0] + (p[1] - p[0]) / Math.Pow(1 + Math.Pow(p[3]*h, p[2]), 1-1/p[2]);
            };
            
            var fitter = new OneVariableFunctionFitter<TrustRegionMinimizer>(fdelegate);

            DoubleVector parameters = fitter.Fit( xValues, yValues, start );

            SoilParameters = new Dictionary<string, double>
            {
                { "ThetaR", parameters[0] },
                { "ThetaS", parameters[1] },
                { "alpha",  parameters[2] },
                { "n",      parameters[3] }
            };

            CalculatePredictedWaterContents(fdelegate);

            MeasuredStandardDeviation = NMathFunctions.StandardDeviation(yValues);
            MeasuredStandardError     = MeasuredStandardDeviation / Math.Sqrt(sample.MeasuredWaterContents.Count);
            SoilParameters.Add("Standard deviation (measured values)", MeasuredStandardDeviation);
            SoilParameters.Add("Standard error (measured values)",     MeasuredStandardError);

            var predictedWaterContents = new DoubleVector(sample.PredictedWaterContents.Select(x => (double)x).ToArray());
            PredictedStandardDeviation = NMathFunctions.StandardDeviation(predictedWaterContents);
            PredictedStandardError     = PredictedStandardDeviation / Math.Sqrt(sample.PredictedWaterContents.Count);
            SoilParameters.Add("Standard deviation (predicted values)", PredictedStandardDeviation);
            SoilParameters.Add("Standard error (predicted values)",     PredictedStandardError);

            Correlation = NMathFunctions.Correlation(yValues, predictedWaterContents);
            Rsquared = new GoodnessOfFit(fitter, xValues, yValues, parameters).RSquared;

            return SoilParameters;
        }

        public void CalculatePredictedWaterContents(Func<DoubleVector, double, double> fdelegate)
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