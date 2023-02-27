using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Identity;

internal class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
        new IdentityRole
        {
            Id = "def680c0-d1b9-48d3-a411-f3fd28375b4f",
            Name = "Admin",
            NormalizedName = "ADMIN"
        },
          new IdentityRole
          {
              Id = "b2acf7b0-f953-4e02-ae6b-6a177c6783f6",
              Name = "User",
              NormalizedName = "USER"
          }
        );
    }
}
