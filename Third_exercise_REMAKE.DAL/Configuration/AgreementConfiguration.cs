using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Third_exercise_REMAKE.Core.Models;

namespace Third_exercise_REMAKE.DAL.Configuration
{
    public class AgreementConfiguration : IEntityTypeConfiguration<AgreementModel>
    {
        public void Configure(EntityTypeBuilder<AgreementModel> builder)
        {
            builder
                .ToTable("Agreements")
                .HasKey(e => e.Id);

            builder
                .Property(e => e.Id)
                .UseIdentityColumn();
        }
    }
}
