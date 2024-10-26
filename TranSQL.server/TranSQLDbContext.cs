using Microsoft.EntityFrameworkCore;
using TranSQL.shared.models;

namespace TranSQL.server
{
    public class TranSQLDbContext : DbContext
    {
        public TranSQLDbContext(DbContextOptions<TranSQLDbContext> options) : base(options)
        {
        }

        public DbSet<Accesorio> Accesorios { get; set; }
        public DbSet<Asignacion> Asignaciones { get; set; }
        public DbSet<Colaborador> Colaboradores { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<EstadoSolicitud> EstadosSolicitudes { get; set; }
        public DbSet<EstadoVehiculo> EstadosVehiculo { get; set; }
        public DbSet<InspeccionAccesorio> InspeccionesAccesorios { get; set; }
        public DbSet<InspeccionVehiculo> InspeccionVehiculos { get; set; }
        public DbSet<Reservacion> Reservaciones { get; set; }
        public DbSet<SolicitudReservacion> SolicitudesReservacion { get; set; }
        public DbSet<TipoInspeccion> TipoInspecciones { get; set; }
        public DbSet<TipoVehiculo> TipoVehiculos { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Especificar los nombres de tablas y esquemas, si es necesario
            modelBuilder.Entity<Colaborador>().ToTable("Colaborador");
            modelBuilder.Entity<Departamento>().ToTable("Departamento");
            modelBuilder.Entity<Accesorio>().ToTable("Accesorio");
            modelBuilder.Entity<Asignacion>().ToTable("Asignacion");
            modelBuilder.Entity<EstadoSolicitud>().ToTable("EstadoSolicitud");
            modelBuilder.Entity<EstadoVehiculo>().ToTable("EstadoVehiculo");
            modelBuilder.Entity<InspeccionAccesorio>().ToTable("InspeccionAccesorio");
            modelBuilder.Entity<InspeccionVehiculo>().ToTable("InspeccionVehiculo");
            modelBuilder.Entity<Reservacion>().ToTable("Reservacion");
            modelBuilder.Entity<SolicitudReservacion>().ToTable("SolicitudesReservacion");
            modelBuilder.Entity<TipoInspeccion>().ToTable("TipoInspeccion");
            modelBuilder.Entity<TipoVehiculo>().ToTable("TipoVehiculo");
            modelBuilder.Entity<Vehiculo>().ToTable("Vehiculo");

            // Relaciones y configuraciones adicionales
            modelBuilder.Entity<Vehiculo>()
                .HasOne(v => v.TipoVehiculo)
                .WithMany(t => t.Vehiculos)
                .HasForeignKey(v => v.IdTipoVehiculo);

            modelBuilder.Entity<Vehiculo>()
                .HasOne(v => v.EstadoVehiculo)
                .WithMany(e => e.Vehiculos)
                .HasForeignKey(v => v.IdEstadoVehiculo);

            //// Configuración de SolicitudReservacion
            //modelBuilder.Entity<SolicitudReservacion>()
            //    .HasOne(s => s.Colaborador)
            //    .WithMany()
            //    .HasForeignKey(s => s.IdColaborador)
            //    .OnDelete(DeleteBehavior.Restrict);  // Evita eliminaciones en cascada

            modelBuilder.Entity<SolicitudReservacion>()
                .Property(s => s.Motivo)
                .IsRequired(false);  // Permite nulos para este campo

            //modelBuilder.Entity<SolicitudReservacion>()
            //    .HasOne(s => s.EstadoSolicitud)
            //    .WithMany()
            //    .HasForeignKey(s => s.IdEstadoSolicitud)
            //    .OnDelete(DeleteBehavior.Restrict);  // Evita eliminaciones en cascada

            modelBuilder.Entity<SolicitudReservacion>()
                .HasOne(s => s.Colaborador)
                .WithMany()
                .HasForeignKey(s => s.IdColaborador)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SolicitudReservacion>()
                .HasOne(s => s.EstadoSolicitud)
                .WithMany()
                .HasForeignKey(s => s.IdEstadoSolicitud)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Asignacion>()
                .HasOne(a => a.Vehiculo)
                .WithMany(v => v.Asignaciones)
                .HasForeignKey(a => a.Placa);

            modelBuilder.Entity<Asignacion>()
                .HasOne(a => a.SolicitudReservacion)
                .WithMany(s => s.Asignaciones)
                .HasForeignKey(a => a.IdSolicitud);

            // Llama a la configuración base
            base.OnModelCreating(modelBuilder);
        }
    }
}
