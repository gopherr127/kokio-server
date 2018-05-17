using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Kokio.Api.App.TestCases
{
    public class TestCaseContext : ContextBase
    {
        public TestCaseContext(IOptions<Settings> settings) : base(settings) { }

        public IMongoCollection<TestCase> TestCases
        {
            get { return DefaultDatabase.GetCollection<TestCase>("TestCases"); }
        }
    }
}
