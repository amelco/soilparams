using System.Collections.Generic;
using soilparams.Interfaces;
using soilparams.Models;

namespace soilparams.Models
{
    public class Sample : ISoilModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<int> PressureHeads { get; set; }
        public List<double> MeasuredWaterContents { get; set; }
        public List<string> Models { get; set; }
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
                this.chosenModel = new VanGenuchten();
            }
            else if (model.Equals("BC"))
            {
                this.chosenModel = new BrooksAndCorey();
            }
        }
    }
}