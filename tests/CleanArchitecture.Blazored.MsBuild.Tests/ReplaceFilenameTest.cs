namespace CleanArchitecture.Blazored.MsBuild.Tests;

public class ReplaceFilenameTest
{
    private readonly Mock<IBuildEngine> _buildEngine = new();
    private readonly List<BuildErrorEventArgs> _errors = [];
    private readonly List<BuildMessageEventArgs> _messages = [];

    public ReplaceFilenameTest()
    {
        _buildEngine.Setup(x => x.LogErrorEvent(It.IsAny<BuildErrorEventArgs>()))
            .Callback<BuildErrorEventArgs>(e => _errors.Add(e));
        _buildEngine.Setup(x => x.LogMessageEvent(It.IsAny<BuildMessageEventArgs>()))
            .Callback<BuildMessageEventArgs>(e => _messages.Add(e));
    }

    [Fact]
    public void ReplaceFilename_WhenFileNameDoesNotMatchExpression_Success()
    {
        // Arrange
        var replaceFilename = new ReplaceFilename
        {
            Filename = $"Resources/{nameof(ReplaceFilenameTest)}/ExampleFile.Success.cs",
            MatchExpression = "ExampleFile.Replace",
            ReplacementText = "ExampleFile.NewText"
        };
        replaceFilename.BuildEngine = _buildEngine.Object;

        // Act
        var success = replaceFilename.Execute();

        // Assert
        success.Should().BeTrue();
        _errors.Count.Should().Be(0);
        _messages.Count.Should().Be(0);
        File.Exists($"Resources/{nameof(ReplaceFilenameTest)}/ExampleFile.NewText.cs")
            .Should().BeFalse();
    }

    [Fact]
    public void ReplaceFilename_WhenInvalidFilename_FailedWithError()
    {
        // Arrange
        var replaceFilename = new ReplaceFilename
        {
            Filename = $"ExampleFile.Match.cs",
            MatchExpression = "ExampleFile.Match",
            ReplacementText = "ExampleFile.NewText"
        };
        replaceFilename.BuildEngine = _buildEngine.Object;

        // Act
        var success = replaceFilename.Execute();

        // Assert
        success.Should().BeFalse();
        _errors.First().Message.Should().Be($"path variable cannot be null in ExampleFile.Match.cs");
        _messages.Count.Should().Be(0);
    }

    [Fact]
    public void ReplaceFilename_WhenValidFilename_SuccessWithMessage()
    {
        // Arrange
        const string oldFileName = $"Resources/{nameof(ReplaceFilenameTest)}/ExampleFile.Match.cs";
        const string newFileName = $"Resources\\{nameof(ReplaceFilenameTest)}\\ExampleFile.NewText.cs";
        var replaceFilename = new ReplaceFilename
        {
            Filename = oldFileName,
            MatchExpression = "ExampleFile.Match",
            ReplacementText = "ExampleFile.NewText"
        };
        replaceFilename.BuildEngine = _buildEngine.Object;

        // Act
        var success = replaceFilename.Execute();

        // Assert
        success.Should().BeTrue();
        _errors.Count.Should().Be(0);
        var message = _messages.First();
        message.Message.Should().Be($"{oldFileName} -> {newFileName}");
        message.Importance.Should().Be(MessageImportance.High);
        File.Exists(oldFileName)
            .Should().BeFalse();
        File.Exists(newFileName)
            .Should().BeTrue();
        
        // Cleanup
        File.Move(newFileName, oldFileName);
    }
}