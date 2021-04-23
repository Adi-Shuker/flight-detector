using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDetector
{
    class FooterViewModel: INotifyPropertyChanged
    {

        private int[] anomaliesList;  //added
        public int[] VM_AnomaliesList
        {
            get { return anomaliesList; }
            set
            {
                anomaliesList = value;
                NotifyPropertyChanged(nameof(VM_AnomaliesList));
            }
        }

        private int maxValueSlider;
        public int VM_MaxValueSlider
        {
            get { return maxValueSlider; }
            set
            {
                maxValueSlider = value;
                NotifyPropertyChanged("VM_MaxValueSlider");
            }
        }

        private int selectedAnomaly;

        public int VM_SelectedAnomaly
        {
            get { return selectedAnomaly; }
            set
            {
                selectedAnomaly = value;
                this.VM_NextLine = selectedAnomaly;
                NotifyPropertyChanged(nameof(VM_SelectedAnomaly));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private MyFooterModel model;
        public FooterViewModel(MyFooterModel model, int[] anomaliesList)
        {
            if (anomaliesList.Length > 0)
            {
                this.VM_SelectedAnomaly = anomaliesList[0];
            }
            this.VM_AnomaliesList = anomaliesList;
            this.model = model;
            model.connect("127.0.0.1", 5400);
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
            this.VM_MaxValueSlider = 0;
        }
        private int playbackSpeed;
        public int VM_PlaybackSpeed
        {
            get { return playbackSpeed; }
            set
            {
                playbackSpeed = value;
                model.PlaybackSpeed= value;
            }
        }
        private Boolean play;
        public Boolean VM_play
        {
            get { return play; }
            set { 
                play = value;
                model.play();
            }
        }

        public int VM_NextLine
        {
            get { return model.NextLine; }
            set {
                if (model != null)
                {
                    model.nextLine = value;
                }
                
            }
        }

        public void stopPlay()
        {
            model.stopPlay();
        }

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
