using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace CodeWars
{
    public static partial class Kata
    {
        public static int[] ArrayDiff(int[] a, int[] b)
        {

            return a.Where(i => !b.Contains(i)).ToArray();

        }
    }



    [TestFixture]
    public partial class SolutionTest
    {
        [Test]
        public void SampleTest100()
        {
            Assert.AreEqual(new int[] { 2 }, Kata.ArrayDiff(new int[] { 1, 2 }, new int[] { 1 }));
            Assert.AreEqual(new int[] { 2, 2 }, Kata.ArrayDiff(new int[] { 1, 2, 2 }, new int[] { 1 }));
            Assert.AreEqual(new int[] { 1 }, Kata.ArrayDiff(new int[] { 1, 2, 2 }, new int[] { 2 }));
            Assert.AreEqual(new int[] { 1, 2, 2 }, Kata.ArrayDiff(new int[] { 1, 2, 2 }, new int[] { }));
            Assert.AreEqual(new int[] { }, Kata.ArrayDiff(new int[] { }, new int[] { 1, 2 }));
            Assert.AreEqual(new int[] { 3 }, Kata.ArrayDiff(new int[] { 1, 2, 3 }, new int[] { 1, 2 }));
        }
    }
}