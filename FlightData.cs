using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightDetector
{
    class FlightData
    {
        private readonly Dictionary<int, double[]> _data;

        private AnomalyDetectorType _detectorType;

        public Dictionary<string, KeyValuePair<string, float[]>> CorrelationData
        {
            get;
            private set;
        }

        private int[] _allAnomaliesTimeSteps;

        public int[] AllAnomaliesTimeSteps
        {
            get => this._allAnomaliesTimeSteps;
            set => this._allAnomaliesTimeSteps = value;
        }

        public int Size { get; }

        public string[] Features { get; }

        public FlightData(XmlParser xmlParser, string xmlPath, CsvParser csvParser, string csvPath)
        {
            xmlParser.UploadXml(xmlPath);
            this.Features = xmlParser.GetFeatures();
            string[] lines = csvParser.GetCsvLines(csvPath);
            this.Size = lines.Length;
            this._data = csvParser.DataToDictionary(lines);
        }

        public FlightData(CsvParser csvParser, string validFlightPath, string flightToInspectPath, AnomalyDetector detector, string[] features)
        {
            this.Features = features;
            string[] lines = csvParser.GetCsvLines(flightToInspectPath);
            this.Size = lines.Length;
            this._data = csvParser.DataToDictionary(lines);
            string validFlightNewPath = "files/train_flight.csv";
            string flightToInspectNewPath = "files/test_flight.csv";
            CreateCsvWithTitles(validFlightPath, validFlightNewPath, features, csvParser);
            CreateCsvWithTitles(flightToInspectPath, flightToInspectNewPath, features, csvParser);
            BuildCorrelationData(detector, validFlightNewPath, flightToInspectNewPath);

            this._detectorType = detector.DetectorType;
            this.AllAnomaliesTimeSteps = detector.GetAllAnomaliesTimesSteps();
        }

        public double[] GetTimeStepData(int timeStep)
        {
            return this._data[timeStep];
        }

        public double GetFeatureValue(int timeStep, string feature)
        {
            int featureIndex = GetFeatureIndex(feature);
            return this._data[timeStep][featureIndex];
        }

        public double[] GetFeatureAllValues(string feature)
        {
            double[] allValues = new double[this.Size];
            for (int i = 0; i < this.Size; i++)
            {
                allValues[i] = GetFeatureValue(i, feature);
            }

            return allValues;
        }

        public string GetCorrelatedFeature(string feature)
        {
            return this.CorrelationData[feature].Key; // the key of correlation data is the correlative feature
        }

        public float[] GetLinearRegression(string feature)
        {
            return this.CorrelationData[feature].Value; // the value of correlation is the linear regression
        }

        public AnomalyDetectorType GetDetectorType()
        {
            return this._detectorType;
        }


        // Private Methods


        private int GetFeatureIndex(string feature)
        {
            for (int i = 0; i < this.Size; i++)
            {
                if (feature == Features[i])
                {
                    return i;
                }
            }

            throw new Exception("Feature does not exist");
        }

        private void BuildCorrelationData(AnomalyDetector detector, string validFlightPath, string flightToDetectPath)
        {
            detector.learnNormalFromCSV(new StringBuilder(validFlightPath));
            detector.detectFromCSV(new StringBuilder(flightToDetectPath));

            this.CorrelationData = new Dictionary<string, KeyValuePair<string, float[]>>();
            foreach (var feature in this.Features)
            {
                string correlatedFeature = detector.GetMostCorrelativeFeature(feature);
                float[] threshold;

                if (detector.DetectorType == AnomalyDetectorType.LinearRegression)
                {
                    threshold = detector.GetLinearRegression(feature);
                }
                else
                {
                    threshold = detector.GetMinCircle(feature);
                }
                KeyValuePair<string, float[]> correlationData =
                    new KeyValuePair<string, float[]>(correlatedFeature, threshold);
                this.CorrelationData.Add(feature, correlationData);
            }
        }

        private void CreateCsvWithTitles(string originalFilePath, string newFilePath, string[] features, CsvParser csvParser)
        {
            string featuresLine = string.Join(",", features);
            string[] dataLines = csvParser.GetCsvLines(originalFilePath);
            string[] allLines = new string[dataLines.Length + 1];
            allLines[0] = featuresLine;
            for (int i = 1; i < allLines.Length; i++)
            {
                allLines[i] = dataLines[i - 1];

            }
            csvParser.CreateFile(newFilePath, allLines);
        }
    }
}
