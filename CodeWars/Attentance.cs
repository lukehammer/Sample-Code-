using FluentAssertions;
using Xunit;

namespace CodeWars
{
    public class Attentance
    {

        [Fact]
        public void TestOne()
        {
            Attentance.Check("PPALLP").Should().Be(true);
            Attentance.Check("PPALLL").Should().Be(false);
            Attentance.Check("PPALALA").Should().Be(false);
            Attentance.Check("LALL").Should().Be(true);
            Attentance.Check("AA").Should().Be(false);
        }

        private static object Check(string s)
        {
            var timesLateInARow = 0;
            var totalAbstances = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == 'L') timesLateInARow++;
                if (timesLateInARow == 3) return false;
                if (s[i] == 'A') totalAbstances++;
                if (totalAbstances == 2) return false;
                if (s[i] != 'L') timesLateInARow = 0;
            }
            return true;
        }
    }


}
