using System.Collections.Generic;

namespace core.soilparams.Interfaces
{
    public interface ICalculate
    {
        Dictionary<string,double> GetSoilParameters();
    }
}