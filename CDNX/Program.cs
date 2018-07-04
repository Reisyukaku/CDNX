using System;
using System.Threading;
using System.Windows.Forms;

namespace CDNX {

	static class Program {
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Splash());
            Application.Run(new Form1());
        }
	}
}
