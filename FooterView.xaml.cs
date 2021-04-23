using FlightDetector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
    /// Interaction logic for Footer.xaml
    /// </summary>
    public partial class FooterView : UserControl
    {
        FooterViewModel vm;
        public FooterView()
        {
            InitializeComponent();
            // vm = new FooterViewModel(new MyFooterModel(new Client()));
            // DataContext = vm;
            //vm.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            //{
            //};
            ComboBox speeds = new ComboBox();
        }

        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            ((FooterViewModel)this.DataContext).VM_play = true;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Handle();

        }
        private void Handle()
        {
           
            switch (speeds.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None).Last())
            {
                case "0.25":
                    ((FooterViewModel)this.DataContext).VM_PlaybackSpeed = 400;
                    break;
                case "0.5":
                    ((FooterViewModel)this.DataContext).VM_PlaybackSpeed = 200;
                    break;
                case "0.75":
                    ((FooterViewModel)this.DataContext).VM_PlaybackSpeed = 133;
                    break;
                case "Normal":
                    ((FooterViewModel)this.DataContext).VM_PlaybackSpeed = 100;
                    break;
                case "1.25":
                    ((FooterViewModel)this.DataContext).VM_PlaybackSpeed = 80;
                    break;
                case "1.5":
                    ((FooterViewModel)this.DataContext).VM_PlaybackSpeed = 66;
                    break;
                case "1.75":
                    ((FooterViewModel)this.DataContext).VM_PlaybackSpeed = 57;
                    break;
                case "2":
                    ((FooterViewModel)this.DataContext).VM_PlaybackSpeed = 50;
                    break;
            }
        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ((FooterViewModel)this.DataContext).VM_NextLine = Convert.ToInt32(slider.Value.ToString());
        }

        



        // private void Anomalies_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)//added
        // {
        //     Handles();
        // 
        // }
        // 
        // private void Handles() //added
        // {
        //     string anomalie = anomalies.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None).Last();
        //     int time = int.Parse(anomalie);
        //     ((FooterViewModel)this.DataContext).VM_NextLine = time;
        // }


        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            ((FooterViewModel)this.DataContext).stopPlay();
        }

        private void skipToTheStartButton_Click(object sender, RoutedEventArgs e)
        {
            ((FooterViewModel)this.DataContext).VM_NextLine = 1;
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            if ((((FooterViewModel)this.DataContext).VM_NextLine - 30) < 0)
            {
                ((FooterViewModel)this.DataContext).VM_NextLine = 0;
            }
            else
            {
                ((FooterViewModel)this.DataContext).VM_NextLine = ((FooterViewModel)this.DataContext).VM_NextLine - 30;
            }

        }

        private void fastForwardButton_Click(object sender, RoutedEventArgs e)
        {

            if ((((FooterViewModel)this.DataContext).VM_NextLine + 30) > ((FooterViewModel)this.DataContext).VM_MaxValueSlider)
            {
                ((FooterViewModel)this.DataContext).VM_NextLine = ((FooterViewModel)this.DataContext).VM_MaxValueSlider - 1;
            }
            else
            {
                ((FooterViewModel)this.DataContext).VM_NextLine = ((FooterViewModel)this.DataContext).VM_NextLine + 30;
            }
        }

        private void skipToTheEndButton_Click(object sender, RoutedEventArgs e)
        {
            ((FooterViewModel)this.DataContext).VM_NextLine = ((FooterViewModel)this.DataContext).VM_MaxValueSlider;
        }

        private void stopButton_Click_1(object sender, RoutedEventArgs e)
        {
            ((FooterViewModel)this.DataContext).stopPlay();
        }
    }
}
