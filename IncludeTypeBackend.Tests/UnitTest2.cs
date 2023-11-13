namespace IncludeTypeBackend.Tests;

public class UnitTest2
{
    [Test]
    public async Task CheckForUserTestPositive()
    {
        const string URL = @"https://localhost:5001/api/user/checkforuser/SubhamK108";
        using HttpClient client = new();
        using var result = await client.GetAsync(URL);
        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        string resultMsg = await result.Content.ReadAsStringAsync();
        Assert.That(Convert.ToBoolean(resultMsg), Is.True);
    }

    [Test]
    public async Task CheckForUserTestNegative()
    {
        const string URL = @"https://localhost:5001/api/user/checkforuser/abc";
        using HttpClient client = new();
        using var result = await client.GetAsync(URL);
        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        string resultMsg = await result.Content.ReadAsStringAsync();
        Assert.That(Convert.ToBoolean(resultMsg), Is.False);
    }

    [Test]
    public async Task CheckPasswordTestPositive()
    {
        const string URL = @"https://localhost:5001/api/user/checkpassword/SubhamK108-1234567890";
        using HttpClient client = new();
        using var result = await client.GetAsync(URL);
        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        string resultMsg = await result.Content.ReadAsStringAsync();
        Assert.That(Convert.ToBoolean(resultMsg), Is.True);
    }

    [Test]
    public async Task CheckPasswordTestNegative()
    {
        const string URL = @"https://localhost:5001/api/user/checkpassword/SubhamK108-123457890";
        using HttpClient client = new();
        using var result = await client.GetAsync(URL);
        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        string resultMsg = await result.Content.ReadAsStringAsync();
        Assert.That(Convert.ToBoolean(resultMsg), Is.False);
    }
}
