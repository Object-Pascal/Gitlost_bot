using Discord;
using Discord.WebSocket;
using GitLostBot.Handlers;
using GitLostBot.Handlers.Bots;
using GitLostBot.Handlers.Bots.Gitlost;
using GitLostBot.Handlers.IO;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GitLostBot
{
    public partial class BotMaNiceGuyer : Form
    {
        private PostState poststate;

        private string[] lastPost;

        private GitLostHandler gitlostHandler;
        private NiceGuyHandler NiceGuyHandler;

        private GitLostTimedChecker gitlostchecker;

        public BotMaNiceGuyer()
        {
            InitializeComponent();
            LoadFeedChannels();

            txtGitLostLog.ReadOnly = true;
            btnStartGitLostBot.Enabled = true;
            btnStopGitLostBot.Enabled = false;

            txtNiceGuyLog.ReadOnly = true;
            btnStartNiceGuyBot.Enabled = true;
            btnStopNiceGuyBot.Enabled = false;

            poststate = PostState.FirstBoot;
            lastPost = null;

            gitlostHandler = new GitLostHandler();
            NiceGuyHandler = new NiceGuyHandler();

            gitlostchecker = new GitLostTimedChecker(30000, NewPostFound_Tick);
            NewPostFound_Tick();
            gitlostchecker.Start();

            btnStartGitLostBot.Click += (s, e) => StartGitLostBot();
            btnStopGitLostBot.Click += (s, e) => StopGitLostBot();

            btnStartNiceGuyBot.Click += (s, e) => StartNiceGuyBot();
            btnStopNiceGuyBot.Click += (s, e) => StopNiceGuyBot();

            this.Load += (s, e) =>
            {
                //StartGitLostBot();
                StartNiceGuyBot();
            };
        }

        #region gitlostbot

        private async void StartGitLostBot()
        {
            if (gitlostHandler.state != BotState.Running)
            {
                gitlostHandler = new GitLostHandler();

                btnStartGitLostBot.Enabled = false;
                btnStopGitLostBot.Enabled = true;

                await gitlostHandler.Start(GitLostLog);
            }
        }
        private async void StopGitLostBot()
        {
            if (gitlostHandler.state != BotState.Stopped)
            {
                await gitlostHandler.Stop();
                btnStartGitLostBot.Enabled = true;
                btnStopGitLostBot.Enabled = false;
            }
        }                                                                                               

        private Task GitLostLog(LogMessage args)
        {
            txtGitLostLog.Invoke((MethodInvoker)delegate
            {
                txtGitLostLog.Text += args + Environment.NewLine;
                txtGitLostLog.ScrollToCaret();
            });
            return Task.CompletedTask;
        }

        private async void LoadFeedChannels()
        {
            JToken token = JObject.Parse(await JsonHandler.LoadFile("channels.json"));
            JArray coll = (JArray)token.SelectToken("channels");
            JsonHandler.UpdateChannels(coll.ToArray());
        }

        private async void NewPostFound_Tick()
        {
            if (gitlostHandler.state == BotState.Running)
            {
                List<string[]> posts = await Task.Run(() => Fetcher.FetchTweets(1));
                if (posts.Count > 0)
                {
                    if (poststate == PostState.FirstBoot)
                    {
                        poststate = PostState.Ready;
                        lastPost = posts[0];
                        await GitLostLog(new LogMessage(LogSeverity.Info, "Client", $"Initial boot, latest tweet set as last post"));
                    }
                    else
                    {
                        if (posts[0][0] != lastPost[0])
                        {
                            lastPost = posts[0];

                            EmbedBuilder builder = new EmbedBuilder();

                            builder.WithTitle($"[Auto] Developers Swearing @gitlost • {lastPost[1]}")
                                .WithDescription(System.Net.WebUtility.HtmlDecode(lastPost[0]))
                                .WithColor(Discord.Color.Blue);

                            JsonHandler.channels.ForEach(async(c) =>
                            {
                                await (gitlostHandler._client.GetChannel(c) as ISocketMessageChannel).SendMessageAsync("", false, builder.Build());
                            });

                            await GitLostLog(new LogMessage(LogSeverity.Info, "Client", $"New post found, send to {JsonHandler.channels.Count} channel(s)"));
                        }
                        else
                        {
                            await GitLostLog(new LogMessage(LogSeverity.Info, "Client", $"No new post found"));
                        }
                    }
                }
            }
        }

        #endregion

        #region irritantobot

        private async void StartNiceGuyBot()
        {
            if (NiceGuyHandler.state != BotState.Running)
            {
                NiceGuyHandler = new NiceGuyHandler();

                btnStartNiceGuyBot.Enabled = false;
                btnStopNiceGuyBot.Enabled = true;

                await NiceGuyHandler.Start(NiceGuyLog);
            }
        }

        private async void StopNiceGuyBot()
        {
            if (NiceGuyHandler.state != BotState.Stopped)
            {
                await NiceGuyHandler.Stop();
                btnStartNiceGuyBot.Enabled = true;
                btnStopNiceGuyBot.Enabled = false;
            }
        }

        private Task NiceGuyLog(LogMessage args)
        {
            txtNiceGuyLog.Invoke((MethodInvoker)delegate
            {
                txtNiceGuyLog.Text += args + Environment.NewLine;
                txtNiceGuyLog.ScrollToCaret();
            });
            return Task.CompletedTask;
        }

        #endregion
    }
}
