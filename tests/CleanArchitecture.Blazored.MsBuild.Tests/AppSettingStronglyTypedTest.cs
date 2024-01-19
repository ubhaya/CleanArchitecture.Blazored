namespace CleanArchitecture.Blazored.MsBuild.Tests;

public class AppSettingStronglyTypedTest
{
    private readonly Mock<IBuildEngine> _buildEngine = new();
    private readonly List<BuildErrorEventArgs> _errors = [];

    public AppSettingStronglyTypedTest()
    {
        _buildEngine.Setup(x => x.LogErrorEvent(It.IsAny<BuildErrorEventArgs>()))
            .Callback<BuildErrorEventArgs>(e => _errors.Add(e));
    }

    [Fact]
    public void EmptySettingFileList_EmptyClassGenerated()
    {
        // Arrange
        var appSettingStronglyTyped = new AppSettingStronglyTyped
        {
            SettingClassName = "MySettingEmpty",
            SettingNamespaceName = "MyNamespace",
            SettingFiles = Array.Empty<ITaskItem>()
        };
        appSettingStronglyTyped.BuildEngine = _buildEngine.Object;
        var expectedFile = File.ReadLines($"Resources/{nameof(AppSettingStronglyTypedTest)}/empty-class.txt");

        // Act
        var success = appSettingStronglyTyped.Execute();

        // Assert
        success.Should().BeTrue();
        _errors.Count.Should().Be(0);
        appSettingStronglyTyped.ClassNameFile.Should().Be("MySettingEmpty.generated.cs");
        File.Exists(appSettingStronglyTyped.ClassNameFile).Should().BeTrue();
        File.ReadLines(appSettingStronglyTyped.ClassNameFile)
            .SequenceEqual(expectedFile).Should().BeTrue();

        // Cleanup
        File.Delete(appSettingStronglyTyped.ClassNameFile);
    }

    [Fact]
    public void SettingFileBadFormat_NotSuccess()
    {
        // Arrange
        var item = new Mock<ITaskItem>();
        item.Setup(x => x.GetMetadata("FullPath"))
            .Returns($"Resources/{nameof(AppSettingStronglyTypedTest)}/error-prop.setting");
        var appSettingStronglyTyped = new AppSettingStronglyTyped
        {
            SettingClassName = "ErrorPropSetting",
            SettingNamespaceName = "MyNamespace",
            SettingFiles = new[] { item.Object }
        };
        appSettingStronglyTyped.BuildEngine = _buildEngine.Object;

        // Act
        var success = appSettingStronglyTyped.Execute();

        // Assert
        success.Should().BeFalse();
        _errors.Count.Should().Be(1);
        appSettingStronglyTyped.ClassNameFile.Should().BeNullOrEmpty();

        var error = _errors.First();
        error.Message.Should().Be("Incorrect line format. Valid format prop:type:defaultvalue");
        error.LineNumber.Should().Be(1);
    }

    [Fact]
    public void SettingInvalidType_NotSuccess()
    {
        // Arrange
        var item = new Mock<ITaskItem>();
        item.Setup(x => x.GetMetadata("FullPath"))
            .Returns($"Resources/{nameof(AppSettingStronglyTypedTest)}/notvalidtype-prop.setting");
        var appSettingStronglyTyped = new AppSettingStronglyTyped
        {
            SettingClassName = "ErrorPropSetting",
            SettingNamespaceName = "MyNamespace",
            SettingFiles = new[] { item.Object }
        };
        appSettingStronglyTyped.BuildEngine = _buildEngine.Object;

        // Act
        var success = appSettingStronglyTyped.Execute();

        // Assert
        success.Should().BeFalse();
        _errors.Count.Should().Be(1);
        appSettingStronglyTyped.ClassNameFile.Should().BeNullOrEmpty();
        _errors.First().Message.Should().Be("Type not supported -> car");
    }

