using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using FutMobile.Models;

namespace FutMobile.Context
{
    public class ContextoComentario : DbContext
    {
        public DbSet<Comentario> Comentario { get; set; }

        public ContextoComentario() : base("DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<Contexto>(null);
            base.OnModelCreating(modelBuilder);
        }
    }
}