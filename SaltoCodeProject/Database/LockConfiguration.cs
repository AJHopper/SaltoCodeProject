using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaltoCodeProject.Entities;

namespace SaltoCodeProject.Database
{
    public class LockConfiguration : IEntityTypeConfiguration<Lock>
    {
        public void Configure(EntityTypeBuilder<Lock> builder)
        {
            builder.HasMany(x => x.AuthorisedUserIds).WithOne();
        }
    }
}
