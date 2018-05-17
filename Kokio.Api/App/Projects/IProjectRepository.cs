using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Kokio.Api.App.Projects
{
    public interface IProjectRepository
    {
        Task<Project> Add(Project project);
        Task Delete(ObjectId id);
        Task<Project> Get(ObjectId id);
        Task<IEnumerable<Project>> GetAll();
        Task<IEnumerable<Project>> Search(ProjectSearchRequest searchRequest);
        Task<Project> Update(Project screen);
    }
}