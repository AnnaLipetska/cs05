using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace task3
{
    class Program
    {
        static void Main(string[] args)
        {
            // Из файла TelephoneBook.xml (файл должен был быть создан в процессе выполнения
            // дополнительного задания) выведите на экран только номера телефонов.

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
                            if (xmlReader.HasAttributes)
                            {
                                if (xmlReader.Name.Equals("Contact"))
                                {
                                    Console.WriteLine(xmlReader.GetAttribute("TelephoneNumber"));
                                }
                            }
                        }
                    }
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
