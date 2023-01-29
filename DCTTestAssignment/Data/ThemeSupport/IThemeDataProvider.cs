using DCTTestAssignment.Data.ThemeSupport.ThemeData;

namespace DCTTestAssignment.Data.ThemeSupport
{
    public interface IThemeDataProvider
    {
        public string CurrentThemeName { get; set; }
        IThemeData GetThemeData();
        IThemeData GetThemeData(string themeName);
    }
}