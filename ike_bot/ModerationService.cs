using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ike_bot
{
    public class ModerationService
    {
        public ModerationService() { }

        public async Task DeleteMessage(IMessage message)
        {
            await message.DeleteAsync();
        }

        public async Task DeleteMessages(SocketCommandContext Context, int amount)
        {
            var messages = await Context.Channel.GetMessagesAsync(amount).FlattenAsync();
            foreach(var message in messages)
            {
                await DeleteMessage(message);
            }
        }

        public async Task RenameUser(SocketGuildUser user, string newName)
        {
            await user.ModifyAsync(x =>
            {
                x.Nickname = newName;
            });
        }

        public async Task RenameAll(SocketCommandContext Context, string newName)
        {
            var users = await Context.Channel.GetUsersAsync().FlattenAsync();
            foreach(var user in users)
            {
                await RenameUser((user as SocketGuildUser), newName);
                //await Task.Delay(200);
            }
        }
    }
}
