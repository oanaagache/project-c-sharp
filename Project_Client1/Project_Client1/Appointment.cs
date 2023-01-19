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
    public partial class Appointment : Form
    {  //cream un obiect services 1 pt a accesa metodele 
        Project_Client1.ServiceReference1.WebService1SoapClient service1 = new         ServiceReference1.WebService1SoapClient();

        public static string CNP; //folosim var cnp mai departe 

        public Appointment()
        {
            InitializeComponent();
            label6.Text = "Welcome! Please complete the fields below.";
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            string cnp = textBox_cnp.Text;
            string name = textBox_name.Text;
            string surname = textBox_surname.Text;
            string age = textBox_age.Text;
            string phoneNo = textBox_phoneNo.Text;
            try { 
            service1.AddPersonalData(cnp, name, surname, age, phoneNo);
            MessageBox.Show("Your data have been added with success.");
            }
             catch(Exception ex)
            {
                MessageBox.Show("Technical issue!\n" + ex.ToString());
            }

            CNP = cnp;

            //se golesc campurile 
            textBox_cnp.Clear();
            textBox_name.Clear();
            textBox_surname.Clear();
            textBox_age.Clear();
            textBox_phoneNo.Clear();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            string cnp = textBox_cnp.Text;
            try
            { service1.DeleteAppointment(cnp);
              //service1.DeletePersonalData(cnp);
                
              MessageBox.Show("Your data have been deleted with success.");
            }
            catch (Exception ex)
            {MessageBox.Show("Technical issue!\n" + ex);
            }
            textBox_cnp.Clear();
        }

        private void btn_change_Click(object sender, EventArgs e)
        {
            string cnp = textBox_cnp.Text;
            string name = textBox_name.Text;
            string surname = textBox_surname.Text;
            string age = textBox_age.Text;
            string phoneNo = textBox_phoneNo.Text;
            try
            {
                service1.ChangePersonalData(cnp, name, surname, age, phoneNo);
                MessageBox.Show("Your data have been added with success.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Technical issue!\n" + ex.ToString());
            }

            CNP = cnp;

            //se golesc campurile 
            textBox_cnp.Clear();
            textBox_name.Clear();
            textBox_surname.Clear();
            textBox_age.Clear();
            textBox_phoneNo.Clear();

        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            Instructor_appointment instructor_Appointment = new Instructor_appointment();
            instructor_Appointment.Show();
        }

        private void textBox_cnp_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox_phoneNo_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
