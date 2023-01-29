using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCTTestAssignment.Data.LocalizationData.ConvertViewLocalizationData;

public class ConvertViewEnglishLocalizationData : IConvertViewLocalizationData
{
    public string From => "From";

    public string To => "To";

    public string Amount => "Amount";

    public string Convert => "Convert";

    public string CalculatedAmount => "CalculatedAmount";

    public string Loading => "Loading...";

    public string TimeoutMessage => "Response timeout expired. Sorry!";

    public string CoinNotFoundMessage => "Coin, that you are looking for, is not found";

    public string NotFound => "Not found";
}
