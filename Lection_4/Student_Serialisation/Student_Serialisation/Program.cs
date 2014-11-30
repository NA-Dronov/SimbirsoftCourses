using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace Student_Serialisation
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Student> stud_list = Student.GetStudentsList(10);
            using (Stream fStream = new FileStream("Students.xml", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                XmlSerializer xmlFormat = new XmlSerializer(typeof(List<Student>));
                xmlFormat.Serialize(fStream, stud_list);
            }
            Console.WriteLine("-> List saved!");
            Console.ReadLine();
        }
    }
}
