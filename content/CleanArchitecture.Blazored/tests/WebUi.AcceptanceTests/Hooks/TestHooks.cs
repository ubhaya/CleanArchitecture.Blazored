using System.Net;
using Ductus.FluentDocker.Builders;
using Ductus.FluentDocker.Common;
using Ductus.FluentDocker.Services;
using TechTalk.SpecFlow;

namespace CleanArchitecture.Blazored.WebUi.AcceptanceTests.Hooks;

[Binding]
public class TestHooks
{
    private static ICompositeService _compositeService = default!;

    [BeforeTestRun]
    public static void DockerComposeUp()
    {
        var dockerComposeFileName = ConfigurationHelper.GetDockerComposeFileName();
        var dockerComposePath = GetDockerComposeLocation(dockerComposeFileName);

        var confirmationUrl = ConfigurationHelper.GetBaseUrl();

        _compositeService = new Builder()
            .UseContainer()
            .UseCompose()
            .FromFile(dockerComposePath)
            .RemoveOrphans()
            .WaitForHttp("webui", $"{confirmationUrl}/api/TodoItems",
                continuation: Continuation)
            .Build().Start();
    }

    private static long Continuation(RequestResponse response,int _)
    {
        return response.Code != HttpStatusCode.OK ? 2000 : 0;
    }

    [AfterTestRun]
    public static void DockerComposeDown()
    {
        _compositeService.Stop();
        _compositeService.Dispose();
    }

    private static string GetDockerComposeLocation(string dockerComposeFileName)
    {
        var directory = Directory.GetCurrentDirectory();
        while (!Directory.EnumerateFiles(directory, "*.yml").Any(s => s.EndsWith(dockerComposeFileName)))
        {
            directory = directory[..directory.LastIndexOf(Path.DirectorySeparatorChar)];
        }

        return Path.Combine(directory, dockerComposeFileName);
    }
}