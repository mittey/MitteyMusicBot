using System;
using System.Threading.Tasks;
using Telegram.Bot;

namespace MitteyMusicBot_Core
{
    internal static class Program
    {
        private static async Task TestApiAsync()
        {
            var botClient = new TelegramBotClient("MYSECRETAPIKEY");
            var me = await botClient.GetMeAsync();
            Console.WriteLine($"Hello, {me.FirstName}");
        }

        private static void Main(string[] args)
        {
            TestApiAsync().GetAwaiter().GetResult();
            Console.ReadLine();
        }
    }
}