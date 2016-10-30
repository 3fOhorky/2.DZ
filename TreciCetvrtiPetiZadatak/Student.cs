using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreciCetvrtiPetiZadatak
{
    public class Student
    {
        public string Name { get; set; }
        public string Jmbag { get; set; }
        public Gender Gender { get; set; }
        public Student(string name, string jmbag)
        {
            Name = name;
            Jmbag = jmbag;
        }

        public void Example1()
        {
            var list = new List<Student>()
            {
                new Student (" Ivan ", jmbag :" 001234567 ")
            };
            var ivan = new Student(" Ivan ", jmbag: " 001234567 ");
            // false :(
            bool anyIvanExists = list.Any(s => (bool)(s == ivan));
        }
        public void Example2()
        {
            var list = new List<Student>()
            {
                new Student (" Ivan ", jmbag :" 001234567 "),
                new Student (" Ivan ", jmbag :" 001234567 ")
            };
            // 2 :(
            var distinctStudents = list.Distinct().Count();
        }

        public override bool Equals(object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Student return false.
            Student p = obj as Student;
            if ((System.Object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (this.Name == p.Name) && (this.Jmbag == p.Jmbag);
        }

        public bool Equals(Student p)
        {
            // If parameter is null return false:
            if ((object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (this.Name == p.Name) && (this.Jmbag == p.Jmbag);
        }

        public override int GetHashCode()
        {
            return this.Jmbag.GetHashCode();
        }

        public static object operator ==(Student student1, Student student2)
        {
            return student1.Equals(student2);
        }

        public static object operator !=(Student student1, Student student2)
        {
            return !student1.Equals(student2);
        }
    }
    public enum Gender
    {
        Male, Female
    }
}
