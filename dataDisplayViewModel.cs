using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDetector
{
    class dataDisplayViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private dataDisplayModel model;


        // constructor
        public dataDisplayViewModel(dataDisplayModel m)
        {
            this.model = m;

            model.PropertyChanged +=
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

        private double vm_altimeter;
        public double VM_Altimeter
        {
            get { return vm_altimeter; }
            set
            {
                vm_altimeter = value;
                NotifyPropertyChanged(nameof(VM_Altimeter));
            }
        }

        private double vm_airspeed;
        public double VM_Airspeed
        {
            get { return vm_airspeed; }
            set
            {
                vm_airspeed = value;
                NotifyPropertyChanged(nameof(VM_Airspeed));
            }
        }

        private double vm_heading; //kivoun tissa
        public double VM_Heading
        {
            get { return vm_heading; }
            set
            {
                vm_heading = value;
                NotifyPropertyChanged(nameof(VM_Heading));
            }
        }

        private double vm_pitch;
        public double VM_Pitch
        {
            get { return vm_pitch; }
            set
            {
                vm_pitch = value;
                NotifyPropertyChanged(nameof(VM_Pitch));
            }
        }

        private double vm_roll;
        public double VM_Roll
        {
            get { return vm_roll; }
            set
            {
                vm_roll = value;
                NotifyPropertyChanged(nameof(VM_Roll));
            }
        }

        private double vm_sideslip;
        public double VM_Sideslip
        {
            get { return vm_sideslip; }
            set
            {
                vm_sideslip = value;
                NotifyPropertyChanged(nameof(VM_Sideslip));
            }
        }

        /* private double vm_aileron;
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
         */
        // Methods


        void UpdateVars(int time)
        {
            VM_Altimeter = this.model.FlightData.GetFeatureValue(time, "altimeter_indicated-altitude-ft");
            VM_Airspeed = this.model.FlightData.GetFeatureValue(time, "airspeed-kt");
            VM_Heading = this.model.FlightData.GetFeatureValue(time, "heading-deg");
            VM_Pitch = this.model.FlightData.GetFeatureValue(time, "pitch-deg");
            VM_Roll = this.model.FlightData.GetFeatureValue(time, "roll-deg");
            VM_Sideslip = this.model.FlightData.GetFeatureValue(time, "side-slip-deg");
            //VM_Aileron = this.model.FlightData.GetFeatureValue(time, "aileron");
            // VM_Elevator = this.model.FlightData.GetFeatureValue(time, "elevator");
        }

        // call the event propertyChanged when any property of the class is changed
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null) //check if event is null or not, if not, we proceed.
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}