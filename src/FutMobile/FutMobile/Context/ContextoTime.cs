using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using FutMobile.Models;

namespace FutMobile.Context
{
    public class ContextoTime : DbContext
    {
        public DbSet<Time> Time { get; set; }

        public ContextoTime() : base("DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<ContextoTime>(null);
            base.OnModelCreating(modelBuilder);
        }
    }
}