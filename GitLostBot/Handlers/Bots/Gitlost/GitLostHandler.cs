using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Gitlost_bot.Handlers.Bots
{
    class GitLostHandler
    {
        public DiscordSocketClient _client { get; private set; }
        private Func<LogMessage, Task> logCallback;
        private CommandService _commands;
        private IServiceProvider _services;

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
            //state = BotState.Running;

            //string token = ""; 
            throw new NotImplementedException("Enter your bot token in the string variable above first to enable functionality!");

            //await _client.SetGameAsync("v1.1.0");

            //this.logCallback = logCallback;
            //_client.Log += logCallback;
            //_client.MessageReceived += HandleCommandAsync;

            //await _commands.AddModulesAsync(Assembly.GetEntryAssembly());
            //await _client.LoginAsync(TokenType.Bot, token);
            //await _client.StartAsync();
            //await Task.Delay(-1);
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            SocketUserMessage message = (SocketUserMessage) arg;
            if (message == null || message.Author.IsBot) return;

            int argPos = 0;

            if (message.HasStringPrefix("!gitlost ", ref argPos) || message.HasStringPrefix("!gl ", ref argPos))
            {
                try
                {
                    SocketCommandContext context = new SocketCommandContext(_client, message);

                    IResult result = await _commands.ExecuteAsync(context, argPos, _services);

                    if (!result.IsSuccess)
                        await logCallback.Invoke(new LogMessage(LogSeverity.Error, "Execution", result.ErrorReason));
                }
                catch (Exception e)
                {
                    await logCallback.Invoke(new LogMessage(LogSeverity.Critical, "Execution", e.Message));
                }
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
