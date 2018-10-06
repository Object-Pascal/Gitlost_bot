using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Gitlost_bot.Handlers.Bots.Gitlost;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Gitlost_bot.Handlers.Bots
{
    public enum BotState
    {
        Ready,
        Running,
        Stopped
    }

    public enum PostState
    {
        FirstBoot,
        Ready
    }

    class GitLostHandler
    {
        public DiscordSocketClient _client { get; private set; }
        private CommandService _commands;
        private IServiceProvider _services;
        private GitLostTimedChecker gitlostTimer;

        public BotState state { get; private set; }

        public GitLostHandler()
        {
            _client = new DiscordSocketClient();
            _commands = new CommandService();
            _services = new ServiceCollection().AddSingleton(_client).AddSingleton(_commands).BuildServiceProvider();
            state = BotState.Ready;
        }

        public async Task Start(Func<LogMessage, Task> logCallback)
        {
            state = BotState.Running;
            string token = "NDkxOTk4OTY1Nzg5NzUzMzY0.DoQDUA.tl6dgF_3Y3rjHl6ac2tmZXxQDGE";

            await _client.SetGameAsync("v1.0.1");

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

            if (message.HasStringPrefix("!gitlost ", ref argPos) || message.HasStringPrefix("!gl ", ref argPos))
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
