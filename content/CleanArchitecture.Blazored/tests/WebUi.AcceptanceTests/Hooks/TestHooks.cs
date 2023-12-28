using System.Net;
using Ductus.FluentDocker.Builders;
using Ductus.FluentDocker.Common;
using Ductus.FluentDocker.Services;
using Ductus.FluentDocker.Services.Extensions;
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
            .UseHealthCheck("curl --fail http://localhost:5000/health || exit 1")
            .UseCompose()
            .FromFile(dockerComposePath)
            .RemoveOrphans()
            .Wait("webui",Continuation)
            .WaitForHttp("webui", $"{confirmationUrl}/health",
                continuation: Continuation)
            .Build().Start();
    }

    private static int Continuation(IContainerService service, int arg2)
    {
        service.WaitForHealthy();
        return 0;
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