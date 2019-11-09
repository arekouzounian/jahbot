using Discord;
using Discord.Commands;
using System.Threading.Tasks;

using ike_bot.Services;
using System;

namespace ike_bot.Commands
{
    public class AudioCommands : ModuleBase<SocketCommandContext>
    {
        public AudioService audio { get; set; }

        [Command("play", RunMode = RunMode.Async)]
        [RequireContext(ContextType.Guild)]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task Play(string url)
        {
            //IVoiceChannel channel = (Context.User as IGuildUser).VoiceChannel;
            var audioClient = await audio.ConnectAudio(Context);
            if (audioClient == null)
            {
                return;
            }

            await audio.Stream(audioClient, url);
            //Console.WriteLine(url);

            //await audio.JoinAndPlay(Context, url);
        }

        [Command("randomsound"), Alias("loud ass sound")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task randomSound()
        {
            await audio.JoinAndPlay(Context, /*https://www.youtube.com/watch?v=jm1b_Xl9RmU */"https://www.youtube.com/watch?v=JjS3wMkOsWE");
        }

        [Command("disconnect", RunMode = RunMode.Async), Alias("leave", "go away")]
        public async Task Disconnect(IVoiceChannel channel = null)
        {
            await audio.DisconnectAudio(Context);
        }
    }
}
