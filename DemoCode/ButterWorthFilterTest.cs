using MathNet.Filtering;
using MathNet.Filtering.IIR;
using System.Text.Json;
using Xunit.Abstractions;

namespace DemoCode;

public class ButterWorthFilterTest
{
    private readonly ITestOutputHelper _output;
    public delegate double[] FilterImplementation(double[] inputArray, double sampleRate);
    public ButterWorthFilterTest(ITestOutputHelper output)
    {
        _output = output;
    }

    //[Fact]
    //public void Test_Butterworth_Filter_MathNet_Implementation()
    //{
    //    // Call the reusable test with the specific filter implementation
    //    RunFilterTest((inputArray, sampleRate) =>
    //    {
    //        var filter = new ButterworthFilter();
    //        return filter.ApplyHighPassFilter(inputArray, sampleRate);
    //    });
    //}


    [Fact]
    public void Test1()
    {
        // Call the reusable test with the specific filter implementation
        RunFilterTest((inputArray, sampleRate) =>
        {
            var filter = new ButterworthFilter();
            return filter.ButterWorth1(inputArray, sampleRate);
        });
    }

    [Fact]
    public void Test2()
    {
        // Call the reusable test with the specific filter implementation
        RunFilterTest((inputArray, sampleRate) =>
        {
            var filter = new ButterworthFilter();
            return filter.ButterWorth2(inputArray, sampleRate);
        });
    }




    private void RunFilterTest(FilterImplementation filterImplementation)
    {
        GetDataFromFiles(out double[] inputArray, out double[] expectedArray);

        // Validate the lengths of the arrays
        Assert.Equal(expectedArray.Length, inputArray.Length);
        _output.WriteLine($"Input and expected arrays have the same length: {expectedArray.Length}");

        // Sample rate for the test (2048 Hz as implied by the file name)
        double sampleRate = 2048.0;

        // Apply the filter using the provided implementation
        double[] actualArray = filterImplementation(inputArray, sampleRate);

        // Validate the output length
        Assert.Equal(expectedArray.Length, actualArray.Length);
        _output.WriteLine($"Filtered array has the correct length: {actualArray.Length}");

        // Collect results of mismatches and handle consecutive failures
        var mismatches = new System.Text.StringBuilder();
        bool allMatch = true;
        int consecutiveFailures = 0;
        const int failureThreshold = 75;

        for (int i = 0; i < expectedArray.Length; i++)
        {
            double difference = Math.Abs(expectedArray[i] - actualArray[i]);
            bool isMatch = difference < 1e-6;

            if (!isMatch)
            {
                consecutiveFailures++;
                var differanceAsPercent = difference / expectedArray[i];
                string mismatchInfo = $"Node {i} Mismatch - %Off: {differanceAsPercent:P2}, Expected: {expectedArray[i]}, Actual: {actualArray[i]}, Difference: {difference} ";
                _output.WriteLine(mismatchInfo);
                mismatches.AppendLine(mismatchInfo);
                allMatch = false;

                if (consecutiveFailures >= failureThreshold)
                {
                    _output.WriteLine($"Test aborted after {failureThreshold} consecutive mismatches.");
                    mismatches.AppendLine($"Test aborted after {failureThreshold} consecutive mismatches.");
                    break;
                }
            }
            else
            {
                consecutiveFailures = 0; // Reset the counter on a match
            }
        }

        // Assert at the end to allow all comparisons to run or handle early exit
        if (!allMatch)
        {
            Assert.True(allMatch, $"There were mismatches:\n{mismatches}");
        }
    }

    private static void GetDataFromFiles(out double[] inputArray, out double[] expectedArray)
    {
        // File paths
        string inputFilePath = "n=1650_2048hz.json";
        string expectedFilePath = "n=1650_2048hz_filtered.json";

        // Read input and expected data from JSON files
        double[][] nestedInputArray = JsonSerializer.Deserialize<double[][]>(File.ReadAllText(inputFilePath));
        double[][] nestedExpectedArray = JsonSerializer.Deserialize<double[][]>(File.ReadAllText(expectedFilePath));

        // Flatten the nested arrays
        var number = 5;
        inputArray = nestedInputArray.Skip(1).Take(number).SelectMany(x => x).ToArray();
        expectedArray = nestedExpectedArray.Skip(1).Take(number).SelectMany(x => x).ToArray();
    }
}


