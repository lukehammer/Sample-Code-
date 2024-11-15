using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Xunit;

namespace CodeWars
{

    public static partial class Kata
    {
        public static int Candy(int[] ratings)
        {
            var students = ratings.Select(x => new Student { Ratting = x }).ToArray();

            for (var i = 1; i < students.Count(); i++)
            {
                var left = students[i - 1];
                var right = students[i];

                if (left.Ratting > right.Ratting && left.Candy <= right.Candy)
                {
                    left.Candy = right.Candy + 1;
                }
                if (right.Ratting > left.Ratting && left.Candy <= right.Candy)
                {
                    right.Candy = left.Candy + 1;
                }
            }
            return students.Sum(x => x.Candy);
        }
    }

    public class Student 
    {
        public int Ratting { get; set; }
        public int Candy { get; set; } = 1; 
    }

    public partial class SolutionTest
    {
        [Fact]
        public void CandyTest1()
        {

            Assert.Equal(1, Kata.Candy(new[] { 1 }));
            
          
            Assert.Equal(4, Kata.Candy(new[] { 1, 2, 2 }));
            Assert.Equal(6, Kata.Candy(new[] { 1, 0, 2 ,0}));
        }
 
    }
}