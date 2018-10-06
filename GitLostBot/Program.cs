using System;
using System.Windows.Forms;

namespace Gitlost_bot
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AssureChannelsFile();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Gitlost_bot());
        }

        private static void AssureChannelsFile()
        {
            string saveFolder = System.IO.Directory.GetCurrentDirectory();
            string savePath = System.IO.Path.Combine(saveFolder, "channels.json");
            if (!Handlers.IO.JsonHandler.Exists(savePath))
                System.IO.File.WriteAllText(savePath, @"{ ""channels"": [] }");
        }
    }
}
