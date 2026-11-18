using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Time.Testing;
using ShadcnBlazor;

namespace Web.Tests.Infrastructure;

/// <summary>
/// Base class for bUnit component tests. Registers services the same way
/// <c>src/Web/Program.cs</c> does, so ShadcnBlazor components render under bUnit.
/// Test classes inherit from it; xUnit creates one instance (and so one context) per test.
/// </summary>
public abstract class WebTestContext : BunitContext
{
    /// <summary>Fake clock registered as <see cref="TimeProvider"/>; advance it in time-based tests.</summary>
    protected FakeTimeProvider Time { get; } = new(new DateTimeOffset(2026, 1, 1, 12, 0, 0, TimeSpan.Zero));

    protected WebTestContext()
    {
        // ShadcnBlazor components call into JS modules; tests don't assert on those calls by default.
        JSInterop.Mode = JSRuntimeMode.Loose;

        Services.AddShadcnBlazor();
        Services.AddSingleton<TimeProvider>(Time);
    }
}
