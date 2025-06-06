﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DataAccess.Entity;
using System.Reflection.Emit;

namespace DataAccess.Configs
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder
                .HasOne(t => t.Session)
                .WithMany(s => s.Tickets)
                .HasForeignKey(t => t.SessionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(t => t.Status)
                .WithMany(s => s.Tickets)
                .HasForeignKey(t => t.StatusId)
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .HasOne(t => t.Sale)
                .WithMany(s => s.Tickets)
                .HasForeignKey(t => t.SaleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(t => t.Cart)
                .WithMany(c => c.Tickets)
                .HasForeignKey(t => t.CartId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
