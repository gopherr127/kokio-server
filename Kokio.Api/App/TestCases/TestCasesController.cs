using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Threading.Tasks;

namespace Kokio.Api.App.TestCases
{
    [Route("api/[controller]")]
    public class TestCasesController : Controller
    {
        private readonly ITestCaseRepository _testCaseRepository;

        public TestCasesController(ITestCaseRepository projectRepository)
        {
            _testCaseRepository = projectRepository;
        }

        [Route("{id:length(24)}")]
        [HttpGet]
        public async Task<IActionResult> Get(string id)
        {
            return await FunctionWrapper.ExecuteFunction(this, async () =>
            {
                return await _testCaseRepository.Get(new ObjectId(id));
            });
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await FunctionWrapper.ExecuteFunction(this, async () =>
            {
                return await _testCaseRepository.GetAll();
            });
        }

        [Route("search")]
        [HttpGet]
        public async Task<IActionResult> Search([FromQuery]TestCaseSearchRequest searchRequest)
        {
            return await FunctionWrapper.ExecuteFunction(this, async () =>
            {
                return await _testCaseRepository.Search(searchRequest);
            });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]TestCase testCaseR)
        {
            return await FunctionWrapper.ExecuteFunction(this, async () =>
            {
                return await _testCaseRepository.Add(testCaseR);
            });
        }

        [Route("{id:length(24)}")]
        [HttpPut]
        public async Task<IActionResult> Put(string id, [FromBody]TestCase testCaseR)
        {
            return await FunctionWrapper.ExecuteFunction(this, async () =>
            {
                testCaseR.Id = new ObjectId(id);
                return await _testCaseRepository.Update(testCaseR);
            });
        }

        [Route("{id:length(24)}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            return await FunctionWrapper.ExecuteAction(this, async () =>
            {
                await _testCaseRepository.Delete(new ObjectId(id));
            });
        }
    }
}
