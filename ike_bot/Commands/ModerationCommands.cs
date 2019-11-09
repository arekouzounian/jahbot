using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

using ike_bot.Services;

namespace ike_bot.Commands
{ 
    public class ModerationCommands : ModuleBase<SocketCommandContext>
    {
        public ModerationCommands() { }
        
        public ModerationService moderationService { get; set; }

        [Command("rename")]
        [RequireUserPermission(GuildPermission.Administrator)]
        [RequireBotPermission(GuildPermission.ChangeNickname)]
        public async Task Rename(string newName)
        {
            await moderationService.RenameAll(Context, newName);

            await Context.Channel.SendMessageAsync("Done!");

        }

        [Command("unname")]
        [RequireUserPermission(GuildPermission.ChangeNickname)]
        [RequireBotPermission(GuildPermission.ChangeNickname)]
        public async Task UnName()
        {
            await moderationService.RenameAll(Context, "");
        }

        [Command("delete", RunMode = RunMode.Async)]
        [Summary("Deletes the specified amount of messages.")]
        [RequireUserPermission(GuildPermission.Administrator)]
        [RequireBotPermission(ChannelPermission.ManageMessages)]
        public async Task PurgeChat(int amount)
        {
            await moderationService.DeleteMessages(Context, amount);
            const int delay = 5000;
            var m = await ReplyAsync($"Purge completed. _This message will be deleted in {delay / 1000} seconds._");
            await Task.Delay(delay);
            await m.DeleteAsync();
        }

        [Command("retortPercent"), Alias("retortpercent", "RetortPercent")]
        public async Task setRetortPercent(int newPercent)
        {
            var Message = await Context.Channel.GetMessagesAsync(1).FlattenAsync();
            foreach (var CmdMsg in Message)
            {
                await Context.Channel.DeleteMessageAsync(CmdMsg);
            }
            Program.retortPercent = newPercent;
        }

        [Command("check a")]
        public async Task cleanChat(int amount)
        {
            SocketGuildChannel aChannel = null;
            var channels = Context.Guild.Channels;
            foreach(var channel in channels)
            {
                if(channel.Name == "a")
                {
                    aChannel = channel;
                }
            }
            if(aChannel == null)
            {
                await Context.Guild.Owner.SendMessageAsync("why is there no '#a' channel dumpass... that's one of my features so make it right now");
            }

            var messages = await (aChannel as ISocketMessageChannel).GetMessagesAsync(amount).FlattenAsync();
            foreach(var message in messages)
            {
                if(message.Content != "a")
                {
                    await (aChannel as ISocketMessageChannel).DeleteMessageAsync(message);
                }
            }

            await (aChannel as ISocketMessageChannel).SendMessageAsync("a");
        }
    }
}
