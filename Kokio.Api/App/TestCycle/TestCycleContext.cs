using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Kokio.Api.App.TestCycle
{
    public class TestCycleContext : ContextBase
    {
        public TestCycleContext(IOptions<Settings> settings) : base(settings) { }

        public IMongoCollection<TestCycle> TestCycles
        {
            get { return DefaultDatabase.GetCollection<TestCycle>(" TestCycles"); }
        }
    }
}
