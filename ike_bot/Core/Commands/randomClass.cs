using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace ike_bot
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        public AudioService audio { get; set; }

        [Command("hi"), Alias("hello", "hey"), Summary("responds to the user")]
        public async Task Greeting()
        {

            Random gen = new Random();
            if (gen.Next(0, 11) <= 9)
            {
                await Context.Channel.SendMessageAsync("whaddup dumpass...");
            }
            else
            {
                await Context.Channel.SendMessageAsync("shut the fuck up");
            }
        }

        [Command("good morning kanye"), Alias("ey good morning kanye", "ey good mornin kanye", "hey good morning kanye", "hey good mornin kanye")]
        public async Task GoodMorningKanye()
        {
            await Context.Channel.SendMessageAsync("shut the fuck up");
        }

        [Command("spam")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task Spam(string message, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                await Context.Channel.SendMessageAsync(message);
            }
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
            var m = await this.ReplyAsync($"Purge completed. _This message will be deleted in {delay / 1000} seconds._");
            await Task.Delay(delay);
            await m.DeleteAsync();
        }

        [Command("stop"), Alias("oh god oh fuck", "please stop following me")]
        public async Task Threat()
        {
            await Context.Channel.SendMessageAsync("Every second you run I'm only getting closer.");
        }

        [Command("fuckCalvin"), Alias("fuckCalbim")]
        public async Task FuckCalvin()
        {
            //var users = await Context.Channel.GetUsersAsync().FlattenAsync();
            //foreach (IUser user in users)
            //{
            //    var newUser = user as Discord.WebSocket.SocketGuildUser;
            //    if (newUser.Id == 220710429083697152 || newUser.Id == 579404643160031242)
            //    {
            //        continue;
            //    }
            //    await newUser.ModifyAsync(x =>
            //    {
            //        x.Nickname = "fuck you calvin";
            //    });
            //}

            //await Context.Channel.SendMessageAsync("Done!");
            await Rename("fuck you calvin");
        }

        [Command("who are you"), Alias("Who are you?", "what are you?")]
        public async Task Dumpass()
        {
            await Context.Channel.SendMessageAsync("whO did you think it wAs dumpass... the esater bunny??/?");
        }

        [Command("help")]
        public async Task Help()
        {
            var adminUser = await Context.Channel.GetUserAsync(220710429083697152);
            string adminUsername = adminUser.Username;
            await Context.Channel.SendMessageAsync($"if you want help ask {adminUsername}");
        }


        [Command("rename")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task Rename(string newName)
        {

            //if (Context.User.Id == 220710429083697152)
            //{
            var users = await Context.Channel.GetUsersAsync().FlattenAsync();
            foreach (IUser user in users)
            {
                var newUser = user as Discord.WebSocket.SocketGuildUser;
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
            //}
            //else
            //{
            //    await Context.Channel.SendMessageAsync("dumpass... only Ike can use this command");
            //}
        }

        [Command("unname")]
        public async Task UnName()
        {
            await Rename("");
        }

        [Command("jahseh")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task ChatSpeak()
        {
            var Message = await Context.Channel.GetMessagesAsync(1).FlattenAsync();
            foreach (var CmdMsg in Message)
            {
                await Context.Channel.DeleteMessageAsync(CmdMsg);
            }

            bool done = false;
            bool isRunning = false;
            string jahMessage;
            if (isRunning == true)
            {
                await Context.Channel.SendMessageAsync("this command is already in effect");
            }
            else
            {
                while (!done)
                {
                    jahMessage = Console.ReadLine();
                    if (jahMessage == "terminate")
                    {
                        done = true;
                        isRunning = false;
                        break; 
                    }
                    if (jahMessage == "")
                    {
                        await Context.Channel.SendMessageAsync("s");
                    }
                    else
                    {
                        await Context.Channel.SendMessageAsync(jahMessage);
                    }
                    isRunning = true;
                    
                }
            }

        }

        [Command("play", RunMode = RunMode.Async)]
        [RequireContext(ContextType.Guild)]
        public async Task Join(string url)
        {
            //var channel;
            IVoiceChannel channel = (Context.User as IGuildUser).VoiceChannel;
            var audioClient = await audio.ConnectAudio(Context);
            if (audioClient == null)
            {
                return;
            }

            await audio.Stream(audioClient, url);
        }

        [Command("randomsound"), Alias("loud ass sound")]
        [RequireContext(ContextType.Guild)]
        public async Task randomSound()
        {
            await Join("https://www.youtube.com/watch?v=jm1b_Xl9RmU");
        }

        [Command("disconnect", RunMode = RunMode.Async), Alias("leave", "go away")]
        public async Task Disconnect(IVoiceChannel channel = null)
        {
            channel = channel ?? (Context.User as IGuildUser)?.VoiceChannel;
            if (channel == null)
            {
                await Context.Channel.SendMessageAsync("dumpass....");
            }
            else
            {
                await channel.DisconnectAsync();
            }
        }


        [Command("find x")]
        public async Task findX()
        {
            await Context.Channel.SendMessageAsync("too soon...");
        }

        [Command("Bites the Dust")] 
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task bitesTheDust()
        {
            await PurgeChat(50);
            await Context.Channel.SendMessageAsync("Bites the Dust has activated...");
            
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

        [Command("JAH WARUDO")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task stopTime()
        {
            var Message = await Context.Channel.GetMessagesAsync(1).FlattenAsync();
            foreach(var CmdMsg in Message)
            {
                await Context.Channel.DeleteMessageAsync(CmdMsg);
            }

            await Context.Channel.SendMessageAsync("JAH WARUDO");
            
            var users = await Context.Channel.GetUsersAsync().FlattenAsync();
            foreach (IUser user in users)
            {
                var newUser = user as Discord.WebSocket.SocketGuildUser;
                var demotedRole = Context.Guild.GetRole(579569542188236800) as IRole;
                await newUser.AddRoleAsync(demotedRole);
                await Task.Delay(11000);
                await newUser.RemoveRoleAsync(demotedRole);
            }

            await ChatSpeak();
        }
    }
}