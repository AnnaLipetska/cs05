using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace task2
{
    class Program
    {
        // Создайте приложение, которое выводит на экран всю информацию об указанном .xml файле.
        static void Main(string[] args)
        {
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

            try
            {
                using (FileStream stream = new FileStream(fullName, FileMode.Open))
                {
                    using (XmlTextReader xmlReader = new XmlTextReader(stream))
                    {
                        while (xmlReader.Read())
                        {
                            Console.WriteLine("NodeType: {0,-15}| Name: {1,-15}| Value: {2,-15}",
                                            xmlReader.NodeType,
                                            xmlReader.Name,
                                            xmlReader.Value);
                        }
                    }
                }

                Console.WriteLine(new string('-', 20));

                XmlDocument document = new XmlDocument();
                document.Load(fullName);

                Console.WriteLine(document.InnerText);

                Console.WriteLine(new string('-', 20));

                Console.WriteLine(document.InnerXml);

                Console.WriteLine(new string('-', 20));

                XmlNode contacts = document.DocumentElement;

                Console.WriteLine("document.DocumentElement = {0}\n", contacts.LocalName);

                foreach (XmlNode contact in contacts.ChildNodes)
                {
                    Console.WriteLine("Found contact:");

                    foreach (XmlNode contactInfo in contact.ChildNodes)
                    {
                        Console.WriteLine(contactInfo.Name + ": " + contactInfo.InnerText);
                    }
                    foreach (XmlAttribute attribute in contact.Attributes)
                    {
                        Console.WriteLine(attribute.Name + ": " + attribute.Value);
                    }
                    Console.WriteLine(new string('-', 40));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            Console.ReadKey();
        }
    }
}
