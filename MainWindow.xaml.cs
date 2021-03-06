using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Runtime.InteropServices;

namespace FlightDetector
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        public MainWindow(string validFlightPath, string flightToDetectPath, string dllPath, AnomalyDetectorType detectorType)
        {
            InitializeComponent();
            MainViewModel mainViewModel = new MainViewModel(validFlightPath, flightToDetectPath, detectorType);
            DataContext = mainViewModel;
        }
    }
}
