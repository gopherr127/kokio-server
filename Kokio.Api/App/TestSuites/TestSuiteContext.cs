using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Kokio.Api.App.TestSuites
{
    public class TestSuiteContext : ContextBase
    {
        public TestSuiteContext(IOptions<Settings> settings) : base(settings) { }

        public IMongoCollection<TestSuite> TestSuites
        {
            get { return DefaultDatabase.GetCollection<TestSuite>("TestSuites"); }
        }
    }
}
