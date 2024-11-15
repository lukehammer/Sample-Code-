using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Xunit;

namespace CodeWars
{

    public static partial class Kata
    {
        public static int RemoveDuplicates(int[] nums)
        {

            if (nums.Length < 3 ) return nums.Length;
            int writeToLocation = 2;
            for (int i = 2; i < nums.Length; i++)
            {
                var currentNumberSameAs2ago = nums[i] == nums[writeToLocation - 2];
                if ( currentNumberSameAs2ago)
                { continue; }
                nums[writeToLocation++] = nums[i];
            }

            return writeToLocation;
        }
    }




    public partial class CandyTest
    {
        [Fact]
        public void SampleTest3()
        {
            var input = new int[] { 1, 1, 1, 2, 2, 2 };
            Assert.Equal(4, Kata.RemoveDuplicates(input));
        }



        [Fact]
        public void SampleTest2()
        {
            Assert.Equal(0, Kata.RemoveDuplicates(new int[0]));
            Assert.Equal(1, Kata.RemoveDuplicates(new int[] { 1 }));
            Assert.Equal(3, Kata.RemoveDuplicates(new int[] { 1, 2, 3 }));
            Assert.Equal(2, Kata.RemoveDuplicates(new int[] { 1, 1, 1 }));
            Assert.Equal(12, Kata.RemoveDuplicates(new int[] { 1, 1, 1,2,2,2,2,2,2,3,3,3,4,4,4,5,5,5,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6 }));






        }

 
    }
}