using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FlightDetector
{
    static class StatisticsUtil
    {
        public static double Variance(double[] xArray)
        {
            // Variance = E(X^2) - (E(X))^2
            double expectancy = xArray.Average();
            double[] squared = SquareAllElements(xArray);
            double squaredExpectancy = squared.Average();
            return squaredExpectancy - Math.Pow(expectancy, 2);
        }

        public static double Covariance(double[] xArray, double[] yArray)
        {
            // Covariance = E((X - E(X)) * (Y - E(Y)))
            if (xArray.Length != yArray.Length)
            {
                throw new Exception("Arrays are not of the same size");
            }
            double xExpectancy = xArray.Average();
            double yExpectancy = yArray.Average();
            double sum = 0;
            int length = xArray.Length;
            for (int i = 0; i < length; i++)
            {
                sum += (xArray[i] - xExpectancy) * (yArray[i] - yExpectancy);
            }

            return sum / length;
        }

        public static double Pearson(double[] xArray, double[] yArray)
        {
            // Pearson = cov(X, Y) / (xDeviation * yDeviation)
            double xDeviation = Math.Sqrt(Variance(xArray));
            double yDeviation = Math.Sqrt(Variance(yArray));
            return Covariance(xArray, yArray) / (xDeviation * yDeviation);
        }

        private static double[] SquareAllElements(double[] xArray)
        {
            double[] res = new double[xArray.Length];
            for (int i = 0; i < xArray.Length; i++)
            {
                res[i] = Math.Pow(xArray[i], 2);
            }

            return res;
        }
    }
}
