using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlurkDiscordBOT
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            textBox1.Text = Properties.Settings.Default.AppKey;
            textBox2.Text = Properties.Settings.Default.Appsecret;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("AppKey or Appsecret Can't be empty" , "警告");
                return;
            }
            Properties.Settings.Default.AppKey = textBox1.Text;
            Properties.Settings.Default.Appsecret = textBox2.Text;
            this.Dispose();
        }
        DialogResult result;
        private void button2_Click(object sender, EventArgs e)
        {
            result = MessageBox.Show("確定離開? 將會不儲存資料", "警告", MessageBoxButtons.YesNo);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                this.Dispose();
            }
            
        }
    }
}
