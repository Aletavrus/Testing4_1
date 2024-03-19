using Debug_4_1;
using Xunit;

namespace Testing4_1
{
    public class MyArrayTest
    {
        #region Capacity
        [Fact]
        public void CapacityDefault()
        {
            MyArray<int>  ints = new MyArray<int>();
            int capacity = ints.Capacity;
            Assert.Equal(7, capacity);
        }
        [Fact]
        public void CapacityCustom()
        {
            MyArray<int> ints = new MyArray<int>(10);
            int capacity = ints.Capacity;
            Assert.Equal(10, capacity);
        }
        [Fact]
        public void CapacityCollection()
        {
            bool[] boolsToAdd = new bool[4];
            for (int i = 0; i< 4; i++)
            {
                int y = Random.Shared.Next(0,2);
                boolsToAdd[i] = y == 1;
            }
            MyArray<bool> bools = new MyArray<bool>(boolsToAdd);
            int capacity = bools.Capacity;
            Assert.Equal(4, capacity);
        }
        [Fact]
        public void CapacitySet()
        {
            MyArray<int> ints = new MyArray<int>(5);
            int value = 0;
            bool result = false;
            for (int i = 0; i < 5; i++)
            {
                ints.Add(i);
            }
            try
            {
                ints.Capacity = value;
            }
            catch (ArgumentOutOfRangeException)
            {
                result = true;
            }
            Assert.True(result);
        }
        #endregion

        #region Addition
        [Fact]
        public void Add()
        {
            MyArray<string> strings = [];
            for (int i = 0; i< 5; i++)
            {
                strings.Add(i.ToString());
            }
            string[] collection = {"0", "1", "2","3", "4"};
            Assert.Equal(strings.ToArray(), collection);
        }
        [Fact]
        public void AddExtra()
        {
            MyArray<string> strings = [];
            for (int i = 0; i < 5; i++)
            {
                strings.Add(i.ToString());
            }
            strings.Add("5");
            string[] collection = { "0", "1", "2", "3", "4", "5"};
            Assert.Equal(strings.ToArray(), collection);
        }

        [Fact]
        public void AddRangeWithException()
        {
            MyArray<int> arr = [];
            bool result = false;
            for (int i = 0; i < 5; i++)
            {
                arr.Add(i);
            }
            MyArray<int> ints = new MyArray<int>(0);
            try
            {
                arr.AddRange(ints);
            }
            catch (ArgumentNullException)
            {
                result = true;
            }
            Assert.True(result);
        }
        [Fact]
        public void AddRangeDifferent()
        {
            MyArray<int> arr = [];
            bool result = false;
            for (int i = 0; i < 5; i++)
            {
                arr.Add(i);
            }
            MyArray<int> ints = new MyArray<int>(4);
            for (int i = 0; i< 4; i++)
            {
                ints[i] = i*2;
            }
            arr.AddRange(ints);
            int[] collection = { 0, 2, 4, 6, 0, 1, 2, 3, 4 };
            Assert.Equal(collection, arr.ToArray());
        }
        [Fact]
        public void AddRangeSame()
        {
            MyArray<int> arr = [];
            bool result = false;
            for (int i = 0; i < 5; i++)
            {
                arr.Add(i);
            }
            MyArray<int> ints = new MyArray<int>(5);
            for (int i = 0; i < 5; i++)
            {
                ints[i] = i;
            }
            arr.AddRange(ints);
            int[] collection = {0, 1, 2, 3, 4, 0, 1, 2, 3, 4};
            Assert.Equal(collection, arr.ToArray());
        }
        #endregion

        #region All
        [Fact]
        public void AllTrue()
        {
            MyArray<byte> bytes = new MyArray<byte>(5);
            for (int i = 0; i< 5;i++)
            {
                bytes[i] = 0;
            }
            bool result = bytes.All(x => x == 0);
            Assert.True(result);
        }
        [Fact]
        public void AllFalse()
        {
            MyArray<byte> bytes = new MyArray<byte>(5);
            for (int i = 0; i < 5; i++)
            {
                bytes[i] = 0;
            }
            bool result = bytes.All(x => x == 1);
            Assert.False(result);
        }
        #endregion
        #region Any
        [Fact]
        public void AnyTrue()
        {
            Func<string, bool> condition = Condition;
            MyArray<string> strings = new MyArray<string>(3);
            for (int i = 0; i< 2; i++)
            {
                strings.Add("ab");
            }
            strings.Add("abcdef");
            bool result = strings.Any();
            Assert.True(result);
        }

        private static bool Condition(string x)
        {
            return x.Length > 5;
        }



        #endregion
    }
}