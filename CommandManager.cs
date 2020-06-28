using System;
using System.Collections.Generic;
using System.Text;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;
using System.Reflection;
using System.Linq;

namespace Cs_Bot
{
    public class CommandManager
    {
        private readonly DiscordSocketClient _Client;
        private readonly CommandService _Commands;
        public CommandManager(DiscordSocketClient Client, CommandService Commands)
        {
            _Client = Client;

            _Commands = Commands;
        }

        public async Task InitializeAsync()
        {
            await _Commands.AddModulesAsync(Assembly.GetEntryAssembly(), null);
            _Commands.Log += _Commands_Log;
            _Client.MessageReceived += Message_Event;
        }

        private async Task Message_Event(SocketMessage MessageParam)
        {
            var Message = MessageParam as SocketUserMessage;
            var Context = new SocketCommandContext(_Client, Message);

            if (Context.Message == null || Context.Message.Content == "") return;
            if (Context.User.IsBot) return;

            int ArgPos = 0;
            if (!(Message.HasStringPrefix(Config.bot.Prefix, ref ArgPos) || Message.HasMentionPrefix(_Client.CurrentUser, ref ArgPos))) return;

            var Result = await _Commands.ExecuteAsync(Context, ArgPos, null);
            if(!Result.IsSuccess && Result.Error != CommandError.UnknownCommand)
            {

            }
        }

        private Task _Commands_Log(LogMessage Command)
        {
            Console.WriteLine(Command.Message);
            return Task.CompletedTask;
        }
    }
}
