using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace task4.Settings
{
    interface ISetting
    {
        Brush SavedBackgroundBrush { get; set; }
        Brush SavedForegroundBrush { get; set; }
        int SavedFontSize { get; set; }
        FontFamily SavedFontFamily { get; set; }
        void SaveSettings();
    }
}
