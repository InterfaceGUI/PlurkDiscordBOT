using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RenRen.Plurk;
namespace PlurkDiscordBOT
{
    public partial class Form1 : Form
    {
        PlurkHelper helper = new PlurkHelper();
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            panel1.Visible = false;
            helper.Client.SetApp(Properties.Settings.Default.AppKey, Properties.Settings.Default.Appsecret);
            helper.Client.Token = new OAuthToken(Properties.Settings.Default.TokenContent, Properties.Settings.Default.TokenSecret, OAuthTokenType.Permanent);

        }
    
        
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                helper.Client.SetApp(Properties.Settings.Default.AppKey, Properties.Settings.Default.Appsecret);
                helper.Client.GetRequestToken();
                Properties.Settings.Default.TokenContent = helper.Client.Token.Content;
                Properties.Settings.Default.TokenSecret = helper.Client.Token.Secret;
                helper.Client.Token = new OAuthToken(Properties.Settings.Default.TokenContent, Properties.Settings.Default.TokenSecret, OAuthTokenType.Permanent);
                System.Diagnostics.Process.Start(helper.Client.GetAuthorizationUrl());
                panel1.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"錯誤");
            }
            

        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                helper.Client.GetAccessToken(textBox4.Text);
                var entity = helper.GetUnreadPlurks();
                panel1.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void 初始設定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var entity = helper.getPlurks();


            textBox5.Text = "" + entity.plurks[0].content_raw;

        }
    }
}
