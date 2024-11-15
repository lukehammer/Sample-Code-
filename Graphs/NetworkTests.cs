using FluentAssertions;
using Xunit;
using Xunit.Sdk;

namespace CodeWars
{
    public class NetworkTests
    {


        [Fact]
        public void TestNetwork1()
        {
            var network = new int[][] { new int[]{2, 1, 1},
                                        new int[]{2, 3, 1},
                                        new int[]{3, 4, 1} };
            Network.NetworkDelayTime(network, 4, 2).Should().Be(2);
        }

        [Fact]
        public void findSimplePath() 
        {
            var start = 1;
            var end = 2;

            var network = new List<Path> { new Path(start, end, 1) };
            var actual = Network.GetValidPaths(network, start, end);

            var expected = new List<List<int>> { new List<int> { start, end } };
            expected.Should().BeEquivalentTo(actual);
        }


        [Fact]
        public void findThreeStepPath()
        {
            var start = 1;
            var middle = 2;
            var end = 3;

            var network = new List<Path> { 
                new Path(start, middle, 1),
                new Path(middle, end, 1),
            };
            var actual = Network.GetValidPaths(network, start, end);

            var expected = new List<List<int>> { new List<int> { start, end } };
            expected.Should().BeEquivalentTo(actual);

        }



        //[Fact]
        //public void TestNetwork2()
        //{
        //    var network = new int[][] { new int[] { 1, 2, 1 } };
        //    Network.NetworkDelayTime(network, 2, 1).Should().Be(1);
        //}

        //[Fact]
        //public void TestNetwork3()
        //{
        //    var network = new int[][] { new int[] { 1, 2, 1 } };
        //    Network.NetworkDelayTime(network, 2, 2).Should().Be(0);
        //}

        //[Fact]
        //public void Test_origin_and_target_same()
        //{
        //    var network = new int[][] { new int[] { 1, 2, 1 } };
        //    Network.NetworkDelayTime(network, 2, 2).Should().Be(0);
        //}



    }
}
