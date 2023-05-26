using CandidateBrowserCleanArch.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Persistence
{
    public class AuditableDbContext:DbContext
    {
        public AuditableDbContext(DbContextOptions options)
            :base(options) { }


        public virtual async Task<bool> SaveChangesAsync(string username = "SYSTEM")
        {
            foreach (var entry in base.ChangeTracker.Entries<IAuditableEntity>()
                .Where(q => q.State == EntityState.Modified || q.State == EntityState.Added))
            {
                entry.Entity.ModifiedDate = DateTime.UtcNow;
                entry.Entity.ModifiedBy = username;
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedDate = DateTime.UtcNow;
                    entry.Entity.CreatedBy = username;
                }
            }
            try
            {
                await base.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
