using System;
using System.Windows.Forms;

namespace CDNX {
    public partial class Splash : Form {

        Timer t = new Timer();

        public Splash() {
            InitializeComponent();
            t.Tick += new EventHandler(TimerEventProcessor);
            t.Interval = 1800;
            t.Start();
        }

        private void TimerEventProcessor(Object myObject, EventArgs myEventArgs) {
            t.Stop();
            Close();
        }
    }
}
