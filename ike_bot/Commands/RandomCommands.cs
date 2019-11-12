using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace ike_bot.Commands
{
    public class RandomCommands : ModuleBase<SocketCommandContext>
    {
        public ModerationService modService { get; set; }

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

        [Command("stop"), Alias("oh god oh fuck", "please stop following me")]
        public async Task Threat()
        {
            await Context.Channel.SendMessageAsync("Every second you run I'm only getting closer.");
        }


        [Command("who are you"), Alias("Who are you?", "what are you?")]
        public async Task Dumpass()
        {
            await Context.Channel.SendMessageAsync("whO did you think it wAs dumpass... the esater bunny??/?");
        }

        [Command("help")]
        public async Task Help()
        {
            await Context.Channel.SendMessageAsync("go to the github (https://github.com/arekouzounian/jahbot) to see the code and what the bot does dumpass...");
        }

        [Command("lockName")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task LockName(string lockname, IUser user)
        {
            Program.lockedName = lockname;
            Program.lockedUser = user;
            if(user.Username != lockname)
            {
                await (user as SocketGuildUser).ModifyAsync(x =>
                {
                    x.Nickname = lockname;
                });
            }
            
        }

        bool jahsehDone = false;
        bool jahsehIsRunning = false;
        [Command("jahseh")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task ChatSpeak()
        {
            await modService.DeleteMessage(Context.Message as IMessage);

            string jahMessage;
            if (jahsehIsRunning == true)
            {
                await Context.Channel.SendMessageAsync("this command is already in effect");
            }
            else
            {
                while (!jahsehDone)
                {
                    jahMessage = Console.ReadLine();
                    if (jahMessage == "terminate")
                    {
                        jahsehDone = true;
                        jahsehIsRunning = false;
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
                    jahsehIsRunning = true;
                    
                }
            }

        }
        
        [Command("test")]
        [RequireOwner]
        public async Task test()
        {
            await Context.Channel.SendMessageAsync("test completed!");
        }

        [Command("find x")]
        public async Task findX()
        {
            await Context.Channel.SendMessageAsync("too soon...");
        }
    }
}
