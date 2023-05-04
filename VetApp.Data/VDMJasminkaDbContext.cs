using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Npgsql.EntityFrameworkCore.PostgreSQL.Migrations.Internal;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDMJasminka.Core;
using VDMJasminka.Core.Ambulance.OwnerAggregate;
using VDMJasminka.Core.Ambulance.ValueObjects;
using VDMJasminka.Core.Appointments.ScheduleAggregate;
using VDMJasminka.Core.Common;
using VDMJasminka.Core.Entities;
using VDMJasminka.Core.Inventory.MedicamentAggregate;
using VDMJasminka.Core.Inventory.SupplierAggregate;
using VDMJasminka.Core.Users;
using VDMJasminka.Data.EntityConfigurations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace VDMJasminka.Data
{
    public partial class VDMJasminkaDbContext : DbContext
    {
        private readonly IMediator _mediator;

        public VDMJasminkaDbContext()
        {
        }

        public VDMJasminkaDbContext(DbContextOptions<VDMJasminkaDbContext> options, IMediator mediator)
            : base(options)
        {
            _mediator = mediator;
        }

        private IDbContextTransaction _transaction;

        public void BeginTransaction()
        {
            _transaction = Database.BeginTransaction();
        }

        public async Task CommitAsync()
        {
            try
            {
                await SaveChangesAsync();
                await _transaction.CommitAsync();
            }
            finally
            {
                var domainEventEntities = ChangeTracker.Entries<Entity>()
                                     .Select(po => po.Entity)
                                     .Where(po => po.DomainEvents.Any())
                                     .SelectMany(po => po.DomainEvents)
                                     .ToArray();

                foreach (var item in domainEventEntities)
                {
                    await _mediator.Publish(item);
                }
                await _transaction.DisposeAsync();
            }
        }

        public void Rollback()
        {
            _transaction.Rollback();
            _transaction.Dispose();
        }

        public virtual DbSet<Supplier> Suppliers { get; set; }

        public virtual DbSet<Medicament> Medicaments { get; set; }
        public virtual DbSet<Pet> Pets { get; set; }
        public virtual DbSet<Checkup> Checkups { get; set; }
        public virtual DbSet<CheckupItem> CheckupDetails { get; set; }
        public virtual DbSet<SupplierOrder> Resupplies { get; set; }
        public virtual DbSet<SupplierOrderItem> ResupplyDetails { get; set; }
        public virtual DbSet<Breed> Breeds { get; set; }
        public virtual DbSet<Owner> Owners { get; set; }

        //public virtual DbSet<Schedule> Schedules { get; set; }
        //public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<Species> AnimalTypes { get; set; }

        public virtual DbSet<MedicamentInventory> Inventory { get; set; }

        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
        });

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
            modelBuilder.ApplyConfiguration(new CheckupEntityConfiguration());
            modelBuilder.ApplyConfiguration(new OwnerEntityConfiguration());
            modelBuilder.ApplyConfiguration(new PetEntityConfiguration());
            modelBuilder.ApplyConfiguration(new SupplierEntityConfiguration());
            modelBuilder.ApplyConfiguration(new DiagnosisEntityConfiguration());
            modelBuilder.ApplyConfiguration(new MedicamentEntityConfiguration());
            modelBuilder.ApplyConfiguration(new SpeciesEntityConfiguration());
            modelBuilder.ApplyConfiguration(new BreedEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CheckupItemEntityConfiguration());
            modelBuilder.ApplyConfiguration(new SupplierEntityConfiguration());
            modelBuilder.ApplyConfiguration(new InventoryEntryEntityConfiguration());
            modelBuilder.ApplyConfiguration(new InventoryItemEntityConfiguration());
            modelBuilder.ApplyConfiguration(new MedicamentInventoryEntityConfiguration());
            modelBuilder.ApplyConfiguration(new SupplierOrderEntityConfiguration());
            modelBuilder.ApplyConfiguration(new SupplierOrderItemEntityConfiguration());
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}