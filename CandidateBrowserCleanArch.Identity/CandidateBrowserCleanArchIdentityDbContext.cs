using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Identity
{
    public class CandidateBrowserCleanArchIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public CandidateBrowserCleanArchIdentityDbContext(DbContextOptions <CandidateBrowserCleanArchIdentityDbContext> options) 
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
             base.OnModelCreating(builder);
        }
    }
}
