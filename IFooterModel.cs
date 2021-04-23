using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDetector
{
    interface IFooterModel: INotifyPropertyChanged
    {
        // connection to the robot
        void connect(string ip, int port);
        void disconnect();
        public void start();
        void play();
        public void stopPlay();
        // activate actuators
        public Boolean getHasDone();

    }
}
