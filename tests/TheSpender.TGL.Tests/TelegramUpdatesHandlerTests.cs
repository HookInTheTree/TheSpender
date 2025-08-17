using Microsoft.Extensions.Logging;
using Moq;
using Telegram.Bot;
using Telegram.Bot.Types;
using TheSpender.TGL.Commands;
using Xunit;

namespace TheSpender.TGL.Tests;

public class TelegramUpdatesHandlerTests
{
    private readonly Mock<ICommand> _supportedCommandMock;
    private readonly Mock<ICommand> _unsupportedCommandMock;
    private readonly Mock<ILogger<TelegramUpdatesHandler>> _loggerMock = new();
    private readonly Mock<ITelegramBotClient> _tgBotCLientMock = new();
    private readonly List<ICommand> _commands;

    public TelegramUpdatesHandlerTests()
    {
        _supportedCommandMock = new Mock<ICommand>();
        _supportedCommandMock.Setup(x => x.CommandName).Returns(CommandNames.MyBalance);

        _unsupportedCommandMock = new Mock<ICommand>();
        _unsupportedCommandMock.Setup(x => x.CommandName).Returns(CommandNames.UnsupportedCommand);

        _commands =
        [
            _supportedCommandMock.Object,
            _unsupportedCommandMock.Object
        ];
    }

    [Fact]
    public async Task HandleUpdateAsync_SupportedUpdate_CorrectCommandInvoked()
    {
        var update = new Update()
        {
            CallbackQuery = new()
            {
                Data = _supportedCommandMock.Object.CommandName
            }
        };

        var handler = new TelegramUpdatesHandler(_commands, _loggerMock.Object);

        await handler.HandleUpdateAsync(_tgBotCLientMock.Object, update, CancellationToken.None);

        _unsupportedCommandMock.Verify(x => x.Execute(update, CancellationToken.None), Times.Never);
        _supportedCommandMock.Verify(x => x.Execute(update, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task HandleUpdateAsync_UnsupportedUpdate_UnsupportedCommandInvoked()
    {
        var update = new Update()
        {
            Message = new()
            {
                Text = "some-unknown-command"
            }
        };

        var handler = new TelegramUpdatesHandler(_commands, _loggerMock.Object);

        await handler.HandleUpdateAsync(_tgBotCLientMock.Object, update, CancellationToken.None);

        _supportedCommandMock.Verify(x => x.Execute(update, CancellationToken.None), Times.Never);
        _unsupportedCommandMock.Verify(x => x.Execute(update, CancellationToken.None), Times.Once);
    }
}
