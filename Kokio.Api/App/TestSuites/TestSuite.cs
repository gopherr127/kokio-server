using MongoDB.Bson;

namespace Kokio.Api.App.TestSuites
{
    public class TestSuite : Entity
    {
        public string Name { get; set; }
        public ObjectId ParentId { get; set; }
    }
}
