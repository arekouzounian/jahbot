using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;
using ike_bot.Services;

namespace ike_bot.Commands
{
    public class JoJoReferences : ModuleBase<SocketCommandContext>
    {
        //ModerationCommands modCommands = new ModerationCommands();
        //AudioCommands audioCommands = new AudioCommands();
        public AudioService audioService { get; set; }

        public ModerationService moderationService { get; set; }

        [Command("JAH WARUDO")]
        [RequireUserPermission(GuildPermission.Administrator)] 
        public async Task stopTime(int seconds)
        {
            IRole demotedRole = null; 
            var roles = Context.Guild.Roles;
            foreach (var role in roles)
            {
                if (role.Name == "demoted")
                {
                    demotedRole = role as IRole;
                }
            }

            if (demotedRole == null)
            {
                await Context.Guild.Owner.SendMessageAsync("you dumpass... I'm trying to mute someone because they're spamming but there's no 'demoted' role. Make a role" +
                    "called 'demoted' on the server, and give it no perms as punishment for those dumpasses....");
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
            await moderationService.DeleteMessages(Context, 50);
            await Context.Channel.SendMessageAsync("Bites the Dust has activated...");
        }

        [Command("KING CRIMSON")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task kingCrimson(int amount = 11)
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
                Program.kingCrimsonAmount = amount;
            }
        }

        [Command("ZA HANDO")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task zaHando(IUser User)
        {
            await audioService.JoinAndPlay(Context, "https://www.youtube.com/watch?v=FuM8GpMc59M");

            var users = await Context.Channel.GetUsersAsync().FlattenAsync();
            foreach (var user in users)
            {
                if (user == User)
                {
                    await (user as IGuildUser).ModifyAsync(x =>
                    {
                        x.Channel = null;
                    });
                }
            }
            await audioService.DisconnectAudio(Context);
        }
    }
}
