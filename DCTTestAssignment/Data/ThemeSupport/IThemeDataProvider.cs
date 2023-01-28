using DCTTestAssignment.Data.ThemeSupport.ThemeData;

namespace DCTTestAssignment.Data.ThemeSupport
{
    public interface IThemeDataProvider
    {
        IThemeData GetThemeData(string themeName);
    }
}