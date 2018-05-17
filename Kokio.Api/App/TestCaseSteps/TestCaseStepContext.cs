using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Kokio.Api.App.TestCaseSteps
{
    public class TestCaseStepContext : ContextBase
    {
        public TestCaseStepContext(IOptions<Settings> settings) : base(settings) { }

        public IMongoCollection<TestCaseStep> TestCaseSteps
        {
            get { return DefaultDatabase.GetCollection<TestCaseStep>("TestCaseSteps"); }
        }
    }
}
