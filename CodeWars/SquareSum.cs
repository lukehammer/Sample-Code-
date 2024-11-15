using FluentAssertions;
using Xunit;

namespace CodeWars
{
    public static partial class Kata
    {
        public static int SquareSum(int[] numbers)
        {
            var total = 0;
            foreach (int number in numbers) { total += number * number; }
            return total;
        }
    }



    public partial class Tests
    {
        public static IEnumerable<object[]> SampleTestCases
        {
            get
            {
                yield return new object[] { new int[] { 1, 2, 2 }, 9 };
                yield return new object[] { new int[] { 1, 2 }, 5 };
                yield return new object[] { new int[] { 5, 3, 4 }, 50 };
                yield return new object[] { new int[] { }, 0 };
            }
        }

        [Theory]
        [MemberData(nameof(SampleTestCases))]
        public void SampleTest(int[] numbers, int result)
        {
            Kata.SquareSum(numbers).Should().Be(result);
        }

        public class TestCaseData
        {
            public int[] numbers;
            public readonly int result;

            public TestCaseData(int[] numbers, int result)
            {
                this.numbers = numbers;
                this.result = result;
            }
        }
    }
}
