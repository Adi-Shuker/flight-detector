using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDetector
{
    class MainViewModel
    {
        private const string XML_PATH = "files\\playback_small.xml";
        private const int DEFAULT_SPEED = 100;

        private int _timeStep;

        public int TimeStep
        {
            get { return _timeStep; }
            set
            {
                _timeStep = value;
                UpdateViewModelsTimeStepChanged();
            }
        }

        private int _speed;

        public int Speed
        {
            get { return _speed; }
            set
            {
                _speed = value;
                UpdateViewModlesSpeedChanged();
            }
        }


        private FooterViewModel _footerViewModel;

        public FooterViewModel FooterViewModel
        {
            get { return _footerViewModel; }
            set
            {
                _footerViewModel = value;
            }
        }

        private GraphsViewModel _graphsViewModel;

        public GraphsViewModel GraphsViewModel
        {
            get { return _graphsViewModel; }
            set { _graphsViewModel = value; }
        }


        private joystickViewModel _joystickViewModel;

        public joystickViewModel JoystickViewModel
        {
            get { return _joystickViewModel; }
            set { _joystickViewModel = value; }
        }

        private dataDisplayViewModel _dataDisplayViewModel;

        public dataDisplayViewModel DataDisplayViewModel
        {
            get { return _dataDisplayViewModel; }
            set { _dataDisplayViewModel = value; }
        }



        // Constructor

        public MainViewModel(string validFlightPath, string flightToDetectPath, AnomalyDetectorType detectorType)
        {
            var features = GetFeatures(out var xmlParser, out var xmlPath);
            CsvParser csvParser = new CsvParser();
            // FlightData data = new FlightData(xmlParser, xmlPath, csvParser, validFlightPath);
            AnomalyDetector detector = new AnomalyDetector(validFlightPath, flightToDetectPath, detectorType);
            FlightData data = new FlightData(csvParser, validFlightPath, flightToDetectPath, detector, features);

            this.FooterViewModel = new FooterViewModel(new MyFooterModel(new Client()), data.AllAnomaliesTimeSteps);
            this.FooterViewModel.VM_MaxValueSlider = csvParser.GetCsvLines("files\\test_flight.csv").Length - 2;
            this.FooterViewModel.PropertyChanged += delegate (object sender, PropertyChangedEventArgs args)
            {
                OnFooterPropertyChange(args);
            };
            this.GraphsViewModel = new GraphsViewModel(new GraphsModel(data), (int)1000 / DEFAULT_SPEED);
            this.JoystickViewModel = new joystickViewModel(new joystickModel(data));
            this.DataDisplayViewModel = new dataDisplayViewModel(new dataDisplayModel(data));
        }

        private static string[] GetFeatures(out XmlParser xmlParser, out string xmlPath)
        {
            xmlParser = new XmlParser();
            xmlPath = XML_PATH;
            xmlParser.UploadXml(xmlPath);
            string[] features = xmlParser.GetFeatures();
            return features;
        }

        private void OnFooterPropertyChange(PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "VM_NextLine")
            {
                this.TimeStep = this.FooterViewModel.VM_NextLine;
            }

            if (args.PropertyName == "VM_PlaybackSpeed")
            {
                this.Speed = this.FooterViewModel.VM_PlaybackSpeed;
            }
        }

        private void UpdateViewModelsTimeStepChanged()
        {
            if (this.TimeStep >= this.FooterViewModel.VM_MaxValueSlider)
            {
                return;
            }
            this.GraphsViewModel.TimeStep = this.TimeStep;
            this.DataDisplayViewModel.TimeStep = this.TimeStep;
            this.JoystickViewModel.TimeStep = this.TimeStep;
            this.FooterViewModel.VM_NextLine = this.TimeStep;
        }

        private void UpdateViewModlesSpeedChanged()
        {
            this.GraphsViewModel.TimeStepsPerSecond = (int)1000 / this.Speed;
        }
    }
}
