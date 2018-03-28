using System.Threading.Tasks;
using Test.Framework;
using TestTool.Core;

namespace TestTool.Runner.Http
{
    public class HttpPostStep : BaseHttpStep
    {
        private readonly IHttpClient httpClient;

        public HttpPostStep(
                    string url, 
                    IHttpClient httpClient,
                    IExpectedContent expectedData, 
                    bool canBeRunInParallel = false)
                                : base(
                                        url,
                                        expectedData,
                                        canBeRunInParallel )
        {
            Guard.IsNotNull(httpClient, nameof(httpClient));
            this.httpClient = httpClient;
        }

        protected override Task<string> GetUrlContent(string url)
        {
            return httpClient.Post(url);
        }
    }
}
