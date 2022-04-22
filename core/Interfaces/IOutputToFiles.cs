using core.soilparams.Enums;
using core.soilparams.Models;

namespace core.soilparams.Interfaces
{
    public interface IOutputToFiles
    {
        void WriteToFile(Sample sample, OutputFileType outputType);
    }
}