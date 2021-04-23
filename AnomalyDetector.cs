using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace FlightDetector
{
    public enum AnomalyDetectorType
    {
        LinearRegression, MinCircle
    }

    class AnomalyDetector
    {
        private string _dllPath;
        public string CsvPath { get; private set; }
        private AnomalyDetectorType _detectorType;
        IntPtr anomalyDetector;

        public AnomalyDetectorType DetectorType
        {
            get => this._detectorType;
            set => this._detectorType = value;
        }
        //function from the DLL
        [DllImport("files\\SimpleAnomalyDetector.dll")]
        public static extern IntPtr create();

        //learn normal
        [DllImport("files\\SimpleAnomalyDetector.dll")]
        public static extern int getMostCorrelativeFeature(IntPtr a, StringBuilder feature);

        [DllImport("files\\SimpleAnomalyDetector.dll")]
        public static extern int getLenOfStringWrapper(IntPtr a);

        [DllImport("files\\SimpleAnomalyDetector.dll")]
        public static extern char getCharByIndexStringWrapper(IntPtr a, int x);

        [DllImport("files\\SimpleAnomalyDetector.dll")]
        public static extern int learnNormalFromCSV(IntPtr a, StringBuilder CSVfileName);

        //get Linear  Regression 
        [DllImport("files\\SimpleAnomalyDetector.dll")]
        public static extern void getLinearRegression(IntPtr a, StringBuilder feature);

        [DllImport("files\\SimpleAnomalyDetector.dll")]
        public static extern int getLenOfFloatArrayWrapper(IntPtr a);

        [DllImport("files\\SimpleAnomalyDetector.dll")]
        public static extern float getFloatArrayByIndex(IntPtr a, int index);

        //detect
        [DllImport("files\\SimpleAnomalyDetector.dll")]
        public static extern int detectFromCSV(IntPtr a, StringBuilder CSVfileName);

        [DllImport("files\\SimpleAnomalyDetector.dll")]
        public static extern int getAllAnomalyTimestamp(IntPtr a);

        [DllImport("files\\SimpleAnomalyDetector.dll")]
        public static extern int getLenOfArrayWrapper(IntPtr a);

        [DllImport("files\\SimpleAnomalyDetector.dll")]
        public static extern int getAnomalyByIndex(IntPtr a, int index);

        //CircleDetector
        [DllImport("files\\MinCircleDll.dll")]
        public static extern IntPtr createCircleDetector();

        [DllImport("files\\MinCircleDll.dll")]
        public static extern int learnNormalFromCSVCircle(IntPtr a, StringBuilder CSVfileName);

        [DllImport("files\\MinCircleDll.dll")]
        public static extern int getMostCorrelativeFeatureCircle(IntPtr a, StringBuilder feature);

        [DllImport("files\\MinCircleDll.dll")]
        public static extern int getLenOfStringWrapperCircle(IntPtr a);

        [DllImport("files\\MinCircleDll.dll")]
        public static extern char getCharByIndexStringWrapperCircle(IntPtr a, int x);

        [DllImport("files\\MinCircleDll.dll")]
        public static extern void getRegressionCircle(IntPtr a, StringBuilder feature);

        [DllImport("files\\MinCircleDll.dll")]
        public static extern int getLenOfFloatArrayWrapperCircle(IntPtr a);

        [DllImport("files\\MinCircleDll.dll")]
        public static extern float getFloatArrayByIndexCircle(IntPtr a, int index);

        [DllImport("files\\MinCircleDll.dll")]
        public static extern int detectFromCSVCircle(IntPtr a, StringBuilder CSVfileName);

        [DllImport("files\\MinCircleDll.dll")]
        public static extern int getAllAnomalyTimestampCircle(IntPtr a);
        [DllImport("files\\MinCircleDll.dll")]
        public static extern int getLenOfArrayWrapperCircle(IntPtr a);
        [DllImport("files\\MinCircleDll.dll")]
        public static extern int getAnomalyByIndexCircle(IntPtr a, int index);

        public AnomalyDetector(string dllPath, string csvPath, AnomalyDetectorType detectorType)
        {

            this._dllPath = dllPath;
            this.CsvPath = csvPath;
            this.DetectorType = detectorType;
            if (this.DetectorType.Equals(AnomalyDetectorType.LinearRegression))
            {
                this.anomalyDetector = create(); //create simple anomaly detector
            }
            else
            {
                this.anomalyDetector = createCircleDetector();
            }
        }
        public int learnNormalFromCSV(StringBuilder CSVfileName)
        {
            if (this.DetectorType.Equals(AnomalyDetectorType.LinearRegression))
            {
                return learnNormalFromCSV(anomalyDetector, CSVfileName);
            }
            else
            {
                return learnNormalFromCSVCircle(anomalyDetector, CSVfileName);
            }
        }
        public int getLenOfStringBuilder()
        {
            if (this.DetectorType.Equals(AnomalyDetectorType.LinearRegression))
            {
                return getLenOfStringWrapper(anomalyDetector);
            }
            else
            {
                return getLenOfStringWrapperCircle(anomalyDetector);
            }
        }

        public char getCharByIndex(int index)
        {
            if (this.DetectorType.Equals(AnomalyDetectorType.LinearRegression))
            {
                return getCharByIndexStringWrapper(anomalyDetector, index);
            }
            else
            {
                return getCharByIndexStringWrapperCircle(anomalyDetector, index);
            }
        }
        public string GetMostCorrelativeFeature(string feature)
        {
            if (this.DetectorType.Equals(AnomalyDetectorType.LinearRegression))
            {
                getMostCorrelativeFeature(anomalyDetector, new StringBuilder(feature));
                int strLen = getLenOfStringWrapper(anomalyDetector);
                string s = "";
                for (int i = 0; i < strLen; i++)
                {
                    char c = getCharByIndexStringWrapper(anomalyDetector, i);
                    s += c.ToString();
                }
                return s;
            }
            else
            {
                getMostCorrelativeFeatureCircle(anomalyDetector, new StringBuilder(feature));
                int strLen = getLenOfStringWrapperCircle(anomalyDetector);
                string s = "";
                for (int i = 0; i < strLen; i++)
                {
                    char c = getCharByIndexStringWrapperCircle(anomalyDetector, i);
                    s += c.ToString();
                }
                return s;
            }
        }
        //helper function for GetLinearRegression

        public int getLenOfFloatArrayWrapper()
        {
            if (this.DetectorType.Equals(AnomalyDetectorType.LinearRegression))
            {
                return getLenOfFloatArrayWrapper(anomalyDetector);
            }
            else
            {
                return getLenOfFloatArrayWrapperCircle(anomalyDetector);
            }
        }

        public float getFloatArrayByIndex(int index)
        {
            if (this.DetectorType.Equals(AnomalyDetectorType.LinearRegression))
            {
                return getFloatArrayByIndex(anomalyDetector, index);
            }
            else
            {
                return getFloatArrayByIndexCircle(anomalyDetector, index);
            }
        }

        //before using need to run learnNormalFromCSV(StringBuilder CSVfileName)
        public float[] GetLinearRegression(string feature)
        {
            if (this.DetectorType.Equals(AnomalyDetectorType.LinearRegression))
            {
                getLinearRegression(anomalyDetector, new StringBuilder(feature));
            }
            else
            {

                getRegressionCircle(anomalyDetector, new StringBuilder(feature));
            }
            int length = getLenOfFloatArrayWrapper();
            float[] line = new float[length];
            for (int i = 0; i < length; i++)
            {
                line[i] = getFloatArrayByIndex(i);
            }
            return line;
        }

        public float[] GetMinCircle(string feature)
        {
            getRegressionCircle(anomalyDetector, new StringBuilder(feature));
            int length = getLenOfFloatArrayWrapper();
            float[] line = new float[length];
            for (int i = 0; i < length; i++)
            {
                line[i] = getFloatArrayByIndex(i);
            }
            return line;
        }

        public int detectFromCSV(StringBuilder CSVfileName)
        {
            if (this.DetectorType.Equals(AnomalyDetectorType.LinearRegression))
            {
                return detectFromCSV(anomalyDetector, CSVfileName);
            }
            else
            {
                return detectFromCSVCircle(anomalyDetector, CSVfileName);
            }
        }

        public int getLenOfArrayWrapper()
        {
            if (this.DetectorType.Equals(AnomalyDetectorType.LinearRegression))
            {
                return getLenOfArrayWrapper(anomalyDetector);
            }
            else
            {
                return getLenOfArrayWrapperCircle(anomalyDetector);
            }
        }
        public int getAnomalyByIndex(int index)
        {
            if (this.DetectorType.Equals(AnomalyDetectorType.LinearRegression))
            {
                return getAnomalyByIndex(anomalyDetector, index);
            }
            else
            {
                return getAnomalyByIndexCircle(anomalyDetector, index);
            }
        }

        public int[] GetAllAnomaliesTimesSteps()
        {
            if (this.DetectorType.Equals(AnomalyDetectorType.LinearRegression))
            {
                getAllAnomalyTimestamp(anomalyDetector);







            }
            else
            {
                getAllAnomalyTimestampCircle(anomalyDetector);
            }
            int length = getLenOfArrayWrapper();
            int[] anomalies = new int[length];
            for (int i = 0; i < length; i++)
            {
                anomalies[i] = getAnomalyByIndex(i);
            }
            return anomalies;

        }
    }
}
