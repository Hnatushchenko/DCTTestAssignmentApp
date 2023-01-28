using DCTTestAssignment.Data.ThemeSupport.ThemeData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DCTTestAssignment.Data.ThemeSupport;

public class ThemeDataProvider : IThemeDataProvider
{
    private Dictionary<string, IThemeData> _applicationThemes = new Dictionary<string, IThemeData>();

    public ThemeDataProvider()
    {
        var themeTypes = Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => typeof(IThemeData).IsAssignableFrom(type))
            .Where(type => !type.IsAbstract && !type.IsInterface);

        var themes = themeTypes.Select(Activator.CreateInstance).Cast<IThemeData>();

        foreach (var theme in themes)
        {
            var themeName = theme.GetType().Name.Replace("ThemeData", string.Empty);
            _applicationThemes.Add(themeName, theme);
        }
    }

    public IThemeData GetThemeData(string themeName)
    {
        if (_applicationThemes.ContainsKey(themeName) == false)
        {
            throw new NotSupportedException($"{themeName} theme is not supported.");
        }

        return _applicationThemes[themeName];
    }
}
