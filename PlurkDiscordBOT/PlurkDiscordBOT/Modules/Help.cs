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
        [Command("help")]
        public async Task helpAsync()
        {
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
                Timestamp = DateTime.UtcNow,
                Url = "https://github.com/InterfaceGUI"
            
            };
            await ReplyAsync("",false, builder);

        }

        
    }
}
