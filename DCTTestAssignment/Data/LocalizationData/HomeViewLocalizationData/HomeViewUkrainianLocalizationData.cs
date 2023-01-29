using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCTTestAssignment.Data.LocalizationData.HomeLocalizationData;

public class HomeViewUkrainianLocalizationData : IHomeViewLocalizationData
{
    public string Name => "Назва";
    public string Price => "Ціна";
    public string TotalVolume => "Загальний обсяг торгів";
    public string Search => "Шукати";
    public string Ath => "Історичний максимум";
    public string AthDate => "Дата максимуму";
}
