using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace ike_bot.Core.Commands
{ 
    public class ModerationCommands : ModuleBase<SocketCommandContext>
    {
        public ModerationCommands() { }


        [Command("rename")]
        [RequireUserPermission(GuildPermission.Administrator)]
        [RequireBotPermission(GuildPermission.ChangeNickname)]
        public async Task Rename(string newName)
        {
            var users = await Context.Channel.GetUsersAsync().FlattenAsync();
            foreach (IUser user in users)
            {
                var newUser = user as SocketGuildUser;
                if (newUser.Id == 220710429083697152)
                {
                    continue;
                }
                else
                {
                    await newUser.ModifyAsync(x =>
                    {
                        x.Nickname = newName;
                    });
                }
            }

            await Context.Channel.SendMessageAsync("Done!");

        }

        [Command("unname")]
        [RequireUserPermission(GuildPermission.ChangeNickname)]
        [RequireBotPermission(GuildPermission.ChangeNickname)]
        public async Task UnName()
        {
            await Rename("");
        }

        [Command("delete", RunMode = RunMode.Async)]
        [Summary("Deletes the specified amount of messages.")]
        [RequireUserPermission(GuildPermission.Administrator)]
        [RequireBotPermission(ChannelPermission.ManageMessages)]
        public async Task PurgeChat(int amount)
        {
            var messages = await Context.Channel.GetMessagesAsync(amount + 1).FlattenAsync();

            foreach (var message in messages)
            {
                await Context.Channel.DeleteMessageAsync(message);
            }
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
            var aChannel = Context.Guild.GetChannel(579185404842999818);
            var messages = await (aChannel as ISocketMessageChannel).GetMessagesAsync().FlattenAsync();
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
