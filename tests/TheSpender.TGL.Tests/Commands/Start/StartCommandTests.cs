using System.Globalization;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Telegram.Bot;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using TheSpender.BLL.Services.Users;
using TheSpender.TestUtils;
using TheSpender.TGL.Commands;
using TheSpender.TGL.Commands.Start;
using Xunit;
using User = TheSpender.DAL.Entities.Users.User;

namespace TheSpender.TGL.Tests.Commands.Start;

public class StartCommandTests
{
    private readonly Mock<ITelegramBotClient> _botClientMock = new();
    private readonly Mock<IUserService> _userServiceMock = new();

    [Fact]
    public async Task Execute_CorrectData_CreateUserAndSendHelloMessageToUser()
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

        _userServiceMock.Setup(x => x.CreateUser(update.Message.Chat.Id.ToString(CultureInfo.InvariantCulture),
            It.IsAny<CancellationToken>())).Returns(new User()
        {
            ClientId = "some-client-id",
        }).Verifiable(Times.Once());

        await using var dbContext = DatabaseFixture.CreateContext();

        var command = new StartCommand(dbContext, _userServiceMock.Object, _botClientMock.Object);
        await command.Execute(update, CancellationToken.None);

        var userWasCreated = await dbContext.Users.AnyAsync(CancellationToken.None);
        userWasCreated.Should().BeTrue();

        _botClientMock.Verify(x => x.SendRequest(
                It.Is<SendMessageRequest>(r => r.ChatId == update.Message.Chat.Id &&
                                               r.Text == StartCommand.HelloMessageText &&
                                               r.ReplyMarkup == Keyboards.MainMenu),
                It.IsAny<CancellationToken>()),
            Times.Once);

        _userServiceMock.Verify();
    }

    [Fact]
    public void CommandName_GetCommandName_ReturnsCorrectCommandName()
    {
        using var dbContext = DatabaseFixture.CreateContext();
        var command = new StartCommand(dbContext, _userServiceMock.Object, _botClientMock.Object);
        command.CommandName.Should().NotBeEmpty().And.BeEquivalentTo(CommandNames.Start);
    }
}