    [Fact]
    public void SettingInvalidValue_NotSuccess()
    {
        // Arrange
        var item = new Mock<ITaskItem>();
        item.Setup(x => x.GetMetadata("FullPath"))
            .Returns($"Resources/{nameof(AppSettingStronglyTypedTest)}/notvalidvalue-prop.setting");
        var appSettingStronglyTyped = new AppSettingStronglyTyped
        {
            SettingClassName = "ErrorPropSetting",
            SettingNamespaceName = "MyNamespace",
            SettingFiles = new[] { item.Object }
        };
        appSettingStronglyTyped.BuildEngine = _buildEngine.Object;

        // Act
        var success = appSettingStronglyTyped.Execute();

        // Assert
        success.Should().BeFalse();
        _errors.Count.Should().Be(1);
        appSettingStronglyTyped.ClassNameFile.Should().BeNullOrEmpty();
        _errors.First().Message.Should().Be("It is not possible parse some value based on the type -> bool - awsome");
    }

    [Theory]
    [InlineData("string")]
    [InlineData("int")]
    [InlineData("bool")]
    [InlineData("guid")]
    [InlineData("long")]
    public void SettingFileWithProperty_ClassGeneratedWithOneProperty(string value)
    {
        // Arrange
        var item = new Mock<ITaskItem>();
        item.Setup(x => x.GetMetadata("FullPath"))
            .Returns($"Resources/{nameof(AppSettingStronglyTypedTest)}/{value}-prop.setting");
        var appSettingStronglyTyped = new AppSettingStronglyTyped
        {
            SettingClassName = $"My{value}PropSetting",
            SettingNamespaceName = "MyNamespace",
            SettingFiles = new[] { item.Object }
        };
        appSettingStronglyTyped.BuildEngine = _buildEngine.Object;

        // Act
        var success = appSettingStronglyTyped.Execute();

        // Assert
        success.Should().BeTrue();
        _errors.Count.Should().Be(0);
        appSettingStronglyTyped.ClassNameFile.Should().Be($"My{value}PropSetting.generated.cs");
        File.Exists(appSettingStronglyTyped.ClassNameFile).Should().BeTrue();
        File.ReadLines(appSettingStronglyTyped.ClassNameFile)
            .SequenceEqual(File.ReadLines($"Resources/{nameof(AppSettingStronglyTypedTest)}/{value}-prop-class.txt"))
            .Should().BeTrue();

        // Cleanup
        File.Delete(appSettingStronglyTyped.ClassNameFile);
    }

    [Fact]
    public void SettingFileWithMultipleProperty_ClassGeneratedWithMultipleProperty()
    {
        // Arrange
        var item = new Mock<ITaskItem>();
        item.Setup(x => x.GetMetadata("FullPath")).Returns($"Resources/{nameof(AppSettingStronglyTypedTest)}/complete-prop.setting");
        var appSettingStronglyTyped = new AppSettingStronglyTyped
        {
            SettingClassName = $"MyCompletePropSetting",
            SettingNamespaceName = "MyNamespace",
            SettingFiles = new[] { item.Object }
        };
        appSettingStronglyTyped.BuildEngine = _buildEngine.Object;

        // Act
        var success = appSettingStronglyTyped.Execute();

        // Assert
        success.Should().BeTrue();
        _errors.Count.Should().Be(0);
        appSettingStronglyTyped.ClassNameFile.Should().Be("MyCompletePropSetting.generated.cs");
        File.Exists(appSettingStronglyTyped.ClassNameFile).Should().BeTrue();
        File.ReadLines(appSettingStronglyTyped.ClassNameFile)
            .SequenceEqual(File.ReadLines($"Resources/{nameof(AppSettingStronglyTypedTest)}/complete-prop-class.txt"))
            .Should().BeTrue();

        // Cleanup
        File.Delete(appSettingStronglyTyped.ClassNameFile);
    }
}