using Discord;
using Discord.Commands;
using Discord.WebSocket;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gitlost_bot.Handlers.IO;
using Newtonsoft.Json.Linq;

namespace Gitlost_bot.Handlers.Bots
{
    public class CommandModule : ModuleBase<SocketCommandContext>
    {
        #region gitlost bot

        [Command("help")]
        public async Task help()
        {
            int argPos = 0; if (!(Context.Message.HasStringPrefix("!gitlost ", ref argPos) || Context.Message.HasStringPrefix("!gl ", ref argPos))) return;

            EmbedBuilder builder = new EmbedBuilder();
            StringBuilder message1 = new StringBuilder();
            StringBuilder message2 = new StringBuilder();

            message1.AppendLine("[!gl / !gitlost] help");
            message1.AppendLine("[!gl / !gitlost] link");
            message1.AppendLine("[!gl / !gitlost] latest");
            message1.AppendLine("[!gl / !gitlost] last");
            message1.AppendLine("[!gl / !gitlost] all (all 30)");
            message1.AppendLine("[!gl / !gitlost] top [amount] (30 max)");
            message1.AppendLine("[!gl / !gitlost] bottom [amount] (30 max)");
            message2.AppendLine("[!gl / !gitlost] register (registers the current channel to the newest post feed)");
            message2.AppendLine("[!gl / !gitlost] unregister (unregisters the current channel to the newest post feed)");
            message2.AppendLine("[!gl / !gitlost] registered (view registered channels)");

            builder.WithTitle($"General commands")
                .WithDescription(message1.ToString())
                .WithColor(Color.Blue);
            await ReplyAsync("", false, builder.Build());

            builder.WithTitle($"Admin commands")
                .WithDescription(message2.ToString())
                .WithColor(Color.Blue);
            await ReplyAsync("", false, builder.Build());
        }

        [Command("latest")]
        public async Task latest()
        {
            int argPos = 0; if (!(Context.Message.HasStringPrefix("!gitlost ", ref argPos) || Context.Message.HasStringPrefix("!gl ", ref argPos))) return;

            EmbedBuilder builder = new EmbedBuilder();
            List<string[]> posts = await Task.Run(() => Fetcher.FetchTweets(1));

            builder.WithTitle($"Developers Swearing @gitlost • {posts[0][1]}")
                .WithDescription(posts[0][0])
                .WithColor(Color.Blue);
            await ReplyAsync("", false, builder.Build());
        }

        [Command("last")]
        public async Task last()
        {
            int argPos = 0; if (!(Context.Message.HasStringPrefix("!gitlost ", ref argPos) || Context.Message.HasStringPrefix("!gl ", ref argPos))) return;

            EmbedBuilder builder = new EmbedBuilder();
            List<string[]> posts = await Task.Run(() => Fetcher.FetchTweets(30));

            builder.WithTitle($"Developers Swearing @gitlost • {posts[29][1]}")
                .WithDescription(posts[29][0])
                .WithColor(Color.Blue);
            await ReplyAsync("", false, builder.Build());
        }

        [Command("all")]
        public async Task all()
        {
            int argPos = 0; if (!(Context.Message.HasStringPrefix("!gitlost ", ref argPos) || Context.Message.HasStringPrefix("!gl ", ref argPos))) return;

            EmbedBuilder builder = new EmbedBuilder();
            List<string[]> posts = await Task.Run(() => Fetcher.FetchTweets(30));
            StringBuilder message = new StringBuilder();

            for (int i = 0; i < posts.Count; i++)
            {
                message.AppendLine($"{posts[i][1]} • {posts[i][0]}");
                message.AppendLine("");
            }
            builder.WithTitle("Developers Swearing @gitlost")
                .WithDescription(message.ToString())
                .WithColor(Color.Blue);
            await ReplyAsync("", false, builder.Build());
        }

        [Command("top")]
        public async Task top([Remainder] string remainder)
        {
            int argPos = 0; if (!(Context.Message.HasStringPrefix("!gitlost ", ref argPos) || Context.Message.HasStringPrefix("!gl ", ref argPos))) return;

            int amount = int.TryParse(remainder, out amount) ? amount : 0;
            amount = amount > 30 ? 30 : amount;

            if (amount > 0)
            {
                EmbedBuilder builder = new EmbedBuilder();
                List<string[]> posts = await Task.Run(() => Fetcher.FetchTweets(amount));
                StringBuilder message = new StringBuilder();

                for (int i = 0; i < posts.Count; i++)
                {
                    message.AppendLine($"{posts[i][1]} • {posts[i][0]}");
                    message.AppendLine("");
                }
                builder.WithTitle("Developers Swearing @gitlost")
                    .WithDescription(message.ToString())
                    .WithColor(Color.Blue);
                await ReplyAsync("", false, builder.Build());
            }
        }

        [Command("bottom")]
        public async Task last([Remainder] string remainder)
        {
            int argPos = 0; if (!(Context.Message.HasStringPrefix("!gitlost ", ref argPos) || Context.Message.HasStringPrefix("!gl ", ref argPos))) return;

            int amount = int.TryParse(remainder, out amount) ? amount : 0;
            amount = amount > 30 ? 30 : amount;

            if (amount > 0)
            {
                EmbedBuilder builder = new EmbedBuilder();
                List<string[]> posts = await Task.Run(() => Fetcher.FetchTweets(30));

                posts.Reverse();

                StringBuilder message = new StringBuilder();

                for (int i = 0; i < amount; i++)
                {
                    message.AppendLine($"{posts[i][1]} • {posts[i][0]}");
                    message.AppendLine("");
                }
                builder.WithTitle("Developers Swearing @gitlost")
                    .WithDescription(message.ToString())
                    .WithColor(Color.Blue);
                await ReplyAsync("", false, builder.Build());
            }
        }

