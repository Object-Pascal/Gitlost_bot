using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Gitlost_bot.Handlers.IO
{
    public static class JsonHandler
    {
        public static List<ulong> channels = new List<ulong>();

        public static async Task<string> LoadFile(string filename)
        {
            string saveFolder = Directory.GetCurrentDirectory();
            string savePath = Path.Combine(saveFolder, filename);
            using (StreamReader reader = File.OpenText(savePath))
            {
                return await reader.ReadToEndAsync();
            }
        }

        public static void SaveFile(string filename, string text)
        {
            string saveFolder = Directory.GetCurrentDirectory();
            string savePath = Path.Combine(saveFolder, filename);
            File.WriteAllText(savePath, text);
        }

        public static void DeleteFile(string filename)
        {
            string saveFolder = Directory.GetCurrentDirectory();
            string savePath = Path.Combine(saveFolder, filename);
            File.Delete(savePath);
        }

        public static bool Exists(string filename)
        {
            string saveFolder = Directory.GetCurrentDirectory();
            string savePath = Path.Combine(saveFolder, filename);
            return File.Exists(savePath);
        }

        public static async Task<T> GetSingleJsonValue<T>(string filename, string name)
        {
            string file = await LoadFile(filename);
            dynamic data = await Task.Run(() => JsonConvert.DeserializeObject(file));
            return (T)data[name];
        }

        public static async Task<T> GetSingleJsonValue<T>(string filename, string name, int index)
        {
            string file = await LoadFile(filename);
            dynamic data = await Task.Run(() => JsonConvert.DeserializeObject(filename));
            return (T)data[index][name];
        }

        public static void UpdateChannels(JToken[] collection)
        {
            channels.Clear();
            for (int i = 0; i < collection.Length; i++)
            {
                channels.Add(ulong.Parse(collection[i].Value<string>()));   
            }
        }
    }
}