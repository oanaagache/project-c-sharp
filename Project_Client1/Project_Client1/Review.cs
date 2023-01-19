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
    public partial class Review : Form
    {
        Project_Client1.ServiceReference1.WebService1SoapClient service1 = new ServiceReference1.WebService1SoapClient();

        Random rand = new Random();
        public char p; //salvam puctajul

        public Review()
        {
            InitializeComponent();
            label1.Text = "Please complete the fields below.";
            textBox_id_r.Text = rand.Next(1000).ToString();
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            int id_r = int.Parse(textBox_id_r.Text);
            int id_a = int.Parse(textBox_id_a.Text);
            string description = richTextBox_description.Text;
            switch (p)
            {
                case '1': radioButton1.Checked = false; break;
                case '2': radioButton2.Checked = false; break;
                case '3': radioButton3.Checked = false; break;
                case '4': radioButton4.Checked = false; break;
                case '5': radioButton5.Checked = false; break;
            }
            try
            {
                service1.AddReviews(id_r, id_a, p, description);
                MessageBox.Show("Recenzia d-voastra au fost trimisa! Va multumim!");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Technical issue!\n" + ex.ToString());
                
            }
            textBox_id_r.Clear();
            textBox_id_a.Clear();
            richTextBox_description.Clear();
         }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            p = '1';
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            p = '2';
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            p = '3';
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            p = '4';
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            p = '5';
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }

}
