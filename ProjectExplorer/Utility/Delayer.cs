using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Utility
{
    public class Delayer
    {
        public double TimeElapsed { get; private set; }
        public double Duration { get; private set; }
        private bool singleUse;

        public event EventHandler End;

        public Delayer(double duration, bool singleUse) 
        {
            TimeElapsed = 0;
            Duration = duration;
            this.singleUse = singleUse;
        }

        public void Restart(double duration)
        {
            TimeElapsed = 0;
            Duration = duration;
        }

        public void Restart()
        {
            TimeElapsed = 0;
        }

        public void Update(GameTime gameTime)
        {
            if (TimeElapsed > Duration)
            {
                End?.Invoke(this, EventArgs.Empty);
                if (singleUse)
                {
                    // Clears the event handler
                    // TODO: Test this
                    End = null;
                }
            }
            else
            {
                TimeElapsed += gameTime.ElapsedGameTime.TotalSeconds;
            }
        }
    }
}
