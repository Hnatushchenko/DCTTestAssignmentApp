using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCTTestAssignment.Data.LocalizationData.HomeLocalizationData;

public interface IHomeViewLocalizationData
{
    public string? Name { get; }
    public string? Price { get; }
    public string? TotalVolume { get; }
    public string? Search { get; }
}
