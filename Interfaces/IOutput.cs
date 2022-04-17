using System.Collections.Generic;
using soilparams.Enums;
using soilparams.Models;

namespace soilparams.Interfaces
{
    public interface IOutput
    {
        void WriteToFile(Sample sample, OutputFileType outputType);
    }
}