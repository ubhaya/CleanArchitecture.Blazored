using Ductus.FluentDocker.Builders;
using Ductus.FluentDocker.Common;
using Ductus.FluentDocker.Services;
using Ductus.FluentDocker.Services.Extensions;
using Ductus.FluentDocker.Services.Impl;

namespace CleanArchitecture.Blazored.WebUi.AcceptanceTests.Helpers;

public abstract class DockerComposeTestBase : IDisposable
{
    protected ICompositeService CompositeService;
    protected IHostService? DockerHost;

    public DockerComposeTestBase()
    {
        EnsureDockerHost();

        // ReSharper disable VirtualMemberCallInConstructor
        CompositeService = Build();
        // ReSharper restore VirtualMemberCallInConstructor
        try
        {
            CompositeService.Start();
            //
            // foreach (var container in CompositeService.Containers)
            // {
            //     container.WaitForHealthy();
            // }
        }
        catch
        {
            CompositeService.Dispose();
            throw;
        }

        // ReSharper disable VirtualMemberCallInConstructor
        OnContainerInitialized();
        // ReSharper restore VirtualMemberCallInConstructor
    }

    public void Dispose()
    {
        OnContainerTearDown();
        var compositeService = CompositeService;
        CompositeService = null!;
        try
        {
            compositeService.Stop();
            compositeService.Dispose();
        }
        catch
        {
            Ignore();
        }
    }

    protected abstract DockerComposeCompositeService Build();

    protected virtual void OnContainerTearDown()
    {
    }

    protected virtual void OnContainerInitialized()
    {
    }

    private void EnsureDockerHost()
    {
        if (DockerHost?.State == ServiceRunningState.Running) return;
        
        var hosts  = new Hosts().Discover();
        DockerHost = hosts.FirstOrDefault(x => x.IsNative) ?? hosts.FirstOrDefault(x => !x.IsNative);

        if (DockerHost is not null)
        {
            if (DockerHost.State != ServiceRunningState.Running) DockerHost.Start();
            
            return;
        }

        if (hosts.Count > 0) DockerHost = hosts.First();
        
        if (DockerHost is not null) return;
        
        EnsureDockerHost();
    }
    
    private void Ignore()
    {
    }
}