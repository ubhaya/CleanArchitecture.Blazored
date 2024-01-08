using System.Net;
using CleanArchitecture.Blazored.WebUi.AcceptanceTests.Helpers;
using Ductus.FluentDocker.Builders;
using Ductus.FluentDocker.Common;
using Ductus.FluentDocker.Extensions;
using Ductus.FluentDocker.Model.Compose;
using Ductus.FluentDocker.Services;
using Ductus.FluentDocker.Services.Extensions;
using Ductus.FluentDocker.Services.Impl;

namespace CleanArchitecture.Blazored.WebUi.AcceptanceTests.Hooks;

public class WebUiDockerCompose : DockerComposeTestBase
{
    public static WebUiDockerCompose Up()
    {
        return new WebUiDockerCompose();
    }

    protected override void OnContainerInitialized()
    {
        var webUiService = CompositeService.Containers.FirstOrDefault(s => s.Name == "WebUi");
        webUiService.Wait((service, cnt) =>
        {
            if (cnt > 60) throw new FluentDockerException("Failed to wait for WebUi service");
            var res = $"{ConfigurationHelper.GetBaseUrl()}/health".DoRequest().Result;
            return (res.Code == HttpStatusCode.OK) ? 0 : 500;
        });
    }

    protected override DockerComposeCompositeService Build()
    {
        var dockerCompose = GetDockerComposeLocation(ConfigurationHelper.GetDockerComposeFileName());

        
        
        return new DockerComposeCompositeService(
            DockerHost,
            new DockerComposeConfig
            {
                ComposeFilePath = [dockerCompose],
                ForceRecreate = true,
                RemoveOrphans = true,
                StopOnDispose = true,
                // ContainerConfiguration =
                // {
                //     { "WebUi", new ContainerSpecificConfig
                //     {
                //         WaitLambda = {
                //             (service, cnt) =>
                //             {
                //                 if (cnt > 60) throw new FluentDockerException("Failed to wait for WebUi service");
                //                 var res = $"{ConfigurationHelper.GetBaseUrl()}/health".DoRequest().Result;
                //                 return (res.Code == HttpStatusCode.OK) ? 0 : 500;
                //             }}
                //     }}
                // }
            });
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