namespace CleanArchitecture.Blazored.MsBuild.Tests;

public class ReplaceFileTextTest
{
    private readonly Mock<IBuildEngine> _buildEngine = new();
    private readonly List<BuildErrorEventArgs> _errors = [];
    private readonly List<BuildMessageEventArgs> _messages = [];

    public ReplaceFileTextTest()
    {
        _buildEngine.Setup(x => x.LogErrorEvent(It.IsAny<BuildErrorEventArgs>()))
            .Callback<BuildErrorEventArgs>(e => _errors.Add(e));
        _buildEngine.Setup(x => x.LogMessageEvent(It.IsAny<BuildMessageEventArgs>()))
            .Callback<BuildMessageEventArgs>(e => _messages.Add(e));
    }

    [Theory]
    [MemberData(nameof(ReplaceFileTextTestData))]
    public void ReplaceFileText_ReplacesText_IfMatchFound(string fileName, string match, string replace, bool expected)
    {
        // Arrange
        var oldContent = File.ReadAllText(fileName);
        var replaceFileText = new ReplaceFileText
        {
            FileName = fileName,
            MatchExpression = match,
            ReplacementText = replace,
        };
        replaceFileText.BuildEngine = _buildEngine.Object;

        // Act
        var success = replaceFileText.Execute();

        // Assert
        success.Should().Be(expected);
        _errors.Should().BeEmpty();
        var message = _messages.First();
        message.Message.Should().Be($"Replacing {match}->{replace} in {fileName}");
    }

    public static IEnumerable<object[]> ReplaceFileTextTestData => 
    [
        [$"Resources/{nameof(ReplaceFileTextTest)}/ExampleFile.Contains.cs","Example_Contains","Example_Replace",true],
        [$"Resources/{nameof(ReplaceFileTextTest)}/ExampleFile.Contains.cs","Example_NotContains","Example_Replace",true],
    ];
}