using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace ike_bot.Services 
{
    public class ModerationService
    {
        public ModerationService() { }

        public async Task RenameAll(SocketCommandContext context, string newName)
        {
            var users = await context.Channel.GetUsersAsync().FlattenAsync();
            foreach (var user in users)
            {
                var newUser = user as SocketGuildUser;
                await Rename(context, newUser, newName);
            }
        }

        public async Task Rename(SocketCommandContext context, SocketGuildUser user, string newName)
        {
            if(user.Id == context.Guild.OwnerId)
            {
                return;
            }
            else
            {
                await user.ModifyAsync(x =>
                {
                    x.Nickname = newName;
                });
            }
        }

        public async Task DeleteMessages(SocketCommandContext context, int amount)
        {
            var messages = await context.Channel.GetMessagesAsync(amount).FlattenAsync();
            foreach(var message in messages)
            {
                await DeleteMessage(context, message);
            }
        }

        public async Task DeleteMessage(SocketCommandContext context, IMessage message)
        {
            await context.Channel.DeleteMessageAsync(message);
        }
    }
}
