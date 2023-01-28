using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCTTestAssignment.Data.LocalizationData.ConvertViewLocalizationData;

public interface IConvertViewLocalizationData
{
    string From { get; }
    string To { get; }
    string Amount { get; }
    string Convert { get; }
    string CalculatedAmount { get; }
    string Loading { get; }
    string TimeoutMessage { get; }
    string CoinNotFoundMessage { get; }
}
