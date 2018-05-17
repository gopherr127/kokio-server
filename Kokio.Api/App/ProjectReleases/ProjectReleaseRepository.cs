using Kokio.Api.App.Projects;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kokio.Api.App.ProjectReleases
{
    public class ProjectReleaseRepository : IProjectReleaseRepository
    {
        private readonly ProjectReleaseContext _context = null;
        private readonly IProjectRepository _projectRepository;

        public ProjectReleaseRepository(IOptions<Settings> settings,
            IProjectRepository projectRepository)
        {
            _context = new ProjectReleaseContext(settings);
            _projectRepository = projectRepository;
        }

        private async Task<ProjectRelease> HydrateForGet(ProjectRelease release)
        {
            try
            {
                // Hydrate Project
                if (release.ProjectId != null || release.ProjectId != ObjectId.Empty)
                {
                    release.Project = await _projectRepository.Get(release.ProjectId);
                }

                return release;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<ProjectRelease>> GetAll()
        {
            try
            {
                return await _context.Releases.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ProjectRelease> Get(ObjectId id)
        {
            try
            {
                var filter = Builders<ProjectRelease>.Filter.Eq("_id", id);

                var release = await _context.Releases.Find(filter).FirstOrDefaultAsync();

                return await HydrateForGet(release);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<ProjectRelease>> Search(ProjectReleaseSearchRequest searchRequest)
        {
            try
            {
                List<FilterDefinition<ProjectRelease>> filters = new List<FilterDefinition<ProjectRelease>>();

                if (!string.IsNullOrEmpty(searchRequest.ProjectId))
                    filters.Add(Builders<ProjectRelease>.Filter.Eq("ProjectId", new ObjectId(searchRequest.ProjectId)));

                if (!string.IsNullOrEmpty(searchRequest.Name))
                    filters.Add(Builders<ProjectRelease>.Filter.Regex("Name", new BsonRegularExpression($".*{searchRequest.Name}.*")));

                var filterConcat = Builders<ProjectRelease>.Filter.And(filters);

                return await _context.Releases.Find(filterConcat).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ProjectRelease> Add(ProjectRelease release)
        {
            try
            {
                if (string.IsNullOrEmpty(release.Name))
                    throw new ApplicationException("Name is required.");

                var searchResult = await Search(new ProjectReleaseSearchRequest()
                {
                    Name = release.Name
                });

                if (searchResult.Count() > 0)
                {
                    throw new ApplicationException("A release with that name already exists.");
                }

                await _context.Releases.InsertOneAsync(release);

                return await Get(release.Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ProjectRelease> Update(ProjectRelease release)
        {
            try
            {
                var filter = Builders<ProjectRelease>.Filter.Eq("_id", release.Id);

                await _context.Releases.ReplaceOneAsync(filter, release);

                return await Get(release.Id);
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
                var filter = Builders<ProjectRelease>.Filter.Eq("_id", id);

                await _context.Releases.DeleteOneAsync(filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
