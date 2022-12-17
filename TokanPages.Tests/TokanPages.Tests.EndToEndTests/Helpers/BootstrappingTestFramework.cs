using Xunit.Abstractions;
using Xunit.Sdk;

[assembly: Xunit.TestFramework(
    "TokanPages.Tests.EndToEndTests.Helpers.TestBootstrap", 
    "TokanPages.Tests.EndToEndTests")
]

namespace TokanPages.Tests.EndToEndTests.Helpers;

public class BootstrappingTestFramework : XunitTestFramework, IDisposable
{
    public BootstrappingTestFramework(IMessageSink messageSink) : base(messageSink)
    {
        //TODO: add initialization code
    }

    public new void Dispose()
    {
        //TODO: add tear down code
        base.Dispose();
    }
}