using MongoDB.Bson;

namespace Kokio.Api.App.TestCases
{
    public class TestCaseSearchRequest
    {
        public ObjectId TestSuiteId { get; set; }

        public string Name { get; set; }
    }
}