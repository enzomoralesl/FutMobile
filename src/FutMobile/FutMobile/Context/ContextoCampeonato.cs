using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using FutMobile.Models;

namespace FutMobile.Context
{
    public class ContextoCampeonato : DbContext
    {
        public DbSet<Campeonato> Campeonato { get; set; }

        public ContextoCampeonato() : base("DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<ContextoCampeonato>(null);
            base.OnModelCreating(modelBuilder);
        }
    }
}