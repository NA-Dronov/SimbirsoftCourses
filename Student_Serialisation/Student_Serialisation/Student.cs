using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Threading.Tasks;

namespace Student_Serialisation
{
    [Serializable]
    public class Student
    {
        [XmlAttribute]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string LastName { get; set; }

        [XmlIgnore]
        public DateTime Birth { get; set; }

        [XmlElement("Birth")]
        public string BirthDate {
            get { return Birth.ToString("dd-MM-yyyy"); }
            set { Birth = DateTime.Parse(value); }
        }

        public string Faculty { get; set; }
        public float AverageMark { get; set; }

        private byte course;
        public byte Course 
        { 
            get
            {
                return course;
            }
            set 
            {
                if (course <= 0)
                    course = 1;
                else if (course > 5)
                    course = 5;
                else
                    course = value;
            }
        }

        public Student () { }

        public static List<Student> GetStudentsList(int listSize)
        {
            Random rd = new Random();
            List<Student> sl = new List<Student>();

            List<string> names = new List<string> 
            {
                "Noah", "Liam", "Jacob", "Mason", "William", "Ethan", "Michael", "Alexander", "Jayden", "Daniel",
                "Elijah", "Aiden", "James", "Benjamin", "Matthew", "Jackson", "Logan", "David", "Anthony", "Joseph",
                "Joshua", "Andrew", "Lucas", "Gabriel", "Samuel", "Christopher", "John", "Dylan", "Isaac", "Ryan",
                "Nathan", "Carter", "Caleb", "Luke", "Christian", "Hunter", "Henry", "Owen", "Landon", "Jack"
            };

            List<string> patronymics = new List<string>
            {
                "Anatolievich", "Ivanovich", "Lvovich", "Nikiforovich", "Alexandrovich", "Petrovich", "Vyacheslavich", "Nikolaevich", "Davidovich", "Denisovich",
                "Pavlovich", "Yurievich", "Eldarovich", "Haritonovich", "Semenovich", "Sergeevich", "Romanovich", "Rudolfovich", "Rostislavovich", "Robertovich",
                "Vadimovich", "Platonovich", "Artemovich", "Vladimirovich", "Boguslavovich", "Valentinovich", "Grigorievich", "Genadievich", "Georgeevich", "Vladislavovich"
            };

            List<string> lastName = new List<string>
            {
                "Smith", "Johnson", "Williams", "Jones", "Brown", "Davis", "Miller", "Wilson", "Moore", "Taylor",
                "Anderson", "Thomas", "Jackson", "White", "Harris", "Martin", "Thompson", "Garcia", "Martinez", "Robinson",
                "Clark", "Rodriguez", "Lewis", "Lee", "Walker", "Hall", "Allen", "Young", "Hernandez", "King",
                "Wright", "Lopez", "Hill", "Scott", "Green", "Adams", "Baker", "Gonzalez", "Nelson", "Carter"
            };

            List<DateTime> bl = new List<DateTime>();
            for (int i = 0; i < 70; i++)
			{
			     bl.Add(new DateTime(rd.Next(1991, 1995), rd.Next(1, 12), rd.Next(1, 28)));
			}

            List<string> fl = new List<string>
            {
                "Math", "Cybersecurity", "BioMed", "Nanotech", "Multicore", "Energy"
            };

            for (int i = 0; i < listSize; i++)
            {
                sl.Add(new Student() { ID = i + 1, 
                                       Name = names.ElementAt(rd.Next(0, names.Count)),
                                       Patronymic = patronymics.ElementAt(rd.Next(0, patronymics.Count)),
                                       LastName = lastName.ElementAt(rd.Next(0, names.Count)),
                                       Birth = bl.ElementAt(rd.Next(0, bl.Count)),
                                       Faculty = fl.ElementAt(rd.Next(0, fl.Count)),
                                       AverageMark = rd.Next(30, 50)/10,
                                       Course = (byte)rd.Next(1, 5)});
            }

            return sl;
        }
    }
}
