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
    public partial class Instructor_appointment : Form
    {
        Project_Client1.ServiceReference1.WebService1SoapClient service1 = new ServiceReference1.WebService1SoapClient();
        Random rand = new Random();
        public Instructor_appointment()
        {   InitializeComponent();
            label1.Text = "Please complete the fields below.";
            MessageBox.Show("Please remind the id !");
            textBox_id.Text = rand.Next(1000).ToString();
            textBox_cnp.Text = Appointment.CNP;
        }



        private void label1_Click(object sender, EventArgs e)
        {
           
        }

        private void comboBox_instructor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            int id_a = int.Parse(textBox_id.Text);
            string cnp = textBox_cnp.Text;
            string date = textBox_date.Text;
            string hour = textBox_hour.Text;
            string instructor = comboBox_instructor.SelectedItem.ToString();
            try
            {
                service1.AddAppointment(id_a, cnp, date, hour, instructor);
                MessageBox.Show("Your appointment has been added with success.");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Technical issue!\n" + ex.ToString());
            }
           
        }

        private void btn_change_Click(object sender, EventArgs e)
        {
            int id_a = int.Parse(textBox_id.Text);
            string cnp = textBox_cnp.Text;
            string date = textBox_date.Text;
            string hour = textBox_hour.Text;
            string instructor = comboBox_instructor.SelectedItem.ToString();
            try
            {
                service1.ChangeAppointment(id_a, date, hour, instructor);
                 MessageBox.Show("Your appointment has been added with success");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Technical issue!\n" + ex.ToString());
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
          string cnp = textBox_cnp.Text;
            try
            {
                service1.DeleteAppointment(cnp);
                MessageBox.Show("Your appointment has been deleted with success");
            }
            catch
            {
                MessageBox.Show("Technical issue!\n");
            }
            textBox_id.Clear();
            textBox_id.Clear();
            textBox_cnp.Clear();
            textBox_date.Clear();
            textBox_hour.Clear();
            comboBox_instructor.Text = "Choose Instructor";

        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            Exit exit = new Exit();
           exit.Show();
        }
    }
}


