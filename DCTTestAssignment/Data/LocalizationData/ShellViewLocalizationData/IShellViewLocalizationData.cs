using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCTTestAssignment.Data.LocalizationData.ShellViewLocalizationData;

public interface IShellViewLocalizationData
{
    public string? DCTTestAssignment { get; }
    public string? Home { get; }
    public string? Converter { get; }
    public string? Theme { get; }
    public string? Language { get; }
}
