using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configs
{
    public class FilmConfiguration : IEntityTypeConfiguration<Film>
    {
        public void Configure(EntityTypeBuilder<Film> builder)
        {
            builder
                .HasOne(f => f.Discount)
                .WithOne(d => d.Film)
                .HasForeignKey<Discount>(d => d.FilmId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(f => f.RegularDiscount)
                .WithOne(d => d.Film)
                .HasForeignKey<RegularDiscount>(d => d.FilmId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
