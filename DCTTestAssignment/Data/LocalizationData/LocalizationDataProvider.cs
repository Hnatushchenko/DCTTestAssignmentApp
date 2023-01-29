using DCTTestAssignment.Data.LocalizationData.DetailsViewLocalizationData;
using DCTTestAssignment.Data.ThemeSupport.ThemeData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace DCTTestAssignment.Data.LocalizationData;

public class LocalizationDataProvider<TLocalizationData> : ILocalizationDataProvider<TLocalizationData>
{
    private Dictionary<string, TLocalizationData> _viewLocalizationData = new Dictionary<string, TLocalizationData>();
    public LocalizationDataProvider()
    {
        var localizationDataTypes = Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => typeof(TLocalizationData).IsAssignableFrom(type))
            .Where(type => !type.IsAbstract && !type.IsInterface);

        var localizationDataInstances = localizationDataTypes.Select(Activator.CreateInstance).Cast<TLocalizationData>();

        foreach (var localizationData in localizationDataInstances)
        {
            if (localizationData is null) continue;

            var localizationDataName = localizationData.GetType().Name.Replace("LocalizationData", string.Empty);
            localizationDataName = localizationDataName.Remove(0, count: localizationDataName.IndexOf("View") + "View".Length);
            _viewLocalizationData.Add(localizationDataName, localizationData);
        }
    }
    public TLocalizationData GetLocalizationData(string language)
    {
        if (_viewLocalizationData.ContainsKey(language) == false)
        {
            throw new NotSupportedException($"{language} language is not supported.");
        }

        return _viewLocalizationData[language];
    }
}
