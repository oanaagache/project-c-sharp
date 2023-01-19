using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_Client1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_logout_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void btn_appointment_Click(object sender, EventArgs e)
        {
            Appointment appointment = new Appointment();
            appointment.Show(); //deschide form Appointments
        }

        private void btn_review_Click(object sender, EventArgs e)
        {
            Review review = new Review();
            review.Show(); //deschide form Review

        }
    }
}
