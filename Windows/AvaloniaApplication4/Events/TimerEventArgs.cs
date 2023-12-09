using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventArgs
{
   public class TimerEventArgs
    {
        public int currentTime;
        public int newTime;


        public TimerEventArgs(int currentTime)
        { this.currentTime = currentTime;
            this.newTime = currentTime;
        }

        public TimerEventArgs(int currentTime, int newTime )
        {
            this.currentTime = currentTime;
            this.newTime = newTime;
        }

    }
}
