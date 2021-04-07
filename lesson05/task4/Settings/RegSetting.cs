using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace task4.Settings
{
    class RegSetting : ISetting
    {
        public Brush SavedBackgroundBrush { get; set; }
        public Brush SavedForegroundBrush { get; set; }
        public int SavedFontSize { get; set; }
        public FontFamily SavedFontFamily { get; set; }

        RegistryKey myKey;

        public RegSetting()
        {
            myKey = Registry.CurrentUser;
            myKey = myKey.OpenSubKey("Software", true);
            string[] keyNameArray = myKey.GetSubKeyNames();
            string key = keyNameArray.FirstOrDefault(x => x == "MyAppSettings");
            if (key == null)
            {
                RegistryKey newKey = myKey.CreateSubKey("MyAppSettings");
                newKey.SetValue("BackColor", "");
                newKey.SetValue("TextColor", "");
                newKey.SetValue("TextSize", "");
                newKey.SetValue("TextFont", "");
            }
            myKey = myKey.OpenSubKey(key, true);
            ReadFromRegistry();
        }

        private void ReadFromRegistry()
        {
            var bc = new BrushConverter();
            string messageException = string.Empty;
            try
            {
                SavedBackgroundBrush = (Brush)bc.ConvertFromString(myKey.GetValue("BackColor").ToString());
            }
            catch (Exception)
            {
                SavedBackgroundBrush = (Brush)bc.ConvertFromString(Colors.AliceBlue.ToString());
                messageException += "Цвет фона не задан или задан не верно: " + myKey.GetValue("BackColor") + Environment.NewLine;
            }

            try
            {
                SavedForegroundBrush = (Brush)bc.ConvertFromString(myKey.GetValue("TextColor").ToString());
            }
            catch (Exception)
            {
                SavedForegroundBrush = (Brush)bc.ConvertFromString(Colors.Black.ToString());
                messageException += "Цвет текста не задан или задан не верно: " + myKey.GetValue("TextColor") + Environment.NewLine;
            }

            try
            {
                SavedFontSize = Convert.ToInt32(myKey.GetValue("TextSize"));
            }
            catch (Exception)
            {
                SavedFontSize = 12;
                messageException += "Размер текста не задан или задан не верно: " + myKey.GetValue("TextSize") + Environment.NewLine;
            }

            try
            {
                SavedFontFamily = new FontFamily(myKey.GetValue("TextFont").ToString());
            }
            catch (Exception)
            {
                SavedFontFamily = new FontFamily("Segoe UI");
            }

            if (!string.IsNullOrEmpty(messageException))
            {
                MessageBox.Show(messageException);
            }
        }

        public void SaveSettings()
        {
            myKey.SetValue("BackColor", SavedBackgroundBrush);
            myKey.SetValue("TextColor", SavedForegroundBrush);
            myKey.SetValue("TextSize", SavedFontSize);
            myKey.SetValue("TextFont", SavedFontFamily);
        }
    }
}
