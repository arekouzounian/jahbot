using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Diagnostics;
using Discord.Audio;

namespace ike_bot.Services
{
    public class AudioService 
    {
        public AudioService() { }

        public async Task<IAudioClient> ConnectAudio(SocketCommandContext context)
        { 
            SocketGuildUser user = context.User as SocketGuildUser;
            IVoiceChannel channel = user.VoiceChannel;
            if (channel == null)
            {
                await context.Message.Channel.SendMessageAsync("User must be in a voice channel dumpass");
                return null;
            }
            return await channel.ConnectAsync();
        }

        public async Task DisconnectAudio(SocketCommandContext context)
        {
            var channel = (context.User as IGuildUser).VoiceChannel;
            if (channel == null)
            {
                await context.Channel.SendMessageAsync("dumpass....");
            }
            else
            {
                await channel.DisconnectAsync();
            }
        }

        public async Task Stream(IAudioClient client, string url)
        {
            var ffmpeg = CreateYoutubeStream(url);
            var output = ffmpeg.StandardOutput.BaseStream;
            var discord = client.CreatePCMStream(AudioApplication.Mixed, 96000);
            await output.CopyToAsync(discord);
            await discord.FlushAsync();
        }

        public async Task JoinAndPlay(SocketCommandContext context, string url)
        {
            // IVoiceChannel channel = (context.User as IGuildUser).VoiceChannel;
            var audioClient = await ConnectAudio(context);
            if (audioClient == null)
            {
                return;
            }

            await Stream(audioClient, url);
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