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
        private void Form1_Closing(object sender, EventArgs e)
        {
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            panel1.Visible = false;
            

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
        int plurkbase36;
        private void button4_Click(object sender, EventArgs e)
        {
            var entity = helper.getPlurks();

            textBox3.Text = "" + entity.plurks[0].content_raw;
            textBox2.Text = "" + (entity.plurks[0].qualifier_translated != null ? entity.plurks[0].qualifier_translated:entity.plurks[0].qualifier);
            plurkbase36 = (int)entity.plurks[0].plurk_id;
            string SRC = Base36Converter.ConvertTo(plurkbase36).ToLower();
            char[] ArraySRC = SRC.ToCharArray();
            Array.Reverse(ArraySRC);
            SRC = new string(ArraySRC);
            PlurkURL = "https://www.plurk.com/p/" + SRC;

 
            textBox5.Text = entity.plurk_users[0].display_name;
        }
        public class Introduction
        {
            public string display_name { get; set; }
        }

        public static class Base36Converter
        {
            private const string Chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            public static string ConvertTo(int value)
            {
                string result = "";

                while (value > 0)
                {
                    result += Chars[value % 36];
                    value /= 36;
                }

                return result;
            }
        }
        private void 讀取設定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            


            helper.Client.SetApp(Properties.Settings.Default.AppKey, Properties.Settings.Default.Appsecret);
            MessageBox.Show(Properties.Settings.Default.TokenContent + "\r\n"+ Properties.Settings.Default.TokenSecret);
            helper.Client.Token = new OAuthToken(Properties.Settings.Default.TokenContent, Properties.Settings.Default.TokenSecret, OAuthTokenType.Permanent);
        }

        private void 儲存當前設定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
           // Properties.Settings.Default.Upgrade();
            Properties.Settings.Default.Save();
        }
        string PlurkURL;
        private void button5_Click(object sender, EventArgs e)
        {

            System.Diagnostics.Process.Start(PlurkURL);



        }
    }
}
