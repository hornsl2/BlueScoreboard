
using ButtonFiles;
using EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplication4.Models
{
    class CountUpTimer
    {
        private System.Threading.Timer pollTimer;

        public event EventHandler<TimerEventArgs> TimeChanged;
        private int currentSeconds;

        public CountUpTimer()
        {
            pollTimer = new System.Threading.Timer(PollTimer, null, 0, 1000);
            currentSeconds = 0;
        }

        private void PollTimer(object state)
        {
            currentSeconds++;
            TimerUpdateLogic(new TimerEventArgs(currentSeconds));
        }

        public virtual void TimerUpdateLogic(TimerEventArgs time)
        {
            TimeChanged?.Invoke(this, time);

        }

        public int getCurrentSeconds(int seconds)
        {
            return this.currentSeconds;
        }

        public Boolean resetCurrentSeconds()
        {
            if (currentSeconds > 0)
            {
                currentSeconds = 0;
                return true;
            }
            return false;
        }
    }

    
}
