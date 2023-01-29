using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCTTestAssignment.Data.LocalizationData.DetailsViewLocalizationData;

public class DetailsViewEnglishLocalizationData : IDetailsViewLocalizationData
{
    public string Price => "Price";
    public string Volume => "Volume";
    public string Source => "Source";
    public string Pairs => "Pairs";
    public string Confidence => "Confidence";
    public string Markets => "Markets";
}
