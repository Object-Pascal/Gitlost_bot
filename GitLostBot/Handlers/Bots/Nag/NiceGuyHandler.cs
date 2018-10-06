using Discord;
using Discord.Commands;
using Discord.WebSocket;
using GitLostBot.Handlers.Bots.Gitlost;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace GitLostBot.Handlers.Bots
{
    class NiceGuyHandler
    {
        public DiscordSocketClient _client { get; private set; }
        private CommandService _commands;
        private IServiceProvider _services;
        private GitLostTimedChecker gitlostTimer;

        public BotState state { get; private set; }

        public NiceGuyHandler()
        {
            _client = new DiscordSocketClient();
            _commands = new CommandService();
            _services = new ServiceCollection().AddSingleton(_client).AddSingleton(_commands).BuildServiceProvider();
            state = BotState.Ready;
        }

        public async Task Start(Func<LogMessage, Task> logCallback)
        {
            state = BotState.Running;
            string token = "NDk3Mzg5ODU0NjE1NjY2Njg4.Dped7w.OsuULIV61hE5yw-w9iJrx410gSk";

            await _client.SetGameAsync("v0.2 alpha");

            _client.Log += logCallback;
            _client.MessageReceived += HandleCommandAsync;

            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
            await Task.Delay(-1);
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            SocketUserMessage message = (SocketUserMessage) arg;
            if (message == null || message.Author.IsBot) return;

            int argPos = 0;

            if (message.HasStringPrefix("!niceguy ", ref argPos) || message.HasStringPrefix("!ng ", ref argPos))
            {
                SocketCommandContext context = new SocketCommandContext(_client, message);

                IResult result = await _commands.ExecuteAsync(context, argPos, _services);

                if (!result.IsSuccess)
                    Console.WriteLine(result.ErrorReason);
            }
        }

        public async Task Stop()
        {
            await _client.LogoutAsync();
            await _client.StopAsync();
            state = BotState.Stopped; 
        }
    }
}
