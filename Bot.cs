using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Reflection;
using System.Linq;

namespace Cs_Bot
{
    public class Bot
    {
        private DiscordSocketClient Client;
        private CommandService Commands;
        private IServiceProvider Service;

        public Bot() 
        {
            Client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Debug
            });

            Commands = new CommandService(new CommandServiceConfig
            {
                CaseSensitiveCommands = true,
                DefaultRunMode = RunMode.Async,
                LogLevel = LogSeverity.Debug
            });
        }

        public async Task MainAsync()
        {
            CommandManager cmdManager = new CommandManager(Client, Commands);

            Client.Ready += Ready_Event;
            Client.Log += Client_Log;
            if (Config.bot.Token == "" || Config.bot.Token == null) return;

            await Client.LoginAsync(TokenType.Bot, Config.bot.Token);
            await Client.StartAsync();
            await Task.Delay(-1);
        }

        private Task Client_Log(LogMessage Message)
        {
            Console.WriteLine($"{DateTime.Now} At {Message.Source}: {Message.Message}");
            return Task.CompletedTask;
        }

        private async Task Ready_Event()
        {
            Console.WriteLine($"{Client.CurrentUser.Username} is ready");
            await Client.SetGameAsync($"{Config.bot.Prefix} help");
            await Client.SetStatusAsync(UserStatus.Online);
        }
    }
}
