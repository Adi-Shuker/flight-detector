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

namespace FlightDetector
{
    /// <summary>
    /// Interaction logic for dataDisplayView.xaml
    /// </summary>
    public partial class dataDisplayView : UserControl
    {
        //dataDisplayViewModel vm;
        public dataDisplayView()
        {
            InitializeComponent();
            //XmlParser xmlParser = new XmlParser();
            //CsvParser csvParser = new CsvParser();
            //string csvPath = "C:\\Users\\Shana\\source\\repos\\FlightDetector\\reg_flight.csv";
            //string xmlPath = "C:\\Users\\Shana\\source\\repos\\FlightDetector\\playback_small.xml";
            //FlightData data = new FlightData(xmlParser, xmlPath, csvParser, csvPath);
            //vm = new dataDisplayViewModel(new dataDisplayModel(data));
            //DataContext = vm;

            //vm.TimeStep = 1;
        }
    }
}