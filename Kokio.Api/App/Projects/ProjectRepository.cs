using Kokio.Api.App.ProjectReleases;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kokio.Api.App.Projects
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ProjectContext _context = null;

        public ProjectRepository(IOptions<Settings> settings)
        {
            _context = new ProjectContext(settings);
        }

        private async Task<Project> HydrateForGet(Project project)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return project;
        }

        public async Task<IEnumerable<Project>> GetAll()
        {
            try
            {
                return await _context.Projects.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Project> Get(ObjectId id)
        {
            try
            {
                var filter = Builders<Project>.Filter.Eq("_id", id);

                var project = await _context.Projects.Find(filter).FirstOrDefaultAsync();

                return await HydrateForGet(project);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Project>> Search(ProjectSearchRequest searchRequest)
        {
            try
            {
                List<FilterDefinition<Project>> filters = new List<FilterDefinition<Project>>();

                if (!string.IsNullOrEmpty(searchRequest.Name))
                    filters.Add(Builders<Project>.Filter.Regex("Name", new BsonRegularExpression($".*{searchRequest.Name}.*")));

                var filterConcat = Builders<Project>.Filter.And(filters);

                return await _context.Projects.Find(filterConcat).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Project> Add(Project project)
        {
            try
            {
                if (string.IsNullOrEmpty(project.Name))
                    throw new ApplicationException("Name is required.");

                var searchResult = await Search(new ProjectSearchRequest()
                {
                    Name = project.Name
                });

                if (searchResult.Count() > 0)
                {
                    throw new ApplicationException("A project with that name already exists.");
                }

                await _context.Projects.InsertOneAsync(project);

                return await Get(project.Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Project> Update(Project project)
        {
            try
            {
                var filter = Builders<Project>.Filter.Eq("_id", project.Id);

                await _context.Projects.ReplaceOneAsync(filter, project);

                return await Get(project.Id);
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
                var filter = Builders<Project>.Filter.Eq("_id", id);

                await _context.Projects.DeleteOneAsync(filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
