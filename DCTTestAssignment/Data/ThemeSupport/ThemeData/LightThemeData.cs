using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCTTestAssignment.Data.ThemeSupport.ThemeData;

public class LightThemeData : IThemeData
{
    public string NavigationBarBackground => "#FFFFFF";

    public string MainContentBackground => "#F8FAFC";

    public string Foreground => "#000000";

    public string BorderBrush => "#ABADB3";
}
