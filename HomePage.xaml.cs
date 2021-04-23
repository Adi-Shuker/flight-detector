using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Sockets;

namespace FlightDetector
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        private string validFlightPath;
        private string flightToDetectPath;
        private string dllPath;
        private AnomalyDetectorType detectorType;

        public string[] DetectorTypes { get; set; }

        public string selectedType;

        public string SelectedType
        {
            get => selectedType;
            set
            {
                selectedType = value;
                if (value == "Linear Regression based")
                {
                    this.detectorType = AnomalyDetectorType.LinearRegression;
                }
                else if (value == "Min Circle based")
                {
                    this.detectorType = AnomalyDetectorType.MinCircle;
                }
            }
        }
        public HomePage()
        {
            InitializeComponent();
            ShowsNavigationUI = false;
            DataContext = this;
            this.DetectorTypes = new string[] { "Linear Regression based", "Min Circle based" };
            this.SelectedType = this.DetectorTypes[0];
        }

        private void UploadValidFlightButton_OnClick(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();

            dialog.DefaultExt = ".csv";
            dialog.Filter = "CSV Files (*.csv)|*.csv";

            Nullable<bool> chosenFile = dialog.ShowDialog();

            if (chosenFile == true)
            {
                validFlightPath = dialog.FileName;
            }
        }

        private void UploadDllButton_OnClick(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();

            dialog.DefaultExt = ".dll";
            dialog.Filter = "DLL Files (*.dll)|*.dll";

            Nullable<bool> chosenFile = dialog.ShowDialog();

            if (chosenFile == true && IsDllFileNameValid(dialog.FileName))
            {
                dllPath = dialog.FileName;
            }
            else
            {
                MessageBox.Show("Please upload DLL according to instructions!", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void UploadFlightToDetectButton_OnClick(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();

            dialog.DefaultExt = ".csv";
            dialog.Filter = "CSV Files (*.csv)|*.csv";

            Nullable<bool> chosenFile = dialog.ShowDialog();

            if (chosenFile == true)
            {
                flightToDetectPath = dialog.FileName;
            }
        }

        private void NavigateToMainPageButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (validFlightPath == null || flightToDetectPath == null || dllPath == null)
            {
                MessageBox.Show("Please upload all files!", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (!IsFlightgearOpen())
            {

                MessageBox.Show("Please open FlightGear and make sure the settings match the instructions", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (!IsDllMatchesType())
            {
                MessageBox.Show("DLL do not matches the chosen type!", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            this.LoadingText.Visibility = Visibility.Visible;
            this.NavigationService.Navigate(new MainPage(validFlightPath, flightToDetectPath, dllPath, detectorType));
        }

        private bool IsFlightgearOpen()
        {
            Process[] pname = Process.GetProcessesByName("fgfs");
            if (pname.Length == 0)
                return false;
            return true;
        }


        private bool IsDllFileNameValid(string fullPath)
        {
            string[] dividedPath = fullPath.Split('\\');
            string fileName = dividedPath[^1];
            return fileName == "SimpleAnomalyDetector.dll" || fileName == "MinCircleDll.dll";
        }

        private bool IsDllMatchesType()
        {
            string[] dividedPath = this.dllPath.Split('\\');
            string fileName = dividedPath[^1];
            return (fileName == "SimpleAnomalyDetector.dll" &&
                    this.detectorType == AnomalyDetectorType.LinearRegression) ||
                   (fileName == "MinCircleDll.dll" && this.detectorType == AnomalyDetectorType.MinCircle);
        }
    }
}
