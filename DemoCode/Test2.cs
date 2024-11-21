using MathNet.Filtering;
using MathNet.Filtering.IIR;
using System.Text.Json;
using Xunit.Abstractions;
using FluentAssertions;

namespace DemoCode
{
    public class ButterWorthFilterTest
    {
        private readonly ITestOutputHelper _output;
        public delegate double[] FilterImplementation(double[] inputArray);

        public ButterWorthFilterTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Getting_ButterWorthHighPassCoeffecents()
        {

            const double passBandFreq = .25;
            const double stopBandFreq = .2;
            const double passBandRipple = .5;
            const double stopBandAttenuation = 40.0;

            (double[] numerator, double[] denominator) coefficients = MathNet.Filtering.Butterworth.IirCoefficients.HighPass(
                stopBandFreq, passBandFreq, passBandRipple, stopBandAttenuation);


            _output.WriteLine($"{coefficients}");

        }
        [Fact]
        public void ValidateHighPassFilterCoefficients()
        {
            // Arrange
            double stopBandFreq = 0.2; // Example normalized stopband frequency
            double passBandFreq = 0.25; // Example normalized passband frequency
            double passBandRipple = 0.5; // Passband ripple in dB
            double stopBandAttenuation = 40.0; // Stopband attenuation in dB

            // Act
            (double[] numerator, double[] denominator) coefficients = MathNet.Filtering.Butterworth.IirCoefficients.HighPass(
                stopBandFreq, passBandFreq, passBandRipple, stopBandAttenuation);

            // Expected coefficients (replace with actual expected values for validation)
            double[] expectedNumerator = { 0.9845337085968967, -0.9845337085968967, 0.0 }; // Feedforward coefficients
            double[] expectedDenominator = { 1.0, -0.9690674171937933, 0.0 }; // Feedback coefficients

            // Assert
            _output.WriteLine("Validating denominator coefficients (A)...");
            coefficients.denominator.Should().HaveCount(expectedDenominator.Length, "the denominator (feedback) coefficients should have the expected length");
            coefficients.denominator.Should().BeEquivalentTo(expectedDenominator, options => options.WithStrictOrdering()
                .Using<double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, 1e-6))
                .WhenTypeIs<double>());

            foreach (var (index, value) in coefficients.denominator.Select((v, i) => (i, v)))
            {
                _output.WriteLine($"Denominator Coefficient A[{index}]: {value}");
            }

            _output.WriteLine("Validating numerator coefficients (B)...");
            coefficients.numerator.Should().HaveCount(expectedNumerator.Length, "the numerator (feedforward) coefficients should have the expected length");
            coefficients.numerator.Should().BeEquivalentTo(expectedNumerator, options => options.WithStrictOrdering()
                .Using<double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, 1e-6))
                .WhenTypeIs<double>());

            foreach (var (index, value) in coefficients.numerator.Select((v, i) => (i, v)))
            {
                _output.WriteLine($"Numerator Coefficient B[{index}]: {value}");
            }

            _output.WriteLine("HighPass filter coefficients validated successfully.");
        }






        [Fact]
        public void Cauculate_Filter_and_Apply()
        {
            RunFilterTest(inputArray => new ButterworthFilter().ApplyHighPassFilter(inputArray));
        }

        [Fact]
        public void Apply_Predefined_filter()
        {
            RunFilterTest(inputArray => new ButterworthFilter().ApplyPredefinedFilter(inputArray));
        }

        private void RunFilterTest(FilterImplementation filterImplementation)
        {
            var (inputArrays, expectedArrays) = GetDataFromFiles();

            Assert.Equal(expectedArrays.Length, inputArrays.Length);
            _output.WriteLine($"Number of test cases: {expectedArrays.Length}");

            var mismatches = new System.Text.StringBuilder();
            bool allMatch = true;

            for (int i = 0; i < inputArrays.Length; i++)
            {
                _output.WriteLine($"Checking Block {i} of {inputArrays.Length}");
                double[] actualArray = filterImplementation(inputArrays[i]);
                Assert.Equal(expectedArrays[i].Length, actualArray.Length);

                for (int j = 0; j < actualArray.Length; j++)
                {
                    double difference = Math.Abs(expectedArrays[i][j] - actualArray[j]);
                    if (difference >= 1e-6)
                    {
                        allMatch = false;
                        string mismatchInfo = $"Case {i}, Index {j} - Expected: {expectedArrays[i][j]:F6}, " +
                                              $"Actual: {actualArray[j]:F6}, Diff: {difference:F6}";
                        mismatches.AppendLine(mismatchInfo);
                        _output.WriteLine(mismatchInfo);
                    }
                    if (difference != 0) 
                    {
                        string mismatchInfo = $"Case {i}, Index {j} - Expected: {expectedArrays[i][j]:F6}, " +
                                           $"Actual: {actualArray[j]:F6}, Diff: {difference:F6}";
                        _output.WriteLine(mismatchInfo);
                    }
                }
            }

            if (!allMatch)
            {
                Assert.True(allMatch, $"Mismatches detected:\n{mismatches}");
            }
        }

        private static (double[][] inputArray, double[][] expectedArray) GetDataFromFiles()
        {
            try
            {
                string inputFilePath = "n=1650_2048hz.json";
                string expectedFilePath = "n=1650_2048hz_filtered.json";

                double[][] inputArray = JsonSerializer.Deserialize<double[][]>(File.ReadAllText(inputFilePath));
                double[][] expectedArray = JsonSerializer.Deserialize<double[][]>(File.ReadAllText(expectedFilePath));

                return (inputArray, expectedArray);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error reading or deserializing files.", ex);
            }
        }
    }

    public class ButterworthFilter
    {
        public double[] ApplyHighPassFilter(double[] signal) //this is what we would like but is not working yet

        {
            const double passBandFreq = .25; // related to the cutoff frequency 
            const double stopBandFreq = .2;   
            const double passBandRipple = .5;
            const double stopBandAttenuation = 40.0;

            (double[] numerator, double[] denominator) coefficients = MathNet.Filtering.Butterworth.IirCoefficients.HighPass(
                stopBandFreq, passBandFreq, passBandRipple, stopBandAttenuation);
            
            // Combine numerator and denominator coefficients into a single array
            var combinedCoefficients = coefficients.numerator.Concat(coefficients.denominator).ToArray();

            var filter = new OnlineIirFilter(combinedCoefficients);
            return filter.ProcessSamples(signal);
        }

        public double[] ApplyPredefinedFilter(double[] signal)  // this is the one that works 
        {
            double[] coefficients = [0.9845337085968967, -0.9845337085968967, 0.0, 1.0, -0.9690674171937933, 0.0];
            var filter = new OnlineIirFilter(coefficients);
            return filter.ProcessSamples(signal);
        }
    }
}
