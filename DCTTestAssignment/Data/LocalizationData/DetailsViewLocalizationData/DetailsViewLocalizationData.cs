using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCTTestAssignment.Data.LocalizationData.DetailsViewLocalizationData;

public interface IDetailsViewLocalizationData
{
    public string Price { get; }
    public string Volume { get; }
    public string Source { get; }
    public string Pairs { get; }   
    public string Confidence { get; }
    public string Markets { get; }
}
