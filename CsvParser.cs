using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FlightDetector;

namespace FlightDetector
{
    class CsvParser
    {
        public string[] GetCsvLines(string path)
        {
            return File.ReadAllLines(path);
        }

        public Dictionary<int, double[]> DataToDictionary(string[] lines)
        {
            Dictionary<int, double[]> data = new Dictionary<int, double[]>();
            int timeStep = 0;
            foreach (string line in lines)
            {
                double[] values = Array.ConvertAll(line.Split(','), Double.Parse);
                data.Add(timeStep, values);

                timeStep++;
            }
            return data;
        }

        public void CreateFile(string path, string[] data)
        {
            File.WriteAllLines(path, data);
        }
    }
}
