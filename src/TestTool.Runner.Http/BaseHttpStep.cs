using System.Threading.Tasks;
using Test.Framework;
using TestTool.Core;
using TestTool.Core.Model;
using TestTool.Core.Model.Results;

namespace TestTool.Runner.Http
{
    public abstract class BaseHttpStep : ITestStep
    {
        private readonly string url;
        private readonly IHttpClient httpClient;
        private readonly IExpectedContent expectedData;
        private readonly IJsonResultParser jsonResultParser;

        public BaseHttpStep(string url, IExpectedContent expectedData, bool canBeRunInParallel = false)
        {
            Guard.IsNotNull(url, nameof(url));
            Guard.IsNotNull(httpClient, nameof(httpClient));
            Guard.IsNotNull(expectedData, nameof(expectedData));

            this.CanBeRunInParallel = canBeRunInParallel;
        }

        public bool CanBeRunInParallel { get; }

        public string Name => throw new System.NotImplementedException();

        public async Task<IStepRunResult> Run(ITestContext testContext)
        {
            try
            {
                string results = await GetUrlContent(url);
                IActualContent actualResult = jsonResultParser.ParseJson(results);
                return new ComparisonResult(this, expectedData, actualResult);
            }
            catch (System.Exception ex)
            {
                return new ErrorResult(this, ex);
            }
        }

        protected abstract Task<string> GetUrlContent(string url);

    }
}
