using Kokio.Api.App.Projects;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Kokio.Api.App.ProjectReleases
{
    // The class is named ProjectRelease instead of Release to avoid
    // any potential conflicts with Visual Studio or similar tools.
    public class ProjectRelease : Entity
    {
        public string Name { get; set; }
        public ObjectId ProjectId { get; set; }

        [BsonIgnore]
        public Project Project { get; set; }
    }
}
