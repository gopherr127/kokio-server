using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Kokio.Api.App.ProjectReleases
{
    public interface IProjectReleaseRepository
    {
        Task<ProjectRelease> Add(ProjectRelease release);
        Task Delete(ObjectId id);
        Task<ProjectRelease> Get(ObjectId id);
        Task<IEnumerable<ProjectRelease>> GetAll();
        Task<IEnumerable<ProjectRelease>> Search(ProjectReleaseSearchRequest searchRequest);
        Task<ProjectRelease> Update(ProjectRelease screen);
    }
}