        [Command("registered")]
        public async Task registered()
        {
            int argPos = 0; if (!(Context.Message.HasStringPrefix("!gitlost ", ref argPos) || Context.Message.HasStringPrefix("!gl ", ref argPos))) return;

            if (!Context.IsPrivate)
            {
                ISocketMessageChannel channel = Context.Message.Channel;

                EmbedBuilder builder = new EmbedBuilder();
                StringBuilder message = new StringBuilder();

                JToken token = JObject.Parse(await JsonHandler.LoadFile("channels.json"));
                JArray coll = (JArray)token.SelectToken("channels");
                JObject newJson = new JObject();
                newJson.Add(new JProperty("channels", coll));
                JsonHandler.SaveFile("channels.json", newJson.ToString());
                JsonHandler.UpdateChannels(coll.ToArray());

                JsonHandler.channels.ForEach((c) =>
                {
                    message.AppendLine(((ISocketMessageChannel)Context.Client.GetChannel(c)).Name);
                });

                if (JsonHandler.channels.Count > 0)
                {
                    builder.WithTitle("Registered channels for this server")
                    .WithDescription(message.ToString())
                    .WithColor(Color.Blue);
                    await ReplyAsync("", false, builder.Build());
                }
                else
                {
                    builder.WithDescription($"There are no channels registered (yet).\nType '!gl | !gitlost register' to register a channel to the newest post feed.")
                        .WithColor(Color.Blue);
                    await ReplyAsync("", false, builder.Build());
                }
            }
        }

        [Command("register")]
        public async Task register()
        {
            int argPos = 0; if (!(Context.Message.HasStringPrefix("!gitlost ", ref argPos) || Context.Message.HasStringPrefix("!gl ", ref argPos))) return;

            if (!Context.IsPrivate && ((SocketGuildUser)Context.Message.Author).Roles.Any(x => x.Permissions.Administrator))
            {
                ISocketMessageChannel channel = Context.Message.Channel;
                EmbedBuilder builder = new EmbedBuilder();

                JToken token = JObject.Parse(await JsonHandler.LoadFile("channels.json"));
                JArray coll = (JArray)token.SelectToken("channels");

                bool exists = coll.Any(x => x.Value<string>() == channel.Id.ToString());

                if (!exists)
                {
                    coll.Add(channel.Id);
                    JObject newJson = new JObject();
                    newJson.Add(new JProperty("channels", coll));
                    JsonHandler.SaveFile("channels.json", newJson.ToString());
                    JsonHandler.UpdateChannels(coll.ToArray());

                    builder.WithDescription($"'{channel.Name}' has been registered to the newest post feed.\nType '!gl | !gitlost unregister' to unregister this channel.")
                        .WithColor(Color.Blue);
                    await ReplyAsync("", false, builder.Build());
                }
                else
                {
                    builder.WithDescription($"'{channel.Name}' has already been registered.\nType '!gl | !gitlost unregister' to unregister this channel.")
                        .WithColor(Color.Blue);
                    await ReplyAsync("", false, builder.Build());
                }
            }
        }

        [Command("unregister")]
        public async Task unregister()
        {
            int argPos = 0; if (!(Context.Message.HasStringPrefix("!gitlost ", ref argPos) || Context.Message.HasStringPrefix("!gl ", ref argPos))) return;

            if (!Context.IsPrivate && ((SocketGuildUser)Context.Message.Author).Roles.Any(x => x.Permissions.Administrator))
            {
                ISocketMessageChannel channel = Context.Message.Channel;

                EmbedBuilder builder = new EmbedBuilder();

                JToken token = JObject.Parse(await JsonHandler.LoadFile("channels.json"));
                JArray coll = (JArray)token.SelectToken("channels");

                bool exists = coll.Any(x => x.Value<string>() == channel.Id.ToString());

                if (!exists)
                {
                    builder.WithDescription($"'{channel.Name}' hasn't been registered yet.\nType '!gl | !gitlost register' to register this channel.")
                        .WithColor(Color.Blue);
                    await ReplyAsync("", false, builder.Build());
                }
                else
                {
                    coll.Remove(coll.Where(x => x.Value<string>() == channel.Id.ToString()).First());
                    JObject newJson = new JObject();
                    newJson.Add(new JProperty("channels", coll));
                    JsonHandler.SaveFile("channels.json", newJson.ToString());
                    JsonHandler.UpdateChannels(coll.ToArray());

                    builder.WithDescription($"'{channel.Name}' has been unregistered from the newest post feed.")
                        .WithColor(Color.Blue);
                    await ReplyAsync("", false, builder.Build());
                }
            }
        }

        [Command("link")]
        public async Task link()
        {
            int argPos = 0; if (!(Context.Message.HasStringPrefix("!gitlost ", ref argPos) || Context.Message.HasStringPrefix("!gl ", ref argPos))) return;

            EmbedBuilder builder = new EmbedBuilder();

            builder.WithDescription("https://twitter.com/gitlost")
                .WithColor(Color.Blue);
            await ReplyAsync("", false, builder.Build());
        }

        #endregion
    }
}
