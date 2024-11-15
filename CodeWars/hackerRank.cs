using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CodeWars
{
    public static class HackerRank
    {
        public static void ClosestNumbers(List<int> numbers)
        {
   
            // Step 1: Sort the array
            numbers.Sort();
            numbers = numbers.Distinct().ToList();

            // Step 2: Find the minimum absolute difference
            int minDifference = 0;

            for (int i = 1; i < numbers.Count; i++)
            {
                int difference = numbers[i] - numbers[i - 1];
                minDifference = Math.Max(minDifference, difference);
            }

            // Step 3: Find and print all pairs with that minimum absolute difference
            for (int i = 1; i < numbers.Count; i++)
            {
                int difference = numbers[i] - numbers[i - 1];
                if (difference == minDifference)
                {
                    Console.WriteLine($"{numbers[i - 1]} {numbers[i]}");
                }
            }        
        }



    }

    public class HackerRankTest
    {

        [Fact]
        public void Test1()
        {
           HackerRank.ClosestNumbers(new List<int> { 4,4,2,1,3 });
           

        }
    }
}
