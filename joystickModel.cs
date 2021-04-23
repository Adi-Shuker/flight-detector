using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDetector
{
    class joystickModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;


        //properties

        private FlightData flightData;

        public FlightData FlightData
        {
            get { return flightData; }
            set { }
        }


        // constructor
        public joystickModel(FlightData data)
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