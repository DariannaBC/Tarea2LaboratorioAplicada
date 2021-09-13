using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tarea2LaboratorioAplicada.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Tarea2LaboratorioAplicada.DAL
{
    public class Contexto : DbContext

    {
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Usuario> Usuario { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data source= Data\UserControl.db");
        }
    }
}
