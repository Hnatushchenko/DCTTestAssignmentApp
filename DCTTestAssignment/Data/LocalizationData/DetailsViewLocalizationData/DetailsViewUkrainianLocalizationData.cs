using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCTTestAssignment.Data.LocalizationData.DetailsViewLocalizationData;

public class DetailsViewUkrainianLocalizationData : IDetailsViewLocalizationData
{
    public string? Price => "Ціна";
    public string? Volume => "Обсяг";
    public string? Source => "Джерело";
    public string? Pairs => "Пари";
    public string? Confidence => "Довіра";
    public string? Markets => "Ринки";
}
