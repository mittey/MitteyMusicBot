using System;
using System.IO;
using System.Linq;
using TagLib;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using File = TagLib.File;

namespace MitteyMusicBot_Core
{
    internal class BotClient : IBotClient
    {
        private readonly TelegramBotClient _theBotClient =
            new TelegramBotClient("THISISASECRET");

        public BotClient()
        {
            _theBotClient.OnMessage += BotOnMessageReceived;
        }

        public void Start()
        {
            _theBotClient.StartReceiving();
        }

        public void Stop()
        {
            _theBotClient.StopReceiving();
        }

        private async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;
            Console.WriteLine($"\nGot an message from : {message.Chat.Username}");
            if (message.Type != MessageType.TextMessage) return;
            if (message.Text.StartsWith("/start"))
            {
                await _theBotClient.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);
                var keyboard = new ReplyKeyboardMarkup(new[]
                {
                    new[]
                    {
                        new KeyboardButton("/donate"),
                        new KeyboardButton("/getasong")
                    }
                });
                await _theBotClient.SendTextMessageAsync(message.Chat.Id, "Choose",
                    replyMarkup: keyboard);
            }
            else if (message.Text.StartsWith("/getasong"))
            {
                var songFile = Directory.GetFiles(@"E:\\", "*.mp3", SearchOption.TopDirectoryOnly).FirstOrDefault();
                var theSongInfo = File.Create(songFile, ReadStyle.Average);
                using (var stream = System.IO.File.Open(songFile, FileMode.Open))
                {
                    await _theBotClient.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);
                    await _theBotClient.SendAudioAsync(message.Chat.Id, new FileToSend("TheSong", stream),
                        theSongInfo.Tag.FirstAlbumArtist + " - " + theSongInfo.Tag.Title,
                        (int) theSongInfo.Properties.Duration.TotalSeconds, theSongInfo.Tag.FirstAlbumArtist,
                        theSongInfo.Tag.Title);
                }
            }
            else if (message.Text.StartsWith("/donate"))
            {
                await _theBotClient.SendTextMessageAsync(message.Chat.Id, "You just donated $1000!");
            }
        }
    }
}