using System.Collections.Generic;
using core.soilparams.Interfaces;

namespace core.soilparams.Models
{
    public class Sample : ISoilModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<int> PressureHeads { get; set; }
        public List<double> MeasuredWaterContents { get; set; }
        public List<double> PredictedWaterContents { get; set; }
        public List<string> Models { get; set; }
        public List<double> InitialGuess { get; set; }
        public BaseSoilModel chosenModel { get; set; }

        public string GetModelName()
        {
            if (chosenModel == null)
                return "Sem modelo definido";
            return this.chosenModel.Name;
        }

        public void SetModel(string model)
        {
            if (model.Equals("VG"))
            {
                this.chosenModel = new VanGenuchten(this, InitialGuess);
            }
            else if (model.Equals("BC"))
            {
                this.chosenModel = new BrooksAndCorey(this, InitialGuess);
            }
        }
    }
}