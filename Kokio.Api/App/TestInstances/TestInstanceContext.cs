using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Kokio.Api.App.TestInstances
{
    public class TestInstanceContext : ContextBase
    {
        public TestInstanceContext(IOptions<Settings> settings) : base(settings) { }

        public IMongoCollection<TestInstance> TestInstances
        {
            get { return DefaultDatabase.GetCollection<TestInstance>("TestInstances"); }
        }
    }
}