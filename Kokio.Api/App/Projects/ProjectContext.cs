using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Kokio.Api.App.Projects
{
    public class ProjectContext : ContextBase
    {
        public ProjectContext(IOptions<Settings> settings) : base(settings) { }

        public IMongoCollection<Project> Projects
        {
            get { return DefaultDatabase.GetCollection<Project>("Projects"); }
        }
    }
}
