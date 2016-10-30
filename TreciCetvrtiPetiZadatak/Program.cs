using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreciCetvrtiPetiZadatak
{
    class Program
    {
        static void Main(string[] args)
        {
            // 3. Zadatak
            int[] integers = new[] { 1, 2, 2, 2, 3, 3, 4, 5 };
            var integersLINQ = integers.GroupBy(i => i).Select(s => new { Number = s.Key, Count = s.Count() });
            foreach (var l in integersLINQ)
            {
                Console.WriteLine(String.Format("Broj {0} ponavlja se {1} puta",l.Number,l.Count));
            }
            // strings [0] = Broj 1 ponavlja se 1 puta
            // strings [1] = Broj 2 ponavlja se 3 puta
            // strings [2] = Broj 3 ponavlja se 2 puta
            // strings [3] = Broj 4 ponavlja se 1 puta
            // strings [4] = Broj 5 ponavlja se 1 puta

            // 4. Zadatak
            // Objekti tipa Student se ne originalno ne uspoređuju kako bi željeli jer defaultna 
            // Equals metoda i == operator uspoređuju referentne tipove podataka po memorijskoj adresi.
            // Potrebno je overloadati Equals i == operator.
            // Example 1
            var list1 = new List<Student>()
            {
                new Student ("Ivan", jmbag :"001234567")
            };
            var ivan = new Student("Ivan", jmbag: "001234567");
            // false :(
            bool anyIvanExists = list1.Any(s => (bool)(s == ivan));

            // Example 2
            var list2 = new List<Student>()
            {
                new Student ("Ivan", jmbag :"001234567"),
                new Student ("Ivan", jmbag :"001234567")
            };
            // 2 :(
            var distinctStudents = list2.Distinct().Count();
            Console.WriteLine(anyIvanExists.ToString());
            Console.WriteLine(distinctStudents);

            // 5. Zadatak
            University[] universities = GetAllCroatianUniversities();
            Console.WriteLine();
            Console.WriteLine("Svi Hrvatski studenti:");
            var allCroatianStudents = universities.SelectMany(s => s.Students).Distinct();
            foreach (var s in allCroatianStudents)
            {
                Console.WriteLine(s.Name);
            }

            Console.WriteLine();
            Console.WriteLine("Hrvatski sutdenti na više sveučilišta:");
            var croatianStudentsOnMultipleUniversities = universities.SelectMany(s => s.Students).GroupBy(s => s).Where(s => s.Count() > 1).Select(s => s.Key);
            foreach (var s in croatianStudentsOnMultipleUniversities)
            {
                Console.WriteLine(s.Name);
            }

            Console.WriteLine();
            Console.WriteLine("Hrvatski sutdenti na sveučilištima bez žena:");
            //var studentsOnMaleOnlyUniversities = universities.SelectMany(s => s.Students).Where(s => s.Gender == Gender.Female);
            var studentsOnMaleOnlyUniversities = universities.Where(s => s.Students.All(i => i.Gender != Gender.Female)).SelectMany(s => s.Students);
            foreach (var s in studentsOnMaleOnlyUniversities)
            {
                Console.WriteLine(s.Name);
            }

            Console.ReadLine();
        }

        private static University[] GetAllCroatianUniversities()
        {
            var zagreb = new University { Name = "Zagreb" };
            var split = new University { Name = "Split" };
            var rijeka = new University { Name = "Rijeka" };

            var student1 = new Student("Marica", "18");
            student1.Gender = Gender.Female;
            var student2 = new Student("Marija", "19");
            student2.Gender = Gender.Female;
            var student3 = new Student("Marina", "20");
            student3.Gender = Gender.Female;
            var student4 = new Student("Mario", "21");
            student4.Gender = Gender.Male;
            var student5 = new Student("Marin", "22");
            student5.Gender = Gender.Male;

            zagreb.Students = new[] { student1, student2, student5 };
            split.Students = new[] { student3, student2 };
            rijeka.Students = new[] { student4 };

            return new[] { zagreb, split, rijeka };
        }
    }
}
