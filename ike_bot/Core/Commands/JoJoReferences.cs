using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace ike_bot.Core.Commands
{
    public class JoJoReferences : ModuleBase<SocketCommandContext>
    {
        ModerationCommands modCommands = new ModerationCommands();

        [Command("JAH WARUDO")]
        [RequireUserPermission(GuildPermission.Administrator)] 
        public async Task stopTime()
        {
            var demotedRole = Context.Guild.GetRole(579569542188236800) as IRole;
            var Message = await Context.Channel.GetMessagesAsync(1).FlattenAsync();
            foreach (var CmdMsg in Message)
            {
                await Context.Channel.DeleteMessageAsync(CmdMsg);
            }

            await Context.Channel.SendMessageAsync("JAH WARUDO");

            var users = await Context.Channel.GetUsersAsync().FlattenAsync();
            foreach (IUser user in users)
            {
                var newUser = user as SocketGuildUser;
                await newUser.AddRoleAsync(demotedRole);
            }
            await Task.Delay(11000);
            foreach(IUser user in users)
            {
                var newUser = user as SocketGuildUser;
                await newUser.RemoveRoleAsync(demotedRole);
            }
        }

        [Command("Bites the Dust")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task bitesTheDust()
        {
            await modCommands.PurgeChat(50);
            await Context.Channel.SendMessageAsync("Bites the Dust has activated...");
        }
    }
}
