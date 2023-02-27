using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Identity;

internal class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData(
           new IdentityUserRole<string>
           {
               RoleId = "def680c0-d1b9-48d3-a411-f3fd28375b4f",
               UserId = "0ab2a932-6fc4-4487-bfc2-f0dbd11bfdfe"
           },
            new IdentityUserRole<string>
            {
                RoleId = "b2acf7b0-f953-4e02-ae6b-6a177c6783f6",
                UserId = "676439f5-178b-4869-804d-0d88e8c3b70b"
            }
        );
    }
}
