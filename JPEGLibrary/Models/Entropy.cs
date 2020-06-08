using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace JPEGLibrary.Models
{
    public class Entropy
    {
        private int pixelCount;

        public double R { get; set; }
        public double G { get; set; }
        public double B { get; set; }
        public double Y { get; set; }
        public double Cb { get; set; }
        public double Cr { get; set; }
        public double Y_dct { get; set; }
        public double Cb_dct { get; set; }
        public double Cr_dct { get; set; }
        public double Y_quanti { get; set; }
        public double Cb_quanti { get; set; }
        public double Cr_quanti { get; set; }

        public Entropy(int width, int height)
        {
            pixelCount = width * height;
        }

        public double CalculateEntropyFromHistogram(Dictionary<int, int> histogram)
        {
            double entropy = 0;
            foreach (var pixel in histogram)
            {
                double anzahl = pixel.Value;

                double p = anzahl / pixelCount;
                entropy += p * Math.Log(p, 2);
            }

            return -entropy;
        }

        public double CalculateEntropyFromHistogram(Dictionary<double, int> histogram)
        {
            double entropy = 0;
            foreach (var pixel in histogram)
            {
                double anzahl = pixel.Value;

                double p = anzahl / pixelCount;
                entropy += p * Math.Log(p, 2);
            }

            return -entropy;
        }

        public void PopulateHistogram(Dictionary<int, int> histogram, double pixel)
        {
            int pixel_rounded = (int)Math.Round(pixel);

            if (pixel_rounded == 0)
            {
                return;
            }

            if (histogram.ContainsKey(pixel_rounded) == false)
            {
                histogram[pixel_rounded] = 1;
            }
            else
            {
                histogram[pixel_rounded]++;
            }
        }

        public void PopulateHistogramWithDouble(Dictionary<double, int> histogram, double pixel)
        {
            if (pixel == 0)
            {
                return;
            }

            if (histogram.ContainsKey(pixel) == false)
            {
                histogram[pixel] = 1;
            }
            else
            {
                histogram[pixel]++;
            }
        }

    }

    public static class PropertyPrinter
    {
        public static IEnumerable<string> GetPropertyValues<T>(this T inputClass) where T : class
        {
            foreach (var property in inputClass.GetType().GetProperties())
            {
                yield return $"{property.Name}: {property.GetValue(inputClass)}";
            }
        }
    }


}
