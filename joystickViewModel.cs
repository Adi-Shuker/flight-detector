using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDetector
{
    class joystickViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private joystickModel model;


        // constructor
        public joystickViewModel(joystickModel m)
        {
            this.model = m;

            model.PropertyChanged +=   // to update the vm when the model change
            delegate (Object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }


        // properties

        private int timeStep;
        public int TimeStep
        {
            get { return timeStep; }
            set
            {
                timeStep = value;
                UpdateVars(timeStep);
            }
        }

        private double vm_aileron;
        public double VM_Aileron
        {
            get { return vm_aileron; }
            set
            {
                vm_aileron = value;
                NotifyPropertyChanged(nameof(VM_Aileron));
            }

        }

        private double vm_elevator;
        public double VM_Elevator
        {
            get { return vm_elevator; }
            set
            {
                vm_elevator = value;
                NotifyPropertyChanged(nameof(VM_Elevator));
            }

        }

        private double vm_rudder;
        public double VM_Rudder
        {
            get { return vm_rudder; }
            set
            {
                vm_rudder = value;
                NotifyPropertyChanged(nameof(VM_Rudder));
            }

        }

        private double vm_throttle;
        public double VM_Throttle
        {
            get { return vm_throttle; }
            set
            {
                vm_throttle = value;
                NotifyPropertyChanged(nameof(VM_Throttle));
            }

        }


        // Methods

        void UpdateVars(int time)
        {
            VM_Rudder = this.model.FlightData.GetFeatureValue(time, "rudder");
            VM_Throttle = this.model.FlightData.GetFeatureValue(time, "throttle 1");

            //VM_Aileron = this.model.FlightData.GetFeatureValue(time, "aileron");
            //VM_Elevator = this.model.FlightData.GetFeatureValue(time, "elevator");

            double ail = this.model.FlightData.GetFeatureValue(time, "aileron");
            double elev = this.model.FlightData.GetFeatureValue(time, "elevator");

            // VM_Aileron = (ail + 1) * 21.25; //ou 42.5
            // VM_Elevator = (1 - elev) * 21.25;

            VM_Aileron = ail * 100;
            VM_Elevator = elev * (-100);
        }


        // call the event propertyChanged when any property of the class is changed
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null) //check if event is null or not, if not, we proceed.
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }


}