using System;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace ike_bot.Core.Commands
{
    public class JoJoReferences : ModuleBase<SocketCommandContext>
    {
        ModerationCommands modCommands = new ModerationCommands();
        AudioCommands audioCommands = new AudioCommands();

        [Command("JAH WARUDO")]
        [RequireUserPermission(GuildPermission.Administrator)] 
        public async Task stopTime(int seconds)
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
            await Task.Delay(seconds * 1000);
            foreach(IUser user in users)
            {
                var newUser = user as SocketGuildUser;
                await newUser.RemoveRoleAsync(demotedRole);
            }

            await Context.Channel.SendMessageAsync("time begins to move again...");
        }

        [Command("Bites the Dust")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task bitesTheDust()
        {
            await Context.Channel.SendMessageAsync("bites the dust doesn't work right now... maybe next time dumpass");
            await modCommands.PurgeChat(50);
            await Context.Channel.SendMessageAsync("Bites the Dust has activated...");
        }

        [Command("KING CRIMSON")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task kingCrimson()
        {
            if(Program.KingCrimsonActivated == true)
            {
                var m = await Context.Channel.SendMessageAsync("King Crimson is already in effect.");
                await Task.Delay(3000);
                await m.DeleteAsync();
            }
            else
            {
                Program.KingCrimsonActivated = true;
            }
        }

        
    }
}
