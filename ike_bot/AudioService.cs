using Discord;
using Discord.Audio;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ike_bot
{
    public class AudioService
    {
        public AudioService() { }

        public async Task<IAudioClient> ConnectAudio(SocketCommandContext context, SocketVoiceChannel chan)
        {
            if (chan == null)
            {
                await context.Message.Channel.SendMessageAsync("User must be in a voice channel, or a voice channel must be passed as an argument.");
                return null;
            }
            return await chan.ConnectAsync();
        }

        public async Task Stream(IAudioClient client, string url)
        {
            var ffmpeg = CreateYoutubeStream(url);
            var output = ffmpeg.StandardOutput.BaseStream;
            var discord = client.CreatePCMStream(AudioApplication.Mixed, 96000);
            await output.CopyToAsync(discord);
            await discord.FlushAsync();
        }

        private Process CreateYoutubeStream(string url)
        {
            ProcessStartInfo ffmpeg = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $@"/C C:\YT\youtube-dl.exe --no-check-certificate -f bestaudio -o - {url} | ffmpeg -i pipe:0 -f s16le -ar 48000 -ac 2 pipe:1",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };

            return Process.Start(ffmpeg);
        }
    }
}