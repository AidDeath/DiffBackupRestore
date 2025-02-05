using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace DiffBackupRestoreWF
{
    public partial class BusyForm : Form
    {
        private delegate void CloseDelegate();

        private static BusyForm busyForm;
        private static string busyText;
        public BusyForm()
        {
            InitializeComponent();
        }

        static public void ShowBusyForm(Point parentLocation)
        {
            if (busyForm != null) return;

            busyForm = new BusyForm();
            busyForm.BusyLabel.Text = busyText;
            Thread thread = new Thread(new ThreadStart(ShowForm));
            thread.IsBackground = true;
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        static private void ShowForm()
        {
            if (busyForm != null) Application.Run(busyForm);
        }

        static public void CloseForm()
        {
            busyForm?.Invoke(new CloseDelegate(BusyForm.CloseFromInternal));
        }

        private static void CloseFromInternal()
        {
            if (busyForm != null)
            {
                busyForm.Close();
                busyForm = null;
            }
        }

        public static void SetBusyText(string text)
        {
            busyText = text;
        }

    }
}
