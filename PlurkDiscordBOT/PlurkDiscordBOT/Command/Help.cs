using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurkDiscordBOT.Command
{
     class help : ModuleBase<SocketCommandContext>
    {
        [Command("help")]
        public async Task helpAsync()
        {
            var builder = new EmbedBuilder();
            builder.WithTitle("Ice Wizard Stats");
            builder.AddInlineField("Cost", "3");
            builder.AddInlineField("HP", "665");
            builder.AddInlineField("DPS", "42");
            builder.AddInlineField("Hit Speed", "1.5sec");
            builder.AddInlineField("SlowDown", "35%");
            builder.AddInlineField("AOE", "63");
            builder.WithThumbnailUrl("url");
            await Context.Channel.SendMessageAsync("", false, builder);
        }

    }
}
