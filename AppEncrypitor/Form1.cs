using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppEncrypitor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var nome2 = "";
            if (comboBox1.Text == "Encrypitar")
            {
                nome2 = txtNome.Text.ToCrypt();
                //txtResultado.Text = nome2;
                textRes.Text = nome2;

            }
            else
            {
                nome2 = txtNome.Text.FromCript();
                //txtResultado.Text = nome2;
                textRes.Text = nome2;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Text = "Encrypitar";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtNome.Text = "";
            textRes.Text = "";
            txtNome.Focus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textRes.Text);
        }
    }
}
