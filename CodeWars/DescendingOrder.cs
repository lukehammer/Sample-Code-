using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeWars
{
   public static partial class Kata
    {
        public static int DescendingOrder(int num)
        {
            // Bust a move right here
            return 0;
        }
    }




    [TestFixture]
    public partial class Tests
    {
        [Test]
        public void Test0()
        {
            Assert.AreEqual(0, Kata.DescendingOrder(0));
        }
    }
}