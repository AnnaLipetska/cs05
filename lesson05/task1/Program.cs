using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace task1
{
    class Program
    {
        // Создайте.xml файл, который соответствовал бы следующим требованиям:
        
        static void Main(string[] args)
        {
            //- имя файла: TelephoneBook.xml
            DriveInfo[] drives = DriveInfo.GetDrives();

            string lastDriveName = string.Empty;
            for (int i = drives.Count() - 1; i >= 0; i--)
            {
                if (drives[i].DriveType == DriveType.CDRom)
                {
                    continue;
                }
                lastDriveName = (drives[i]).Name;
                break;
            }

            var fullName = lastDriveName + "TelephoneBook.xml";

            using (var xmlWriter = new XmlTextWriter(fullName, null))
            {
                xmlWriter.Formatting = Formatting.Indented;
                xmlWriter.Indentation = 1;
                xmlWriter.IndentChar = '\t';
                xmlWriter.QuoteChar = '\'';

                //- корневой элемент: “MyContacts”
                //- тег “Contact”, и в нем должно быть записано имя контакта и атрибут “TelephoneNumber”
                // со значением номера телефона.

                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("MyContacts");
                xmlWriter.WriteStartElement("Contact");
                xmlWriter.WriteStartAttribute("TelephoneNumber");
                xmlWriter.WriteString("(098)0710223");
                xmlWriter.WriteEndAttribute();
                xmlWriter.WriteString("Anna Lipetska");
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("Contact");
                xmlWriter.WriteStartAttribute("TelephoneNumber");
                xmlWriter.WriteString("(077)7777777");
                xmlWriter.WriteEndAttribute();
                xmlWriter.WriteString("Harry Potter");
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndElement();
            }

            Console.WriteLine($"Файл {fullName} успешно создан.");
            Console.ReadKey();
        }
    }
}
