using FluentAssertions;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace CodeWars
{
    public class Dates
    {
        
        

    }
    

    public class DatesTest 
    {

        [Fact]
        public void TestDate() 
        { 
        
            var expected =  """here is "a" test""";
            var accual = "here is \"a\" test";

            expected.Should().Be(accual);
        }
    
    
    }
}
