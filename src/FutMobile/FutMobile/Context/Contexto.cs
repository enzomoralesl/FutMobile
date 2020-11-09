using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using FutMobile.Models;

namespace FutMobile.Context
{
    public class Contexto : DbContext
    {
        public DbSet<Post> Post { get; set; }

        public Contexto() : base("DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<Contexto>(null);
            base.OnModelCreating(modelBuilder);
        }

        public System.Data.Entity.DbSet<FutMobile.Models.EditUserViewModel> EditUserViewModels { get; set; }

        public System.Data.Entity.DbSet<FutMobile.Models.MotivoModel> MotivoModels { get; set; }
    }
}