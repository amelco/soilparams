using System;
using System.Collections.Generic;
using System.Linq;
using CenterSpace.NMath.Core;
using soilparams.Interfaces;

namespace soilparams.Models
{
    public class BaseSoilModel : ICalculate
    {
        public string Name { get; set; }
        public Sample sample { get; set; }
        public List<double> InitialGuess { get; set; }

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
            
            Dictionary<string, double> soilParameters = new Dictionary<string, double>();
            soilParameters.Add("ThetaR", solution[0]);
            soilParameters.Add("alpha",  solution[1]);
            soilParameters.Add("ThetaS", solution[2]);
            soilParameters.Add ("n",     solution[3]);
            return soilParameters;
        }
    }
}