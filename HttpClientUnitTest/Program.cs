using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using Xunit;

namespace HttpClientUnitTest;
// class Program {
//     public static void Main(string[] args) {
//         var analyzer = new SiteAnalyzer(Client);
//         var size = analyzer.GetContentSize("http://microsoft.com").Result;
//         Console.WriteLine($"Size: {size}");
//     }
//
//     private static readonly HttpClient Client = new HttpClient(); // Singleton
// }

public class SiteAnalyzer(HttpClient httpClient)
{
    public async Task<int> GetContentSize(string uri)
    {
        var response = await httpClient.GetAsync( uri );
        var content = await response.Content.ReadAsStringAsync();
        return content.Length;
    }
}

public class SiteAnalyzerTests {
    [Fact]
    public async void GetContentSizeReturnsCorrectLength() {
        // Arrange
        const string testContent = "test content";
        var mockMessageHandler = new Mock<HttpMessageHandler>();
        mockMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(testContent)
            });
        var underTest = new SiteAnalyzer(new HttpClient(mockMessageHandler.Object));

        // Act
        var result = await underTest.GetContentSize("http://anyurl");

        // Assert
        Assert.Equal(testContent.Length, result);
    }
}