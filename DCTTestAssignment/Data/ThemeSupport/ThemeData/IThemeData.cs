using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCTTestAssignment.Data.ThemeSupport.ThemeData;

public interface IThemeData
{
    public string NavigationBarBackground { get; }
    public string MainContentBackground { get; }
    public string Foreground { get; }
    public string BorderBrush { get; }
}
