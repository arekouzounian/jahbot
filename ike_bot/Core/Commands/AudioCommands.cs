﻿using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace ike_bot.Core.Commands
{
    public class AudioCommands : ModuleBase<SocketCommandContext>
    {
        public AudioService audio { get; set; }
        public AudioCommands() { }

        [Command("play", RunMode = RunMode.Async)]
        [RequireContext(ContextType.Guild)]
        public async Task JoinAndPlay(string url)
        {
            //var channel;
            await audio.JoinAndPlay(Context, url);
            //var audioClient = await audio.ConnectAudio(Context);
            //if (audioClient == null)
            //{
            //    return;
            //}

            //await audio.Stream(audioClient, url);
        }

        [Command("randomsound"), Alias("loud ass sound")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task randomSound()
        {
            await JoinAndPlay("https://www.youtube.com/watch?v=jm1b_Xl9RmU");
        }

        [Command("disconnect", RunMode = RunMode.Async), Alias("leave", "go away")]
        public async Task Disconnect(IVoiceChannel channel = null)
        {
            channel = channel ?? (Context.User as IGuildUser)?.VoiceChannel;
            await audio.DisconnectAudio(Context, channel);
        }
    }
}
