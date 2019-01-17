using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            //First, FirstOrDefault, last, LastOrDefault, Single
            var WholeNum = new List<int>() { -1, -2, 3, -5, 6, -8, 10 };
            var WholeNum2 = new List<int>() { -1, -2, -3, -5, -6, -8, -10 };
            var Symb = "c";
            var StringList = new List<string>() { "somebody cant do this", "сompany", "no one", "dog", "cat" };
            //1.Набор целых чисел (List). Показать первый положительный и последний отрицательный

            Console.WriteLine(WholeNum.First(x => x > 0));
            Console.WriteLine(WholeNum.Last(x => x < 0));
            //2. Расширим предыдущую задачу. Вернуть null в случае отсутствия одного из искомых элементов.

            Console.WriteLine(WholeNum2.LastOrDefault(x => x > 0));
            //3. Есть некоторый символ и есть набор строк. Если в наборе есть один элемент начинающийся с С, то показать его. Пустая строка - если таких элементов нет. Если таких строк несколько, то вернуть строку из них.

            // StringList.Where(x => x.SkipWhile())
            Console.WriteLine(StringList.FirstOrDefault(x => x.StartsWith(Symb)));
            //4. Усложняем задание -обработать ситуацию с ошибкой в предыдущем примере(когда она будет и почему) -Реестр символа должен быть не важен.

            Console.WriteLine(StringList.FirstOrDefault(x => x.ToLower().StartsWith(Symb.ToLower())));
            Console.WriteLine(new string('-', 20));

            //Where, Distinct
            var Numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 1, 3, 5, 20, 80, 106 };
            //5. Есть набор чисел. Вернем все четные, удалим повторы

            Numbers.Where(x => x % 2 == 0).Distinct().ToList().ForEach(x => Console.WriteLine(x));
            Console.WriteLine(new string('-', 20));
            //6. Есть число D и лист целых чисел. Найти первый элемент, больший чем D. Вернуть все четные и положительные числа, поменяв их порядок следования
            int D = 6;
            //  Console.WriteLine(Numbers.Where(x => x > D).FirstOrDefault());

            Numbers.SkipWhile(x => x < D || x == D).Where(x => x % 2 == 0).Reverse().ToList().ForEach(x => Console.WriteLine(x));
            Console.WriteLine(new string('-', 20));

            //Concat, DefaultIfEmpty
            //7. Есть число. Есть два листа чисел. Сделать новый лист из элементов больших чем число (из первой последовательности) и элементов меньших числа (из второй). Если таких элементов нет - подставить некоторую константу.
            var X = 20;
            var First = new List<int>() { 10, 12, 13, 14, 20, 60, 70, 80, 90 };
            var Second = new List<int>() { 1, 21, 33, 4, 5, 20, 2, 3, 48 };
            var Result = First.Where(x => x > X).ToList().Concat(Second.Where(x => x < X)).ToList();
            Result.ForEach(x => Console.WriteLine(x));
            Console.WriteLine(new string('-', 20));
            //GroupBy, ToDictionary

            //8. Есть поставщики, и есть сумма за их поставки. Каждая поставка имеет дату.
            var providers = new List<Provider>()
                {
                    new Provider() {Name = "Provider1", Amount = 3500, Date = new DateTime(2018, 11, 02)},
                    new Provider() {Name = "Provider1", Amount = 4500, Date = new DateTime(2017, 10, 17)},
                    new Provider() {Name = "Provider1", Amount = 6500, Date = new DateTime(2016, 12, 05)},
                    new Provider() {Name = "Provider2", Amount = 1500, Date = new DateTime(2018, 10, 19)},
                    new Provider() {Name = "Provider2", Amount = 6500, Date = new DateTime(2018, 08, 12)},
                    new Provider() {Name = "Provider3" }
                };

            var res = providers.Where(x => x.Amount != 0).GroupBy(x => x.Name).Select(g => new
            {
                Name = g.Key,
                Amount = g.Sum(c => c.Amount),
                FirstDate = g.Min(c => c.Date),
                LastDate = g.Max(c => c.Date)
            });

            Console.WriteLine("Name Ammount FirstDate LastDate");
            foreach (var c in res)
            {
                Console.WriteLine(c.Name + " " + c.Amount + " " + c.FirstDate.ToString("d") + " " + c.LastDate.ToString("d"));
            }

        }
    }
}
