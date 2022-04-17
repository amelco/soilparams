using System.Collections.Generic;

namespace soilparams.Models
{
    public class VanGenuchten : BaseSoilModel
    {
        public VanGenuchten(Sample sample, List<double> initialGuess) : base("Van Genuchten", sample, initialGuess) { }
    } 
}