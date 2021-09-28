
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabajoPracticoVerdadero.Comunes.datos.Entidades;

namespace TrabajoPracticoVerdadero.Comunes.datos
{
    public class dbContext:DbContext
    {

            
        public DbSet<Producto> Productos { get; set; }
         public DbSet<Cliente> Clientes { get; set; }

        public dbContext(DbContextOptions<dbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {
            var cascadeFKs = modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            }

            base.OnModelCreating(modelBuilder);

        }




    }
}
