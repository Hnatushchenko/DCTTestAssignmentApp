using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCTTestAssignment.Data.ThemeSupport.ThemeData;

public class DarkThemeData : IThemeData
{
    public string NavigationBarBackground => "#1F1F1F";

    public string MainContentBackground => "#121212";

    public string Foreground => "#E8E8E8";

    public string BorderBrush => "#414448";
}
