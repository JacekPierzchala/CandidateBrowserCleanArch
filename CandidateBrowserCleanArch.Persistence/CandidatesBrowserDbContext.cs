using CandidateBrowserCleanArch.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Persistence;

public class CandidatesBrowserDbContext : AuditableDbContext
{
    public CandidatesBrowserDbContext(DbContextOptions<CandidatesBrowserDbContext> dbContext)
        : base(dbContext) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly
            (typeof(CandidatesBrowserDbContext).Assembly);


    public DbSet<Candidate> Candidates { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<CandidateCompany> CandidateCompanies { get; set; }
    public DbSet<CandidateProject> CandidateProjects { get; set; }
}
