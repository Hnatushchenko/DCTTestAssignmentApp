using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCTTestAssignment.Data.LocalizationData.HomeLocalizationData;

public class HomeViewEnglishLocalizationData : IHomeViewLocalizationData
{
    public string? Name => "Name";
    public string? Price => "Price";
    public string? TotalVolume => "Total Volume";
    public string? Search => "Search";

    public string? Ath => "All-time high";

    public string? AthDate => "Ath date";
}
