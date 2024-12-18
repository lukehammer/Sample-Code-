//using MathNet.Filtering;
//using MathNet.Filtering.IIR;
//using System.Text.Json;
//using Xunit.Abstractions;

//namespace DemoCode;

//public class ButterWorthFilterTest
//{
//    private readonly ITestOutputHelper _output;
//    public delegate double[] FilterImplementation(double[] inputArray);
//    public ButterWorthFilterTest(ITestOutputHelper output)
//    {
//        _output = output;
//    }

//    [Fact]
//    public void Test1()
//    {
//        // Call the reusable test with the specific filter implementation
//        RunFilterTest((inputArray) =>
//        {
//            var filter = new ButterworthFilter();
//            return filter.ButterWorth1(inputArray);
//        });
//    }

//    [Fact]
//    public void Test2()
//    {
//        // Call the reusable test with the specific filter implementation
//        RunFilterTest((inputArray) =>
//        {
//            var filter = new ButterworthFilter();
//            return filter.ButterWorth2(inputArray);
//        });
//    }




//    private void RunFilterTest(FilterImplementation filterImplementation)
//    {
//        GetDataFromFiles(out double[][] inputArrays, out double[][] expectedArrays);

//        // Validate the lengths of the arrays
//        Assert.Equal(expectedArrays.Length, inputArrays.Length);
//        _output.WriteLine($"Input and expected arrays have the same length: {expectedArrays.Length}");

//        // Sample rate for the test (2048 Hz as implied by the file name)
//        double sampleRate = 2048.0;

//        // Apply the filter using the provided implementation

//        var mismatches = new System.Text.StringBuilder();
//        bool allMatch = true;
//        for (int i = 0; i < inputArrays.Length; i++)
//        {
//            double[] actualArray = filterImplementation(inputArrays[i]);
//            // Validate the output length
//            Assert.Equal(expectedArrays[i].Length, actualArray.Length);
//            _output.WriteLine($"Filtered array {i} has the correct length: {actualArray.Length}");

//            // Collect results of mismatches and handle consecutive failures
//            int consecutiveFailures = 0;
//            const int failureThreshold = 75;

//            for (int j = 0; j < expectedArrays.Length; j++)
//            {
//                double difference = Math.Abs(expectedArrays[i][j] - actualArray[j]);
//                bool isMatch = difference < 1e-6;

//                if (!isMatch)
//                {
//                    consecutiveFailures++;
//                    var differanceAsPercent = difference / expectedArrays[i][j];
//                    string mismatchInfo = $"Node {j} Mismatch - %Off: {differanceAsPercent:P2}, Expected: {expectedArrays[j]}, Actual: {actualArray[j]}, Difference: {difference} ";
//                    _output.WriteLine(mismatchInfo);
//                    mismatches.AppendLine(mismatchInfo);
//                    allMatch = false;

//                    if (consecutiveFailures >= failureThreshold)
//                    {
//                        _output.WriteLine($"Test aborted after {failureThreshold} consecutive mismatches.");
//                        mismatches.AppendLine($"Test aborted after {failureThreshold} consecutive mismatches.");
//                        break;
//                    }
//                }
//                else
//                {
//                    consecutiveFailures = 0; // Reset the counter on a match
//                }
//            }
//        }

//        // Assert at the end to allow all comparisons to run or handle early exit
//        if (!allMatch)
//        {
//            Assert.True(allMatch, $"There were mismatches:\n{mismatches}");
//        }
//    }

//    private static void GetDataFromFiles(out double[][] inputArray, out double[][] expectedArray)
//    {
//        // File paths
//        string inputFilePath = "n=1650_2048hz.json";
//        string expectedFilePath = "n=1650_2048hz_filtered.json";

//        // Read input and expected data from JSON files
//        inputArray = JsonSerializer.Deserialize<double[][]>(File.ReadAllText(inputFilePath));
//        expectedArray = JsonSerializer.Deserialize<double[][]>(File.ReadAllText(expectedFilePath));
//    }
//}


//public class ButterworthFilter
//{
//    public double[] ButterWorth1(double[] signal)
//    {
//        var passBandFreq = 4 ;
//        var stopBandFreq = 1 ;
//        var passBandRipple = 1.0;
//        var stopBandAttenuation = 20.0;

//        (double[] numerator, double[] denominator) coefficients = MathNet.Filtering.Butterworth.IirCoefficients.HighPass(stopBandFreq, passBandFreq, passBandRipple, stopBandAttenuation);

//        var coeffs = new List<double>();
//        foreach (var numerator in coefficients.numerator)
//        {
//            coeffs.Add(numerator);
//        }
//        foreach (var denominator in coefficients.denominator)
//        {
//            coeffs.Add(denominator);
//        }
//        var filter = new MathNet.Filtering.IIR.OnlineIirFilter(coeffs.ToArray());
//        var filteredSignal = filter.ProcessSamples(signal);
//        return filteredSignal;
//    }

//    public double[] ButterWorth2(double[] signal)
//    {
//        double[] coff = [0.9845337085968967, -0.9845337085968967, 0.0, 1.0, -0.9690674171937933, 0.0];
//        var filter = new MathNet.Filtering.IIR.OnlineIirFilter(coff);

//        var filteredSignal = filter.ProcessSamples(signal);
//        return filteredSignal;
//    }

//}






