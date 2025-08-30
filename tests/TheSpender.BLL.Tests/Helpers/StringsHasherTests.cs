using FluentAssertions;
using Microsoft.Extensions.Options;
using TheSpender.BLL.Helpers;
using Xunit;

namespace TheSpender.BLL.Tests.Helpers;

public class StringsHasherTests
{
    private static readonly StringsHasher Hasher = new(Options.Create(new SecurityOptions()
    {
        Salt = "sdf54s;dsaf;f54aF.@)"
    }));

    [Theory]
    [InlineData("200000", "D8EB6F02AEBCF68159EBDD8CB8EB05B131475D5A9165F03DBBDB8AB1374294FD")]
    [InlineData("200001", "92DC8A9884B5416D1A741870F27419D664AE8B16F5B403798F46B98E54308F48")]
    [InlineData("живу-какую-то-жизнь", "72F25CE77A48D3358DD37C26D87BEF4D66C72C104CC89BB62058926F915CA4A3")]
    [InlineData("living-some-life", "8ED94D7716341134DB26F0B25DAE5424C4F32556E2EEBB7BFA0024BC3A38D859")]
    public void GetHash_InputIsValid_ReturnsCorrectSHA256InHex(string input, string expectedHash)
    {
        Hasher.GetHash(input).Should().Be(expectedHash);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void GetHash_InputIsInvalid_ThrowsArgumentNullException(string input)
    {
        Hasher.Invoking(x => x.GetHash(null)).Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void GetHash_TwoEqualInputs_ReturnsTwoEqualHashes()
    {
        var input = "10000001";
        var firstHash = Hasher.GetHash(input);
        var secondHash = Hasher.GetHash(input);

        firstHash.Should().Be(secondHash);
    }
}
