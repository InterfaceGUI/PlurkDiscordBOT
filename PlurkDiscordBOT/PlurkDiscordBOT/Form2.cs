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
            textBox3.Text = Properties.Settings.Default.ServerID;
            textBox4.Text = Properties.Settings.Default.ChannelID;
            textBox5.Text = Properties.Settings.Default.BotToken;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == ""|| textBox4.Text == "")
            {
                MessageBox.Show("Any value Can't be empty", "警告");
                return;
            }
            Properties.Settings.Default.AppKey = textBox1.Text;
            Properties.Settings.Default.Appsecret = textBox2.Text;
            Properties.Settings.Default.ServerID = textBox3.Text;
            Properties.Settings.Default.ChannelID = textBox4.Text;
            Properties.Settings.Default.BotToken = textBox5.Text;
            this.Dispose();
            Properties.Settings.Default.Save();
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
