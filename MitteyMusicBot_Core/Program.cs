using System;

namespace MitteyMusicBot_Core
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var theBot = new BotClient();
            theBot.Start();
            Console.ReadLine();
            theBot.Stop();
        }
    }
}