using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Threading.Tasks;

namespace Kokio.Api.App.ProjectReleases
{
    [Route("api/[controller]")]
    public class ReleasesController : Controller
    {
        private readonly IProjectReleaseRepository _projectRepository;

        public ReleasesController(IProjectReleaseRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        [Route("{id:length(24)}")]
        [HttpGet]
        public async Task<IActionResult> Get(string id)
        {
            return await FunctionWrapper.ExecuteFunction(this, async () =>
            {
                return await _projectRepository.Get(new ObjectId(id));
            });
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await FunctionWrapper.ExecuteFunction(this, async () =>
            {
                return await _projectRepository.GetAll();
            });
        }

        [Route("search")]
        [HttpGet]
        public async Task<IActionResult> Search([FromQuery]ProjectReleaseSearchRequest searchRequest)
        {
            return await FunctionWrapper.ExecuteFunction(this, async () =>
            {
                return await _projectRepository.Search(searchRequest);
            });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ProjectRelease release)
        {
            return await FunctionWrapper.ExecuteFunction(this, async () =>
            {
                return await _projectRepository.Add(release);
            });
        }

        [Route("{id:length(24)}")]
        [HttpPut]
        public async Task<IActionResult> Put(string id, [FromBody]ProjectRelease release)
        {
            return await FunctionWrapper.ExecuteFunction(this, async () =>
            {
                release.Id = new ObjectId(id);
                return await _projectRepository.Update(release);
            });
        }

        [Route("{id:length(24)}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            return await FunctionWrapper.ExecuteAction(this, async () =>
            {
                await _projectRepository.Delete(new ObjectId(id));
            });
        }
    }
}
