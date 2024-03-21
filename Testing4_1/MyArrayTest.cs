using Debug_4_1;
using System.Reflection;
using System.Security.Cryptography;
using Xunit;

namespace Testing4_1
{
    public class MyArrayTest
    {
        #region Capacity
        [Fact]
        public void CapacityDefault()
        {
            MyArray<int> ints = new MyArray<int>();
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
            for (int i = 0; i < 4; i++)
            {
                int y = Random.Shared.Next(0, 2);
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
            for (int i = 0; i < 5; i++)
            {
                strings.Add(i.ToString());
            }
            string[] collection = { "0", "1", "2", "3", "4" };
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
            string[] collection = { "0", "1", "2", "3", "4", "5" };
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
            MyArray<int> ints = null;
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
            for (int i = 0; i < 5; i++)
            {
                arr.Add(i);
            }
            MyArray<int> ints = new MyArray<int>(4);
            for (int i = 0; i < 4; i++)
            {
                ints.Add(i * 2);
            }
            arr.AddRange(ints);
            int[] collection = { 0, 2, 4, 6, 0, 1, 2, 3, 4 };
            Assert.Equal(collection, arr.ToArray());
        }
        [Fact]
        public void AddRangeSame()
        {
            MyArray<int> arr = [];
            for (int i = 0; i < 5; i++)
            {
                arr.Add(i);
            }
            MyArray<int> ints = [];
            for (int i = 0; i < 5; i++)
            {
                ints.Add(i);
            }
            arr.AddRange(ints);
            int[] collection = { 0, 1, 2, 3, 4, 0, 1, 2, 3, 4 };
            Assert.Equal(collection, arr.ToArray());
        }
        #endregion

        #region All
        [Fact]
        public void AllTrue()
        {
            MyArray<byte> bytes = new MyArray<byte>(5);
            for (int i = 0; i < 5; i++)
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
                bytes.Add(0);
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
            for (int i = 0; i < 2; i++)
            {
                strings.Add("ab");
            }
            strings.Add("abcdef");
            bool result = strings.Any(condition);
            Assert.True(result);
        }

        private static bool Condition(string x)
        {
            return x.Length > 5;
        }
        [Fact]
        public void AnyFalse()
        {
            MyArray<byte> bytes = new MyArray<byte>(5);
            for (int i = 0; i < 5; i++)
            {
                bytes.Add(0);
            }
            bool result = bytes.Any(x => x == 1);
            Assert.False(result);
        }
        #endregion

        #region Average
        [Fact]
        public void AverageException()
        {
            Func<char[], char> checking = null;
            MyArray<char> chars = new MyArray<char>(2);
            bool result = false;
            for (int i = 0; i < 2; i++)
            {
                chars.Add('i');
            }
            try
            {
                chars.Average(checking);
            }
            catch (ArgumentNullException)
            {
                result = true;
            }
            Assert.True(result);
        }
        [Fact]
        public void Average()
        {
            MyArray<int> arr = new MyArray<int>(5);
            for (int i = 0; i < 5; i++)
            {
                arr.Add(i);
            }
            double result = arr.Average((array) =>
            {
                int average = 0;
                for (int i = 0; i < array.Length; i++)
                {
                    average += array[i];
                }
                return average / array.Length;
            });
            Assert.Equal(2.0, result);
        }
        #endregion

        #region Clear
        [Fact]
        public void Clear()
        {
            MyArray<int> arr = [];
            for (int i = 0; i < 5; i++)
            {
                arr.Add(i);
            }
            arr.Clear();
            int size = arr.Count;
            Assert.Equal(0, size);
        }
        #endregion

        #region CountByWhere
        [Fact]
        public void CountByWhereException()
        {
            Func<char, bool> checking = null;
            MyArray<char> chars = new MyArray<char>(2);
            bool result = false;
            for (int i = 0; i < 2; i++)
            {
                chars.Add('i');
            }
            try
            {
                chars.CountByWhere(checking);
            }
            catch (ArgumentNullException)
            {
                result = true;
            }
            Assert.True(result);
        }
        [Fact]
        public void CountByWhereZero()
        {
            MyArray<char> chars = new MyArray<char>(2);
            for (int i = 0; i < 2; i++)
            {
                chars.Add('i');
            }
            int result = chars.CountByWhere(x => x == 'a');
            Assert.Equal(0, result);
        }
        [Fact]
        public void CountByWhere()
        {
            MyArray<char> chars = new MyArray<char>(2);
            for (int i = 0; i < 2; i++)
            {
                chars.Add('i');
            }
            int result = chars.CountByWhere(x => x == 'i');
            Assert.Equal(2, result);
        }
        #endregion

        #region Contains
        [Fact]
        public void ContainsNullFalse()
        {
            MyArray<string> strings = new MyArray<string>(2);
            for (int i = 0; i < 2; i++)
            {
                strings.Add("abc");
            }
            Assert.False(strings.Contains(null));
        }
        [Fact]
        public void ContainsNullTrue()
        {
            MyArray<string> strings = new MyArray<string>(2);
            for (int i = 0; i < 2; i++)
            {
                strings.Add(null);
            }
            Assert.True(strings.Contains(null));
        }

        [Fact]
        public void ContainsTrue()
        {
            MyArray<char> chars = [];
            for (int i = 0; i < 2; i++)
            {
                chars.Add('i');
            }
            char item = 'i';
            Assert.True(chars.Contains(item));
        }
        [Fact]
        public void ContainsFalse()
        {
            MyArray<char> chars = [];
            for (int i = 0; i < 2; i++)
            {
                chars.Add('i');
            }
            char item = 'a';
            Assert.False(chars.Contains(item));
        }
        #endregion

        #region CopyTo
        [Fact]
        public void CopyTo()
        {
            MyArray<string> strings = [];
            for (int i = 0; i < 5; i++)
            {
                strings.Add(i.ToString());
            }
            string[] insertingArray = new string[7];
            strings.CopyTo(insertingArray, 1);
            string result = "";
            for(int i = 0; i< 7;i++)
            {
                if (insertingArray[i]!="")
                {
                    result+= insertingArray[i];
                }
            }
            Assert.Equal("01234", result);
        }
        #endregion

        #region First
        [Fact]
        public void First()
        {
            MyArray<int> ints = [];
            for (int i = 0; i < 8; i++)
            {
                ints.Add(i+1);
            }
            int result = ints.First(x => x>5 && x%2!=0);
            Assert.Equal(7, result);
        }
        [Fact]
        public void FirstException()
        {
            MyArray<int> ints = [];
            for (int i = 0; i < 8; i++)
            {
                ints.Add(i + 1);
            }
            Assert.ThrowsAny<Exception>(() => ints.First(x => x>100));
        }
        [Fact]
        public void FirstNull()
        {
            MyArray<int> ints = [];
            for (int i = 0; i < 8; i++)
            {
                ints.Add(i + 1);
            }
            int result = ints.First(null);
            Assert.Equal(1, result);
        }
        #endregion

        #region FirstOrDefault
        [Fact]
        public void Default()
        {
            MyArray<int> ints = [];
            for (int i = 0; i < 8; i++)
            {
                ints.Add(i + 1);
            }
            Assert.Equal(default(int), ints.FirstOrDefault(x => x > 100));
        }
        [Fact]
        public void FirstOrDefaultNull()
        {
            MyArray<int> ints = [];
            for (int i = 0; i < 4; i++)
            {
                ints.Add(i + 1);
            }
            Assert.Equal(1, ints.FirstOrDefault(null));
        }
        [Fact]
        public void FirstOrDefault()
        {
            MyArray<int> ints = [];
            for (int i = 0; i < 8; i++)
            {
                ints.Add(i + 1);
            }
            Assert.Equal(6, ints.FirstOrDefault(x => x > 5 && x % 2 == 0));
        }
        #endregion
        #region ForEach
        [Fact]
        public void ForEachException()
        {
            MyArray<int> ints = [];
            for (int i = 0; i < 2; i++)
            {
                ints.Add(i + 1);
            }
            Assert.Throws<ArgumentNullException>(() => ints.ForEach(null));
        }
        [Fact]
        public void ForEach()
        {
            MyArray<int> ints = [];
            for (int i = 0; i < 2; i++)
            {
                ints.Add(i + 1);
            }
            int[] collection = { };
            Action<int> action = (x) =>
            {
                Console.WriteLine($"Element multiplied by 2 = {x*2}");
            };

            ints.ForEach(action);
        }
        #endregion

        #region IndexOf
        [Fact]
        public void IndexOf()
        {
            MyArray<int> ints = [];
            for (int i = 0; i < 4; i++)
            {
                ints.Add(i + 1);
            }
            Assert.Equal(2, ints.IndexOf(3));
        }
        #endregion

        #region Insert
        [Fact]
        public void InsertException()
        {
            MyArray<string> strings = [];
            for (int i = 0; i< 4;i++)
            {
                strings.Add($"{i}");
            }
            Assert.Throws<ArgumentOutOfRangeException>(() => strings.Insert(5, "abc"));
        }
        [Fact]
        public void Insert()
        {
            MyArray<string> strings = [];
            for (int i = 0; i < 6; i++)
            {
                strings.Add($"{i}");
            }
            strings.Insert(5, "abc");
            string[] collection = { "0", "1", "2", "3", "4", "abc", "5"};
            Assert.Equal(collection, strings.ToArray());
        }
        #endregion

        #region Max
        [Fact]
        public void MaxInt()
        {
            MyArray<int> ints = new MyArray<int>(5);
            for (int i = 0; i < 5; i++)
            {
                ints.Add(i);
            }
            Assert.Equal(4, ints.Max());
        }
        [Fact]
        public void MaxString()
        {
            MyArray<string> strings = [];
            for (int i = 0; i < 6; i++)
            {
                strings.Add($"{i}");
            }
            strings.Add("abc");
            Assert.Equal("abc", strings.Max());
        }

        [Fact]
        public void MaxDouble()
        {
            MyArray<int> nums = [];
            for (int i = 0; i < 3; i++)
            {
                nums.Add(i+1);
            }
            Func<int, double> projector = (x) => x/1.5;
            Assert.Equal(2, nums.Max(projector));
        }
        #endregion

        #region Min
        [Fact]
        public void MinInt()
        {
            MyArray<int> ints = new MyArray<int>(5);
            for (int i = 0; i < 5; i++)
            {
                ints.Add(i);
            }
            Assert.Equal(0, ints.Min());
        }
        [Fact]
        public void MinString()
        {
            MyArray<string> strings = [];
            for (int i = 0; i < 6; i++)
            {
                strings.Add($"{i}");
            }
            strings.Add("abc");
            Assert.Equal("0", strings.Min());
        }

        [Fact]
        public void MinDouble()
        {
            MyArray<int> nums = [];
            for (int i = 0; i < 3; i++)
            {
                int item = i++;
                nums.Add(item);
            }
            Func<int, double> projector = (x) => x / 1.5;
            double expected = (double)2/3;
            Assert.Equal(expected, nums.Min(projector));
        }
        #endregion

        #region OfType
        [Fact]
        public void OfTypeAll()
        {
            MyArray<int> nums = [];
            for (int i = 0; i < 3; i++)
            {
                int item = i++;
                nums.Add(item);
            }
            int[] collection = {1,2, 3};
            Assert.Equal(collection, nums.OfType<int>());
        }
        [Fact]
        public void OfTypeNone()
        {
            MyArray<int> nums = [];
            for (int i = 0; i < 3; i++)
            {
                int item = i++;
                nums.Add(item);
            }
            string[] collection = {};
            Assert.Equal(collection, nums.OfType<string>());
        }
        #endregion

        #region Project
        [Fact]
        public void ProjectException()
        {
            MyArray<string> strings = [];
            for (int i = 0; i < 6; i++)
            {
                strings.Add($"{i}");
            }
            Func<string, int> projector = null;
            Assert.Throws<ArgumentNullException>(() => strings.Project<int>(projector));
        }
        [Fact]
        public void Project()
        {
            MyArray<string> strings = [];
            for (int i = 0; i < 4; i++)
            {
                strings.Add($"{i+1}");
            }
            Func<string, int> projector = ToInt;
            int[] collection = {1, 2, 3, 4};
            Assert.Equal(collection, strings.Project<int>(projector));
        }
        private int ToInt(string el)
        {
            bool res = int.TryParse(el, out int number);
            if (res)
            {
                return number;
            }
            return 0;
        }
        #endregion

        #region Remove
        [Fact]
        public void RemoveSuccess()
        {
            MyArray<int> nums = [];
            for (int i = 0; i < 6; i++)
            {
                nums.Add(i);
            }
            Assert.True(nums.Remove(4));
        }
        [Fact]
        public void RemoveFail()
        {
            MyArray<int> nums = [];
            for (int i = 0; i < 6; i++)
            {
                nums.Add(i);
            }
            Assert.False(nums.Remove(7));
        }
        #endregion

        #region RemoveAt
        [Fact]
        public void RemoveAt()
        {
            MyArray<int> nums = [];
            for (int i = 0; i < 6; i++)
            {
                nums.Add(i);
            }
            nums.RemoveAt(3);
            int[] collection = {0, 1, 2, 4, 5, 0};
            Assert.Equal(collection, nums.ToArray());
        }
        #endregion

        #region RemoveRange

        #endregion
    }
}