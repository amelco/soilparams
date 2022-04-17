using System.Collections.Generic;

namespace soilparams.Interfaces
{
    public interface ICalculate
    {
        Dictionary<string,double> GetSoilParameters();
    }
}