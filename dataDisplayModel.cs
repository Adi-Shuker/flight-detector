using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDetector
{
    class dataDisplayModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private FlightData flightData;
        public FlightData FlightData
        {
            get { return flightData; }
            set { }
        }


        // constructor
        public dataDisplayModel(FlightData data)
        {
            flightData = data;
        }


        // call the event propertyChanged when any property of class is changed
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null) //check if event is null or not, if not, we proceed.
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

    }

}