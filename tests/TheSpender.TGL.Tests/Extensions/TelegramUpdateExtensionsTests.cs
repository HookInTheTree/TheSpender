using System.Globalization;
using FluentAssertions;
using Telegram.Bot.Types;
using TheSpender.TGL.Commands;
using TheSpender.TGL.Extensions;
using Xunit;

namespace TheSpender.TGL.Tests.Extensions;

public class TelegramUpdateExtensionsTests
{
    #region ExtractCommandName
    [Fact]
    public void ExtractCommandName_UpdateIsMessageAndContainsKnownMessageCommandName_ReturnsMessageCommandName()
    {
        var knownMessageCommandNames = CommandNames.Messages;
        foreach (var messageCommandName in knownMessageCommandNames)
        {
            var update = new Update()
            {
                Message = new Message()
                {
                    Text = messageCommandName
                }
            };

            update.ExtractCommandName().Should().Be(update.Message.Text);
        }
    }

    [Fact]
    public void ExtractCommandName_UpdateIsMessageAndContainsNotKnownMessageCommandName_ReturnsNull()
    {
        var update = new Update()
        {
            Message = new Message()
            {
                Text = "some-not-known-command"
            }
        };

        update.ExtractCommandName().Should().BeNull();
    }

    [Fact]
    public void ExtractCommandName_UpdateIsCallbackQuery_ReturnCallbackDataAsCommandName()
    {
        var update = new Update()
        {
            CallbackQuery = new ()
            {
               Data = "some-callback-query-command"
            }
        };

        update.ExtractCommandName().Should().Be(update.CallbackQuery!.Data);
    }

    [Fact]
    public void ExtractCommandName_UpdateIsNotSupportedMessageType_ReturnsNull()
    {
        var update = new Update()
        {
            BusinessMessage = new Message()
            {
                Text = CommandNames.Messages.FirstOrDefault()
            }
        };

        update.ExtractCommandName().Should().BeNull();
    }
    #endregion

    #region GetChatId

    [Fact]
    public void GetChatId_UpdateWithMessage_ReturnsMessageChatId()
    {
        var update = new Update()
        {
            Message = new Message()
            {
                Chat = new Chat()
                {
                    Id = 10000000
                }
            }
        };

        update.GetChatId().Should().Be(update.Message.Chat.Id.ToString(CultureInfo.InvariantCulture));
    }

    [Fact]
    public void GetChatId_UpdateWithCallbackQuery_ReturnsCallbackQueryMessageChatId()
    {
        var update = new Update()
        {
            CallbackQuery = new ()
            {
                Message = new()
                {
                    Chat = new()
                    {
                        Id = 10000000
                    }
                }
            }
        };

        update.GetChatId().Should().Be(update.CallbackQuery.Message.Chat.Id.ToString(CultureInfo.InvariantCulture));
    }

    [Fact]
    public void GetChatId_UpdateIsNotSupportedType_ThrowsInvalidOperationException()
    {
        var update = new Update()
        {
            BusinessMessage = new ()
            {
                Text = CommandNames.Messages.FirstOrDefault()
            }
        };

        update.Invoking(x => x.GetChatId()).Should().Throw<InvalidOperationException>();
    }

    #endregion


}
