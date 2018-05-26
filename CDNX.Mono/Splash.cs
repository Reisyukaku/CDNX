using System;
using System.Windows.Forms;

namespace CDNX.Mono
{
    public partial class Splash : Form
    {
        private readonly Timer t = new Timer();

        public Splash()
        {
            this.InitializeComponent();
            this.t.Tick += new EventHandler(this.TimerEventProcessor);
            this.t.Interval = 1800;
            this.t.Start();
        }

        private void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
        {
            this.t.Stop();
            this.Close();
        }
    }
}
