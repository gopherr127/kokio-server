using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Kokio.Api.App.ProjectReleases
{
    public class ProjectReleaseContext : ContextBase
    {
        public ProjectReleaseContext(IOptions<Settings> settings) : base(settings) { }

        public IMongoCollection<ProjectRelease> Releases
        {
            get { return DefaultDatabase.GetCollection<ProjectRelease>("Releases"); }
        }
    }
}