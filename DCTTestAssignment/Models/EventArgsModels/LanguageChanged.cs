using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCTTestAssignment.Models.EventArgsModels;

public class LanguageChanged
{
    public string Language { get; set; }

    public LanguageChanged(string language)
    {
        Language = language;
    }
}
