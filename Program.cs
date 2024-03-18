using System.Runtime.InteropServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Debug_4_1
{
    class Program
    {
        static void Main()
        {
            MyArray<int> arr = [];
            for (int i = 0; i < 5; i++)
            {
                arr.Add(i);
            }

            foreach (var item in arr)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
            arr.AddRange(arr);

            foreach (var item in arr)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
            Console.WriteLine(arr[1]);
            Console.WriteLine();
            Console.WriteLine(arr.Average((array) =>
            {
                int average = 0;
                for (int i = 0; i < array.Length; i++)
                {
                    average += array[i];
                }
                return average / array.Length;
            }));
            Console.WriteLine(arr.All(x => Condition(x)));
            Console.WriteLine(arr.Any(x => Condition(x)));
            Console.WriteLine(arr.Contains(4));
            Console.WriteLine();
            int[] copyArr = new int[arr.Capacity - 1];
            arr.CopyTo(copyArr, 1);
            Console.WriteLine(arr.CountByWhere(x => x > 11000));
            Console.WriteLine();
            Func<int, bool> func = AnotherCondition;
            Console.WriteLine(arr.First(func));
            Console.WriteLine();
            Console.WriteLine(arr.First(x => x>100000));






            MyArray<bool> bools = new MyArray<bool>();
            MyArray<bool> boolsToAdd = new MyArray<bool>(4);
            Func<int, bool> converter = Convert;
            for (int i = 0; i<4; i++)
            {
                int y = Random.Shared.Next(0, 2);
                boolsToAdd[i]=converter(y);
            }
            Console.WriteLine(bools.Capacity);
            bools.Insert(1, true);
            Console.WriteLine(bools[1]);
            Console.WriteLine();
            Console.WriteLine(bools.Count());
            bools.Clear();
            Console.WriteLine();
            Console.WriteLine(bools.Capacity);
            Console.WriteLine();
            Console.WriteLine(bools.Count());
            bools.AddRange(boolsToAdd);
            foreach(bool el in bools)
            {
                Console.WriteLine(el);
            }


        }

        static bool Condition(int x)
        {
            return x < 50;
        }

        static bool AnotherCondition(int x)
        {
            return x%2==0;
        }

        static bool Convert(int x)
        {
            return x==1;
        }
    }
}
