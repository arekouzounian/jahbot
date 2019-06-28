using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace ike_bot
{
    class Program
    {
        private DiscordSocketClient Client;
        public CommandService Commands;
        private IServiceProvider services;

        public static int retortPercent = 10;

        static void Main(string[] args)
        => new Program().MainAsync().GetAwaiter().GetResult();

        private async Task MainAsync()
        {
            Client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Debug,
                AlwaysDownloadUsers = true,
                MessageCacheSize = 100
            });

            Commands = new CommandService(new CommandServiceConfig
            {
                CaseSensitiveCommands = true,
                DefaultRunMode = RunMode.Async,
                LogLevel = LogSeverity.Debug
            });

            services = new ServiceCollection()
                .AddSingleton(this)
                .AddSingleton(Client)
                .AddSingleton(Commands)
                .AddSingleton<ConfigHandler>()
                .AddSingleton<AudioService>()
                .BuildServiceProvider();

            await services.GetService<ConfigHandler>().FillConfig();

            Client.MessageReceived += HandleCommand;
            await Commands.AddModulesAsync(Assembly.GetEntryAssembly(), services);

            Client.Ready += Client_Ready;
            Client.Log += Client_Log;

            await Client.LoginAsync(TokenType.Bot, services.GetService<ConfigHandler>().GetToken());
            await Client.StartAsync();


            await Task.Delay(-1);
        }
        
        private async Task Client_Log(LogMessage Message)
        {
            Console.WriteLine($"{DateTime.Now} at {Message.Source}] {Message.Message}");
        }

        private async Task Client_Ready()
        {
            await Client.SetGameAsync("with pyro gaming");
        }

        List<ulong> lastMessageIDs = new List<ulong>();
        int index = 0;
        private async Task HandleCommand(SocketMessage MessageParam)
        {
            var Message = MessageParam as SocketUserMessage;
            var Context = new SocketCommandContext(Client, Message);

            if (Context.Message == null || Context.Message.Content == "") return;
            if (Context.User.IsBot) return;

            antiSpam(Message.Author.Id, Context);

            Random gen = new Random();
            if (gen.Next(0, 101) <= retortPercent && Context.Channel.Id != 579185404842999818)
            {
                if(gen.Next(0, 6) <= 2)
                    await Context.Channel.SendFileAsync(@"C:\Users\agouz\Desktop\Important Pictures\deformedman.jpg");
                else
                    await Context.Channel.SendMessageAsync("fuck you");
            }

            int ArgPos = 0;

            if ((Context.Channel.Id == 579185404842999818 && Context.Message.Content != "a"))
                await Context.Channel.DeleteMessageAsync(Message.Id);
            if (!(Message.HasStringPrefix("jahbot ", ref ArgPos)) || (Message.HasMentionPrefix(Client.CurrentUser, ref ArgPos)))
                return;
            

            var Result = await Commands.ExecuteAsync(Context, ArgPos, services);

            if (!Result.IsSuccess)
            {
                Console.WriteLine($"{DateTime.Now} at Commands] Something went wrong with executing a command. Text: {Context.Message.Content} | Error: {Result.ErrorReason}");
                if (Context.Channel.Id != 579185404842999818)
                {
                    if (Result.ErrorReason == "User requires guild permission Administrator.")
                        await Context.Channel.SendMessageAsync("dumpass... you need special perms to use this command.");
                    else
                        await Context.Channel.SendMessageAsync("wrong command dumpass");
                }
            }

        }
        private async void antiSpam(ulong id, SocketCommandContext Context)
        {
            if (Context.Channel.Id == 579185404842999818)
                return;
            
            lastMessageIDs.Add(id);
            var users = await Context.Channel.GetUsersAsync().FlattenAsync();
            ulong mostMessagesID = 0;
            int mostMessagesCount = 0;
            var demotedRole = Context.Guild.GetRole(579569542188236800) as IRole;
            foreach (var user in users)
            {
                if (user.IsBot)
                    continue;

                int msgCount = 0;
                for (int i = 0; i < lastMessageIDs.Count; i++)
                {
                    if (lastMessageIDs[i] == user.Id)
                        msgCount++;
                }

                if (msgCount > mostMessagesCount)
                {
                    mostMessagesCount = msgCount;
                    mostMessagesID = user.Id;
                }
            }
            if (mostMessagesCount >= 30)
            {
                await (Context.Guild.GetUser(mostMessagesID) as IGuildUser).AddRoleAsync(demotedRole);
                await Task.Delay(120000);
                await (Context.Guild.GetUser(mostMessagesID) as IGuildUser).RemoveRoleAsync(demotedRole);
                mostMessagesID = 0;
                mostMessagesCount = 0;
            }

            if (lastMessageIDs.Count > 50)
            {
                lastMessageIDs.RemoveAt(index);
                index++;

                if (index == 50)
                    index = 0;
            }

        }
    }
}