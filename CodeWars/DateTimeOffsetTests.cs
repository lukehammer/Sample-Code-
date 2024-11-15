using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace CodeWars
{
    public class TimeTests(ITestOutputHelper testOutputHelper)
    {
        public ITestOutputHelper TestOutputHelper { get; } = testOutputHelper;

        [Fact]
        public void DateTimeOffsetTest()
        {

            var convertedTime = DateTimeOffset.FromUnixTimeSeconds(1731442275);

            TestOutputHelper.WriteLine($"{convertedTime}");

        }


    }
}
