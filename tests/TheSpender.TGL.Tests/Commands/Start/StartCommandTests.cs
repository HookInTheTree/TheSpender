using FluentAssertions;
using Moq;
using Telegram.Bot;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using TheSpender.TGL.Commands;
using TheSpender.TGL.Commands.Start;
using Xunit;

namespace TheSpender.TGL.Tests.Commands.Start;

public class StartCommandTests
{
    private readonly Mock<ITelegramBotClient> _botClientMock = new();

    [Fact]
    public async Task Execute_CorrectData_SendHelloMessageToUser()
    {
        var update = new Update()
        {
            Message = new Message()
            {
                Chat = new()
                {
                    Id = 100000
                }
            }
        };

        var command = new StartCommand(_botClientMock.Object);
        await command.Execute(update, CancellationToken.None);

        _botClientMock.Verify(x => x.SendRequest(
                It.Is<SendMessageRequest>(r => r.ChatId == update.Message.Chat.Id &&
                                               r.Text == StartCommand.HelloMessageText &&
                                               r.ReplyMarkup == Keyboards.MainMenu),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public void CommandName_GetCommandName_ReturnsCorrectCommandName()
    {
        var command = new StartCommand(_botClientMock.Object);
        command.CommandName.Should().NotBeEmpty().And.BeEquivalentTo(CommandNames.Start);
    }
}
