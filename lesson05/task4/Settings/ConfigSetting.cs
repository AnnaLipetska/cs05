using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Xml;

namespace task4.Settings
{
    class ConfigSetting : ISetting
    {
        public Brush SavedBackgroundBrush { get; set; }
        public Brush SavedForegroundBrush { get; set; }
        public int SavedFontSize { get; set; }
        public FontFamily SavedFontFamily { get; set; }

        XmlDocument doc;

        public ConfigSetting()
        {
            doc = LoadConfigDocument();
            ReadFromXMLorSetDefault();
        }

        public void SaveSettings()
        {
            XmlNode node = doc.SelectSingleNode("//appSettings");
            if (node == null)
            {
                node = doc.CreateNode(XmlNodeType.Element, "appSettings", "");
                XmlElement root = doc.DocumentElement;
                root.AppendChild(node);
            }

            string[] keys = new string[] {"BackColor",
                                          "TextColor",
                                          "TextSize",
                                          "TextFont"};

            string[] values = new string[] { SavedBackgroundBrush.ToString(),
                                             SavedForegroundBrush.ToString(),
                                             SavedFontSize.ToString(),
                                             SavedFontFamily.ToString() };

            for (int i = 0; i < keys.Length; i++)
            {
                XmlElement element = node.SelectSingleNode(string.Format("//add[@key='{0}']", keys[i])) as XmlElement;

                if (element != null) { element.SetAttribute("value", values[i]); }
                else
                {
                    element = doc.CreateElement("add");
                    element.SetAttribute("key", keys[i]);
                    element.SetAttribute("value", values[i]);
                    node.AppendChild(element);
                }
            }
            doc.Save(Assembly.GetExecutingAssembly().Location + ".config");
        }

        private void ReadFromXMLorSetDefault()
        {
            NameValueCollection allAppSettings = ConfigurationManager.AppSettings;


            var bc = new BrushConverter();
            string messageException = string.Empty;
            try
            {
                SavedBackgroundBrush = (Brush)bc.ConvertFromString(allAppSettings["BackColor"]);
            }
            catch (Exception)
            {
                SavedBackgroundBrush = (Brush)bc.ConvertFromString(Colors.AliceBlue.ToString());
                messageException += "Цвет фона не задан или задан не верно: " + allAppSettings["BackColor"] + Environment.NewLine;
            }

            try
            {
                SavedForegroundBrush = (Brush)bc.ConvertFromString(allAppSettings["TextColor"]);
            }
            catch (Exception)
            {
                SavedForegroundBrush = (Brush)bc.ConvertFromString(Colors.Black.ToString());
                messageException += "Цвет текста не задан или задан не верно: " + allAppSettings["TextColor"] + Environment.NewLine;
            }

            try
            {
                SavedFontSize = int.Parse(allAppSettings["TextSize"]);
            }
            catch (Exception)
            {
                SavedFontSize = 12;
                messageException += "Размер текста не задан или задан не верно: " + allAppSettings["TextSize"] + Environment.NewLine;
            }

            try
            {
                SavedFontFamily = new FontFamily(allAppSettings["TextFont"]);
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

        private XmlDocument LoadConfigDocument()
        {
            XmlDocument doc = null;
            try
            {
                doc = new XmlDocument();
                doc.Load(Assembly.GetExecutingAssembly().Location + ".config");
                return doc;
            }
            catch (System.IO.FileNotFoundException e)
            {
                throw new Exception("No configuration file found.", e);
            }
        }
    }
}
