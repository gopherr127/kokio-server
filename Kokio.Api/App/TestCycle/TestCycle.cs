using Kokio.Api.App.ProjectReleases;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Kokio.Api.App.TestCycle
{
    public class TestCycle : Entity
    {
        public string Name { get; set; }
        public ObjectId ProjectReleaseId { get; set; }

        [BsonIgnore]
        public ProjectRelease Release { get; set; }
    }
}
