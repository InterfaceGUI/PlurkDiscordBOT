using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurkDiscordBOT.Command
{
    public class help : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        public async Task PingAsync()
        {
            await ReplyAsync("Ping!!");
        }
        [Command("help")]
        public async Task helpAsync()
        {
            List<EmbedFieldBuilder> Fields = new List<EmbedFieldBuilder>();
            Fields.Add(new EmbedFieldBuilder {
                IsInline = false,
                Name = "help",
                Value = "取得幫助"
            });
            Fields.Add(new EmbedFieldBuilder
            {
                IsInline = false,
                Name = "p!sfollow [URL]",
                Value = "關注 URL 請放入 用戶主頁的網址"  + "看起來如下" + "\n" + "p!sfollow https://www.plurk.com/Interfac_GUI"
            });
            Fields.Add(new EmbedFieldBuilder
            {
                IsInline = false,
                Name = "p!sfollow [URL]",
                Value = "取消關注同關注用法"
            });
            var builder = new EmbedBuilder() {
                Title = " PlurkDiscordBOT",
                Author = new EmbedAuthorBuilder
                {
                    IconUrl = Context.User.GetAvatarUrl(),
                    Name = Context.User.Username + "Request"

                },
                Footer = new EmbedFooterBuilder()
                {
                    Text = "BOT made by Interface_GUI",
                    IconUrl = "https://images-ext-1.discordapp.net/external/ntiOqZA45m-A1b-He-DuE5nREM_4pcPflu05Kvucgak/%3Fsize%3D128/https/cdn.discordapp.com/avatars/226226332944564224/479324dce3872b1c0aad2031159a5c80.png?width=89&height=89"
                },
                Color = Color.Orange,
                Description = "相關命令列表",
                Fields = Fields,
                Timestamp = DateTime.UtcNow,
                Url = "https://github.com/InterfaceGUI"
            
            };
            await ReplyAsync("",false, builder);

        }

        
    }
}
