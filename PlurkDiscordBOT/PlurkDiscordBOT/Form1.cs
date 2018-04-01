using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using RenRen.Plurk;
using Discord.Commands;
using Discord.WebSocket;
using Discord;
using System.Reflection;

namespace PlurkDiscordBOT
{
    public partial class Form1 : Form
    {
        static DiscordSocketClient _client;
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
                if (message.Content.StartsWith("p!sfollow"))
                {
                    
                  string temp =  message.Content.Remove(0, 9);
                    temp = temp.Remove(0,23);
                    await message.Channel.SendMessageAsync("Search... [" + temp + "]");
                    await message.Channel.SendMessageAsync(helper.SetFollowing(helper.UserSearch(temp).users[0].id.ToString(), "true").success_text + "! Now follow!");
                }
                else if (message.Content.StartsWith("p!rfollow"))
                {
                    string temp = message.Content.Remove(0, 9);
                    temp = temp.Remove(0, 23);
                    await message.Channel.SendMessageAsync("Search... [" + temp + "]");
                    string setID = helper.UserSearch(temp).users[0].id.ToString();
                    await message.Channel.SendMessageAsync(helper.SetFollowing(setID,"true").success_text);
                    await message.Channel.SendMessageAsync(helper.SetFollowing(helper.UserSearch(temp).users[0].id.ToString(), "false").error_text + " Unfollow");
                }
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
                MessageBox.Show(ex.Message,"Error");
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

        
        int plurkbase36;

        public static string PlurkUserPURL;
        public static string PlurkName;
        public static string PlurkContent;
        public static string PlurkQualifier;
        public static string PlurkContent_raw;
        public static string PlurkURL;
        public static long PlurkID;
        public static int PlurkUserID;
        

        public static string Url;
        
        private async void button4_Click(object sender, EventArgs e)
        {
            var entity = helper.getPlurks();
            PlurkContent_raw = "" + entity.plurks[0].content_raw;
            PlurkContent = "" + entity.plurks[0].content;
            PlurkQualifier = "" + (entity.plurks[0].qualifier_translated != null ? entity.plurks[0].qualifier_translated : entity.plurks[0].qualifier);
            PlurkName = entity.plurk_users.First().Value.display_name;
            PlurkUserID = entity.plurk_users.First().Key;
            PlurkID = entity.plurks[0].plurk_id;
            PlurkUserPURL = helper.getPublicProfile(PlurkUserID).user_info.avatar_big;

            if (PlurkContent.Contains("<img src="))
            {
                int temp = PlurkContent.IndexOf("<img src=") + 10;
                string strtemp = PlurkContent.Remove(0, temp);
                Url = strtemp.Substring(0, strtemp.IndexOf(textBox1.Text));
                if (Url.Contains("https://images.plurk.com/mx_"))
                {
                    Url= Url.Remove(Url.IndexOf("mx_"), 3);
                }
            }
            else
            {
                Url = "";
            }
        



            plurkbase36 = (int)entity.plurks[0].plurk_id;
            string SRC = Base36Converter.ConvertTo(plurkbase36).ToLower();
            char[] ArraySRC = SRC.ToCharArray();
            Array.Reverse(ArraySRC);
            SRC = new string(ArraySRC);
            PlurkURL = "https://www.plurk.com/p/" + SRC;

            PUPURL = PlurkUserPURL;
            await SendMessage();
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
                    button6.Text = "Stop BOT";
                    label3.Text = "Started";
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
                button6.Text = "Start BOT";
                label3.Text = "Stopped";
                await _client.LogoutAsync();
                await _client.StopAsync();

            }
        }
        string PUPURL;
        private async void timer1_Tick(object sender, EventArgs e)
        {
            var entity = helper.getPlurks();

            PlurkContent = "" + entity.plurks[0].content;
            PlurkQualifier = "" + (entity.plurks[0].qualifier_translated != null ? entity.plurks[0].qualifier_translated : entity.plurks[0].qualifier);
            PlurkContent_raw = "" + entity.plurks[0].content_raw;
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

            if (PlurkContent.Contains("<img src="))
            {
                int temp = PlurkContent.IndexOf("<img src=") + 10;
                string strtemp = PlurkContent.Remove(0, temp);
                Url = strtemp.Substring(0, strtemp.IndexOf(textBox1.Text));
                if (Url.Contains("https://images.plurk.com/mx_"))
                {
                   Url =  Url.Remove(Url.IndexOf("mx_"), 3);
                }
            }
            else
            {
                Url ="";
            }

            if (PlurkUserPURL != PUPURL)
            {
                PUPURL= PlurkUserPURL ;
              await  SendMessage();
            }
            



        }
        public async Task SendMessage()
        {
            var builder = new EmbedBuilder()
            {
                Title = Form1.PlurkQualifier,
                Author = new EmbedAuthorBuilder
                {
                    IconUrl = Form1.PlurkUserPURL,
                    Name = Form1.PlurkName,
                    Url = Form1.PlurkURL
                },
                Footer = new EmbedFooterBuilder()
                {
                    Text = "BOT made by Interface_GUI",
                    IconUrl = "https://images-ext-1.discordapp.net/external/ntiOqZA45m-A1b-He-DuE5nREM_4pcPflu05Kvucgak/%3Fsize%3D128/https/cdn.discordapp.com/avatars/226226332944564224/479324dce3872b1c0aad2031159a5c80.png?width=89&height=89"
                },
                ImageUrl = Form1.Url,
                Color = Discord.Color.Orange,
                Description = Form1.PlurkContent_raw,
                Timestamp = DateTime.UtcNow

            };
            try
            {
                await _client.GetGuild(Properties.Settings.Default.ServerID).GetTextChannel(Properties.Settings.Default.ChannelID).SendMessageAsync("", false, builder);
            }
            catch(Exception e)
            {
                MessageBox.Show("error" + " : " + e.Message);
            }


        }


    }
}
