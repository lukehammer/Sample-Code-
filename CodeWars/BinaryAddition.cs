using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System;

namespace CodeWars
{
    internal class BinaryAddition
    {
        public static class Kata
        {
            public static string AddBinary(int a, int b)
            {
                var sum = a + b;
                return Convert.ToString(sum,2) ;
            }
        }
    }


  
  [TestFixture]
    public class AddBinaryTest
    {
        [Test]
        public void TestExample()
        {
            Assert.AreEqual("11", BinaryAddition.Kata.AddBinary(1, 2), "Should return \"11\" for 1 + 2");
        }
    }

}
