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
using Discord.Commands;
using Discord.WebSocket;
using Discord;
using System.Web.Services.Description;
using System.Reflection;

namespace PlurkDiscordBOT
{
    public partial class Form1 : Form
    {
        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;


        PlurkHelper helper = new PlurkHelper();
        string botToken = Properties.Settings.Default.BotToken;
        public async Task RunBotAsnyc()
        {
            _client = new DiscordSocketClient();
            _commands = new CommandService();
        }
        public async Task RegisterCommandAsync()
        {
            _client.MessageReceived += HandleCommandAsync;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;

                if (message is null || message.Author.IsBot) return;

            int argpos = 0;

            if(message.HasStringPrefix("p!",ref argpos) || message.HasMentionPrefix(_client.CurrentUser , ref argpos))
            {
                var context = new SocketCommandContext(_client, message);

                var result = await _commands.ExecuteAsync(context, argpos);

            }
        }

        public Form1()
        {
            InitializeComponent();

        }
        private async void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
            await _client.LogoutAsync();
            await _client.StopAsync();
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
                Properties.Settings.Default.Save();
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

        string PlurkUserPURL;
        string PlurkName;
        string PlurkContent;
        string PlurkQualifier;
        long PlurkID;
        int PlurkUserID;
        

        private void button4_Click(object sender, EventArgs e)
        {
            var entity = helper.getPlurks();

            PlurkContent = "" + entity.plurks[0].content;
            PlurkQualifier = "" + (entity.plurks[0].qualifier_translated != null ? entity.plurks[0].qualifier_translated : entity.plurks[0].qualifier);
            PlurkName = entity.plurk_users.First().Value.display_name;
            PlurkUserID = entity.plurk_users.First().Key;
            PlurkID = entity.plurks[0].plurk_id;
            PlurkUserPURL = helper.getPublicProfile(PlurkUserID).user_info.avatar_big;


            plurkbase36 = (int)entity.plurks[0].plurk_id;
            string SRC = Base36Converter.ConvertTo(plurkbase36).ToLower();
            char[] ArraySRC = SRC.ToCharArray();
            Array.Reverse(ArraySRC);
            SRC = new string(ArraySRC);
            PlurkURL = "https://www.plurk.com/p/" + SRC;

            
            
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
        


        string PlurkURL;
        private void button5_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(PlurkURL);
        }
        DiscordSocketClient client;
        bool B6 = false;
        private async void button6_Click(object sender, EventArgs e)
        {
            B6 = B6 != true ? true : false;
            if (B6)
            {
                try
                {
                    timer1.Enabled = true;
                    button6.Text = "停止BOT";
                    label3.Text = "已啟動";
                    string botToken = Properties.Settings.Default.BotToken;
                    RunBotAsnyc().GetAwaiter().GetResult();
                    await RegisterCommandAsync();
                    await _client.LoginAsync(TokenType.Bot, botToken);
                    await _client.StartAsync();
                    await Task.Delay(-1);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                timer1.Enabled = false;
                button6.Text = "啟動BOT";
                label3.Text = "已停止";
                await _client.LogoutAsync();
                await _client.StopAsync();

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var entity = helper.getPlurks();

            PlurkContent = "" + entity.plurks[0].content;
            PlurkQualifier = "" + (entity.plurks[0].qualifier_translated != null ? entity.plurks[0].qualifier_translated : entity.plurks[0].qualifier);
            PlurkName = entity.plurk_users.First().Value.display_name;
            PlurkUserID = entity.plurk_users.First().Key;
            PlurkID = entity.plurks[0].plurk_id;
            PlurkUserPURL = helper.getPublicProfile(PlurkUserID).user_info.avatar_big;
            plurkbase36 = (int)entity.plurks[0].plurk_id;
            string SRC = Base36Converter.ConvertTo(plurkbase36).ToLower();
            char[] ArraySRC = SRC.ToCharArray();
            Array.Reverse(ArraySRC);
            SRC = new string(ArraySRC);
            PlurkURL = "https://www.plurk.com/p/" + SRC;

        }
    }
}
