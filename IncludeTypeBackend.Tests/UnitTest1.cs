namespace IncludeTypeBackend.Tests;

public class UnitTest1
{
    [Test]
    public async Task GetAllUsersTest()
    {
        const string URL = @"https://localhost:5001/api/user/";
        using HttpClient client = new();
        using var result = await client.GetAsync(URL);
        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public async Task GetUserTestPositive()
    {
        const string URL = @"https://localhost:5001/api/user/getuser/SubhamK108";
        using HttpClient client = new();
        using var result = await client.GetAsync(URL);
        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public async Task GetUserTestNegative()
    {
        const string URL = @"https://localhost:5001/api/user/getuser/";
        using HttpClient client = new();
        using var result = await client.GetAsync(URL);
        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }
}