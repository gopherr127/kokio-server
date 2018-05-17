using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Threading.Tasks;

namespace Kokio.Api.App.Projects
{
    [Route("api/[controller]")]
    public class ProjectsController : Controller
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectsController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        [Route("{id:length(24)}")]
        [Authorize("read:projects")]
        [HttpGet]
        public async Task<IActionResult> Get(string id)
        {
            return await FunctionWrapper.ExecuteFunction(this, async () =>
            {
                return await _projectRepository.Get(new ObjectId(id));
            });
        }

        [Authorize("read:projects")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await FunctionWrapper.ExecuteFunction(this, async () =>
            {
                return await _projectRepository.GetAll();
            });
        }

        [Route("search")]
        [Authorize("read:projects")]
        [HttpGet]
        public async Task<IActionResult> Search([FromQuery]ProjectSearchRequest searchRequest)
        {
            return await FunctionWrapper.ExecuteFunction(this, async () =>
            {
                return await _projectRepository.Search(searchRequest);
            });
        }

        [Authorize("modify:projects")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Project project)
        {
            return await FunctionWrapper.ExecuteFunction(this, async () =>
            {
                return await _projectRepository.Add(project);
            });
        }

        [Route("{id:length(24)}")]
        [Authorize("modify:projects")]
        [HttpPut]
        public async Task<IActionResult> Put(string id, [FromBody]Project project)
        {
            return await FunctionWrapper.ExecuteFunction(this, async () =>
            {
                project.Id = new ObjectId(id);
                return await _projectRepository.Update(project);
            });
        }

        [Route("{id:length(24)}")]
        [Authorize("delete:projects")]
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
