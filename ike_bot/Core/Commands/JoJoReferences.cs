using System;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace ike_bot.Core.Commands
{
    public class JoJoReferences : ModuleBase<SocketCommandContext>
    {
        public ModerationService modService { get; set; }

        public AudioService audioService { get; set; }

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
            await modService.DeleteMessages(Context, 50);
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
        public async Task zaHando(IUser discordUser = null)
        {
            if (discordUser == null)
            {
                await Context.Channel.SendMessageAsync("dumpass.... wrong args");
            }
            else
            {
                await audioService.JoinAndPlay(Context, "https://www.youtube.com/watch?v=FuM8GpMc59M");

                var users = await Context.Channel.GetUsersAsync().FlattenAsync();
                foreach (var user in users)
                {
                    if (user.Username == discordUser.Username)
                    {
                        await (user as IGuildUser).ModifyAsync(x =>
                        {
                            x.Channel = null;
                        });
                    }
                }

                await audioService.DisconnectAudio(Context, (Context.User as IGuildUser).VoiceChannel);
            }
        }
    }
}
