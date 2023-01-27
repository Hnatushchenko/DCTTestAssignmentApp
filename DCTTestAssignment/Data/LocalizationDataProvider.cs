using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCTTestAssignment.Data;

public class LocalizationDataProvider<TLocalizationData> : ILocalizationDataProvider<TLocalizationData>
{
    public TLocalizationData GetLocalizationData(string language)
    {
        var targetType = GetType().Assembly.GetTypes()
            .Where(type => type.IsClass)
            .Where(type => type.Name.EndsWith($"{language}LocalizationData"))
            .Where(type => type.IsAssignableTo(typeof(TLocalizationData))).Single();

        try
        {
            return (TLocalizationData)Activator.CreateInstance(targetType);
        }
        catch (InvalidCastException)
        {
            throw new NotSupportedException($"{language} language is not supported.");
        }
    }
}
