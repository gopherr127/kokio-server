using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kokio.Api.App.TestCases
{
    public class TestCaseRepository : ITestCaseRepository
    {
        private readonly TestCaseContext _context = null;

        public TestCaseRepository(IOptions<Settings> settings)
        {
            _context = new TestCaseContext(settings);
        }

        private async Task<TestCase> HydrateForGet(TestCase testCase)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return testCase;
        }

        public async Task<IEnumerable<TestCase>> GetAll()
        {
            try
            {
                return await _context.TestCases.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TestCase> Get(ObjectId id)
        {
            try
            {
                var filter = Builders<TestCase>.Filter.Eq("_id", id);

                var project = await _context.TestCases.Find(filter).FirstOrDefaultAsync();

                return await HydrateForGet(project);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<TestCase>> Search(TestCaseSearchRequest searchRequest)
        {
            try
            {
                List<FilterDefinition<TestCase>> filters = new List<FilterDefinition<TestCase>>();

                if (!string.IsNullOrEmpty(searchRequest.Name))
                    filters.Add(Builders<TestCase>.Filter.Regex("Name", new BsonRegularExpression($".*{searchRequest.Name}.*")));

                var filterConcat = Builders<TestCase>.Filter.And(filters);


                return await _context.TestCases.Find(filterConcat).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TestCase> Add(TestCase testCase)
        {
            try
            {
                if (string.IsNullOrEmpty(testCase.Name))
                    throw new ApplicationException("Name is required.");

                var searchResult = await Search(new TestCaseSearchRequest()
                {
                    Name = testCase.Name
                });

                if (searchResult.Count() > 0)
                {
                    throw new ApplicationException("A test case with that name already exists.");
                }

                await _context.TestCases.InsertOneAsync(testCase);

                return await Get(testCase.Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TestCase> Update(TestCase testCase)
        {
            try
            {
                var filter = Builders<TestCase>.Filter.Eq("_id", testCase.Id);

                await _context.TestCases.ReplaceOneAsync(filter, testCase);

                return await Get(testCase.Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Delete(ObjectId id)
        {
            try
            {
                var filter = Builders<TestCase>.Filter.Eq("_id", id);

                await _context.TestCases.DeleteOneAsync(filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
