using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Kokio.Api.App.TestCases
{
    public interface ITestCaseRepository
    {
        Task<TestCase> Add(TestCase testCase);
        Task Delete(ObjectId id);
        Task<TestCase> Get(ObjectId id);
        Task<IEnumerable<TestCase>> GetAll();
        Task<IEnumerable<TestCase>> Search(TestCaseSearchRequest searchRequest);
        Task<TestCase> Update(TestCase testCase);
    }
}