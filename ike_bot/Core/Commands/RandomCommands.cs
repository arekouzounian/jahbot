using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace ike_bot.Core.Commands
{
    public class RandomCommands : ModuleBase<SocketCommandContext>
    {

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
            var adminUser = await Context.Channel.GetUserAsync(220710429083697152);
            string adminUsername = adminUser.Username;
            await Context.Channel.SendMessageAsync($"if you want help ask {adminUsername}");
        }

        bool jahsehDone = false;
        bool jahsehIsRunning = false;
        [Command("jahseh")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task ChatSpeak()
        {
            var Message = await Context.Channel.GetMessagesAsync(1).FlattenAsync();
            foreach (var CmdMsg in Message)
            {
                await Context.Channel.DeleteMessageAsync(CmdMsg);
            }

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

        [Command("find x")]
        public async Task findX()
        {
            await Context.Channel.SendMessageAsync("too soon...");
        }
    }
}