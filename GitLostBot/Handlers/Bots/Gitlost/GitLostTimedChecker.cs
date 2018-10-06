using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace GitLostBot.Handlers.Bots.Gitlost
{
    public class GitLostTimedChecker
    {
        public Timer timer { get; private set; }
        public Action callback { get; private set; }

        public GitLostTimedChecker(int milliseconds, Action onNewFound)
        {
            callback = onNewFound;

            timer = new Timer();
            timer.Interval = milliseconds;
            timer.Tick += Timer_Tick;
        }

        public void Start()
        {
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            callback.Invoke();
        }
    }
}
