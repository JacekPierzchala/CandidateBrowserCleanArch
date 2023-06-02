using CandidateBrowserCleanArch.Application;
using CandidateBrowserCleanArch.Domain;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Persistence;

internal class CandidateCachedRepository :GenericRepository<Candidate>, ICandidateRepository
{
    private readonly ICandidateRepository _decorated;
    private readonly IMemoryCache _memoryCache;

    public CandidateCachedRepository(CandidatesBrowserDbContext dbContext,
        ICandidateRepository decorated,
        IMemoryCache memoryCache) : base(dbContext)
    {
        _decorated = decorated;
        _memoryCache = memoryCache;
    }


    public async Task DeleteCandidateAsync(int id)
    {
        await _decorated.DeleteCandidateAsync(id);
    }

    public async Task<PagedResultResponse<Candidate>> 
        GetAllActiveCandidatesWithDetailsAsync(CandidateQueryParameters QueryParameters)
    {
        string key = "candidateList";
        return await _memoryCache.GetOrCreateAsync(
            key,
            async entry  =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));
                return await _decorated.GetAllActiveCandidatesWithDetailsAsync(QueryParameters);
            } );
                        
    }

    public async Task<Candidate> GetCandidateWithDetailsAsync(int id)
                 => await _decorated.GetCandidateWithDetailsAsync(id);
 

}
