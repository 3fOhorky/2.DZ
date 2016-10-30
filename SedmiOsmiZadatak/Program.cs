using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SedmiOsmiZadatak
{
    class Program
    {
        static void Main(string[] args)
        {
            var asyncOperation = FactorialDigitSum(3);
            Console.WriteLine(asyncOperation.Result);
            
            // Main method is the only method that
            // can ’t be marked with async .
            // What we are doing here is just a way for us to simulate
            // async - friendly environment you usually have with
            // other .NET application types ( like web apps , win apps etc .)
            // Ignore main method , you can just focus on
            // LetsSayUserClickedAButtonOnGuiMethod() as a
            // first method in call hierarchy .
            var t = Task.Run(() => LetsSayUserClickedAButtonOnGuiMethod());
            Console.Read();
            Console.ReadLine();
        }
        public static async Task<int> FactorialDigitSum(int n)
        {
            var result = await Task.Run(() => {
                return Factorials(n);
            });
            return result;
        }

        private static int Factorials(int n)
        {
            if (n <= 1)
                return 1;
            return n*Factorials(n - 1);
        }

        private static async void LetsSayUserClickedAButtonOnGuiMethod()
        {
            var result = await GetTheMagicNumber();
            Console.WriteLine(result);
        }
        private static async Task<int> GetTheMagicNumber()
        {
            return await IKnowIGuyWhoKnowsAGuy();
        }
        private static async Task<int> IKnowIGuyWhoKnowsAGuy()
        {
            return await IKnowWhoKnowsThis(10) + await IKnowWhoKnowsThis(5);
        }
        private static async Task<int> IKnowWhoKnowsThis(int n)
        {
            return await FactorialDigitSum(n);
        }
    }
}