public class ButterworthFilter
{
    /// <summary>
    /// Applies a high-pass Butterworth filter to the input array.
    /// </summary>
    /// <param name="arry">The input data array.</param>
    /// <param name="sampleRate">The sampling rate of the data.</param>
    /// <returns>The filtered data array.</returns>
    public double[] ApplyHighPassFilter(double[] arry, double sampleRate)
    {
    
        if (arry == null || arry.Length == 0)
            throw new ArgumentException("Input array cannot be null or empty.", nameof(arry));

        int burstLen = arry.Length;
        double nyquist = sampleRate / 2.0;

        // Compute cutoff frequency
        double logTerm = Math.Max(Math.Log(Math.Pow((double)burstLen / 8192, -3.0 / 2.0)), 1.0);
        double cutoff = 0.01 * nyquist * logTerm;

        // Create a high-pass Butterworth filter of order 1
        var butterworth = OnlineIirFilter.CreateHighpass(ImpulseResponse.Infinite, sampleRate, cutoff);

        // Apply the filter
        double[] filteredArray = new double[arry.Length];
        for (int i = 0; i < arry.Length; i++)
        {
            filteredArray[i] = butterworth.ProcessSample(arry[i]);
        }
        return filteredArray;
    }


    public double[] ButterWorth1(double[] signal, double sampleFreq)
    {
        //var passBandFreq = 4 / (sampleFreq / 2.0);
        //var stopBandFreq = 1 / (sampleFreq / 2.0);
        //var passBandRipple = 1.0;
        //var stopBandAttenuation = 20.0;

        var passBandFreq = 4 ;
        var stopBandFreq = 1 ;
        var passBandRipple = 1.0;
        var stopBandAttenuation = 20.0;



        (double[] numerator, double[] denominator) coefficients = MathNet.Filtering.Butterworth.IirCoefficients.HighPass(stopBandFreq, passBandFreq, passBandRipple, stopBandAttenuation);

        var coeffs = new List<double>();
        foreach (var numerator in coefficients.numerator)
        {
            coeffs.Add(numerator);
        }
        foreach (var denominator in coefficients.denominator)
        {
            coeffs.Add(denominator);
        }
        var filter = new MathNet.Filtering.IIR.OnlineIirFilter(coeffs.ToArray());
        var filteredSignal = filter.ProcessSamples(signal);
        return filteredSignal;
    }



    public double[] ButterWorth2(double[] signal, double sampleFreq)
    {
        //var passBandFreq = 4 / (sampleFreq / 2.0);
        //var stopBandFreq = 1 / (sampleFreq / 2.0);
        //var passBandRipple = 1.0;
        //var stopBandAttenuation = 20.0;

        ////var passBandFreq = 4;
        ////var stopBandFreq = 1;
        ////var passBandRipple = 1.0;
        ////var stopBandAttenuation = 20.0;



        //(double[] numerator, double[] denominator) coefficients = MathNet.Filtering.Butterworth.IirCoefficients.HighPass(stopBandFreq, passBandFreq, passBandRipple, stopBandAttenuation);

        //var coeffs = new List<double>();
        //foreach (var numerator in coefficients.numerator)
        //{
        //    coeffs.Add(numerator);
        //}
        //foreach (var denominator in coefficients.denominator)
        //{
        //    coeffs.Add(denominator);
        //}
        //var filter = new MathNet.Filtering.IIR.OnlineIirFilter(coeffs.ToArray());
        //double[] coff = [0.9845337085968967, -0.9845337085968967, 0.0, 1.0, -0.9690674171937933, 0.0];

        double[] coff = [0.9845337085968967, -0.9845337085968967, 0.0, 1.0, -0.9690674171937933, 0.0];
        var filter = new MathNet.Filtering.IIR.OnlineIirFilter(coff);
     
        var filteredSignal = filter.ProcessSamples(signal);
        return filteredSignal;
    }

}






