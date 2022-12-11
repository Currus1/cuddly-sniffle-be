using currus.Controllers;
using Microsoft.Extensions.Configuration;
using NUnit.Framework.Internal;

namespace currus.Tests.UnitTests.Controller;

[TestFixture]
public class ApiKeyControllerTests
{
    private ApiKeyController _apiKeyController;
    private IConfiguration configuration;

    private IConfiguration BuildConfiguration(string key)
    {
        var inMemorySettings = new Dictionary<string, string> {
        {"ApiKey", key},
        };

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        return configuration;
    }
    
    [Test]
    public void ApiKeyController_GetApiKey_ReturnsKeyEqual()
    {

        configuration = BuildConfiguration("");
        _apiKeyController = new ApiKeyController(configuration);
        var key1 = _apiKeyController.GetApiKey();


        configuration = BuildConfiguration("key");
        _apiKeyController = new ApiKeyController(configuration);
        var key2 = _apiKeyController.GetApiKey();

        configuration = BuildConfiguration(null);
        _apiKeyController = new ApiKeyController(configuration);
        var key3 = _apiKeyController.GetApiKey();


        Assert.That(key1, Is.EqualTo(""));
        Assert.That(key2, Is.EqualTo("key"));
        Assert.That(key3, Is.Null);
    }
}