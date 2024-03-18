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
            //arr.AddRange(arr);

            //foreach (var item in arr)
            //{
            //    Console.WriteLine(item);
            //}
            //Console.WriteLine();
            //Console.WriteLine(arr[1]);
            //Console.WriteLine();
            //Console.WriteLine(arr.Average((array) =>
            //{
            //    int average = 0;
            //    for (int i = 0; i < array.Length; i++)
            //    {
            //        average += array[i];
            //    }
            //    return average / array.Length;
            //}));
            //Console.WriteLine(arr.All(x => Condition(x)));
            //Console.WriteLine(arr.Any(x => Condition(x)));
            MyArray<bool> bools = new MyArray<bool>();
            Console.WriteLine(bools.Capacity);
            Console.WriteLine();

        }

        static bool Condition(int x)
        {
            return x < 50;
        }
    }
}
