﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using VDMJasminka.Data;

namespace VDMJasminka.Data.Migrations
{
    [DbContext(typeof(VDMJasminkaDbContext))]
    [Migration("20230206214041_Create_InventoryViewTable")]
    partial class Create_InventoryViewTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "3.1.23")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("VDMJasminka.Core.Ambulance.OwnerAggregate.Checkup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("counter")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("Advice")
                        .HasColumnName("medical_advice")
                        .HasColumnType("character varying(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Anamnesis")
                        .HasColumnName("anamnesis")
                        .HasColumnType("text");

                    b.Property<string>("CheckupType")
                        .IsRequired()
                        .HasColumnName("checkup_type")
                        .HasColumnType("character varying(20)")
                        .HasMaxLength(20);

                    b.Property<string>("ClinicalPicture")
                        .HasColumnName("clinical_picture")
                        .HasColumnType("text");

                    b.Property<DateTime?>("Date")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("date")
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("Date()");

                    b.Property<string>("Diagnosis")
                        .HasColumnName("diagnosis")
                        .HasColumnType("character varying(255)")
                        .HasMaxLength(255);

                    b.Property<string>("LabAnalysis")
                        .HasColumnName("lab_analisys")
                        .HasColumnType("text");

                    b.Property<DateTime?>("NextControlCheckup")
                        .HasColumnName("control_checkup_date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("NextVaccinationDate")
                        .HasColumnName("next_vaccination_date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<double?>("PaidSum")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("paid_sum")
                        .HasColumnType("double precision")
                        .HasDefaultValueSql("0");

                    b.Property<int>("PetId")
                        .HasColumnName("petid")
                        .HasColumnType("integer");

                    b.Property<string>("SurgicalIntervention")
                        .HasColumnName("surgical_intervention")
                        .HasColumnType("character varying(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Therapy")
                        .HasColumnName("therapy")
                        .HasColumnType("character varying(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Vaccine")
                        .HasColumnName("vaccine")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .HasName("PrimaryKey");

                    b.HasIndex("PetId");

                    b.ToTable("checkups");
                });

            modelBuilder.Entity("VDMJasminka.Core.Ambulance.OwnerAggregate.CheckupItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("counter")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<double?>("Amount")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("amount")
                        .HasColumnType("double precision")
                        .HasDefaultValueSql("0");

                    b.Property<int?>("CheckupId")
                        .HasColumnName("checkup_id")
                        .HasColumnType("integer");

                    b.Property<string>("MedicamentApplication")
                        .HasColumnName("application")
                        .HasColumnType("character varying(2)")
                        .HasMaxLength(2);

                    b.Property<int>("MedicamentId")
                        .HasColumnName("medicament_id")
                        .HasColumnType("integer");

                    b.Property<double?>("Price")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("price")
                        .HasColumnType("double precision")
                        .HasDefaultValueSql("0");

                    b.Property<string>("Uom")
                        .HasColumnName("uom")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id")
                        .HasName("checkup_details_pkey");

                    b.HasIndex("CheckupId")
                        .HasName("Visit DetailsVisit Number");

                    b.HasIndex("Id")
                        .IsUnique()
                        .HasName("checkup_details_id");

                    b.HasIndex("MedicamentId")
                        .HasName("medid");

                    b.ToTable("checkup_details");
                });

            modelBuilder.Entity("VDMJasminka.Core.Ambulance.OwnerAggregate.Owner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("counter")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<bool>("IsDeleted")
                        .HasColumnName("is_deleted")
                        .HasColumnType("boolean");

                    b.HasKey("Id")
                        .HasName("id");

                    b.HasIndex("Id")
                        .HasName("CustomerID");

                    b.ToTable("owners");
                });

            modelBuilder.Entity("VDMJasminka.Core.Ambulance.OwnerAggregate.Pet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("counter")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<int>("BreedId")
                        .HasColumnName("breed_id")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("LastVisit")
                        .HasColumnName("last_visit")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("OwnerId")
                        .HasColumnName("owner_id")
                        .HasColumnType("integer");

                    b.Property<int>("SpeciesId")
                        .HasColumnName("species_id")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BreedId");

                    b.HasIndex("Id")
                        .HasName("Id index");

                    b.HasIndex("OwnerId");

                    b.HasIndex("SpeciesId")
                        .HasName("speciesId");

                    b.HasIndex("SpeciesId", "BreedId")
                        .HasName("Index1");

                    b.ToTable("pets");
                });

            modelBuilder.Entity("VDMJasminka.Core.Ambulance.ValueObjects.Diagnosis", b =>
                {
                    b.Property<string>("DiagnosisName")
                        .HasColumnName("name")
                        .HasColumnType("text");

                    b.ToTable("diagnoses");
                });

            modelBuilder.Entity("VDMJasminka.Core.Entities.Breed", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("counter")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<int>("AnimalTypeId")
                        .HasColumnName("species_id")
                        .HasColumnType("counter");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id")
                        .HasName("breeds_pkey");

                    b.HasIndex("AnimalTypeId")
                        .HasName("species_idx");

                    b.HasIndex("Id")
                        .HasName("breed_id");

                    b.ToTable("breeds");
                });

            modelBuilder.Entity("VDMJasminka.Core.Inventory.MedicamentAggregate.InventoryAdjustment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<int?>("MedicamentId1")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("MedicamentId1");

                    b.ToTable("InventoryAdjustment");
                });

            modelBuilder.Entity("VDMJasminka.Core.Inventory.MedicamentAggregate.Medicament", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("counter")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .HasName("idx");

                    b.ToTable("medicaments");
                });

            modelBuilder.Entity("VDMJasminka.Core.Inventory.SupplierAggregate.SupplierOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("counter")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnName("date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("InvoiceNumber")
                        .HasColumnName("description")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<int>("SupplierId")
                        .HasColumnName("supplier_id")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Date")
                        .HasName("reception_date");

                    b.HasIndex("Id")
                        .HasName("reception_id");

                    b.HasIndex("SupplierId")
                        .HasName("supplier_id");

                    b.ToTable("supply_receptions");
                });

            modelBuilder.Entity("VDMJasminka.Core.Inventory.SupplierAggregate.SupplierOrderItemEntityConfiguration", b =>
                {
                    b.Property<int>("SupplierOrderId")
                        .HasColumnName("supply_reception_id")
                        .HasColumnType("integer");

                    b.Property<int>("MedicamentId")
                        .HasColumnName("medicament_id")
                        .HasColumnType("integer");

                    b.Property<string>("AdditionalInfo")
                        .HasColumnName("notes")
                        .HasColumnType("character varying(200)")
                        .HasMaxLength(200);

                    b.Property<double>("Amount")
                        .HasColumnName("amount")
                        .HasColumnType("double precision");

                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<double>("Price")
                        .HasColumnName("price")
                        .HasColumnType("double precision");

                    b.HasKey("SupplierOrderId", "MedicamentId")
                        .HasName("supply_reception_details_pkey");

                    b.HasIndex("SupplierOrderId")
                        .HasName("supply_reception_id");

                    b.HasIndex("MedicamentId")
                        .HasName("medicamentid");

                    b.ToTable("supply_reception_details");
                });

            modelBuilder.Entity("VDMJasminka.Core.Inventory.SupplierAggregate.Supplier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("suppliers_pkey");

                    b.ToTable("suppliers");
                });

            modelBuilder.Entity("VDMJasminka.Core.Species", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("counter")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("Type")
                        .HasColumnName("name")
                        .HasColumnType("character varying(25)")
                        .HasMaxLength(25);

                    b.HasKey("Id")
                        .HasName("species_pkey");

                    b.HasIndex("Id")
                        .HasName("VrstaID");

                    b.ToTable("species");
                });

            modelBuilder.Entity("VDMJasminka.Core.Ambulance.OwnerAggregate.Checkup", b =>
                {
                    b.HasOne("VDMJasminka.Core.Ambulance.OwnerAggregate.Pet", "Pet")
                        .WithMany("Checkups")
                        .HasForeignKey("PetId")
                        .HasConstraintName("PetsPregled")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VDMJasminka.Core.Ambulance.OwnerAggregate.CheckupItem", b =>
                {
                    b.HasOne("VDMJasminka.Core.Ambulance.OwnerAggregate.Checkup", "Checkup")
                        .WithMany("CheckupItems")
                        .HasForeignKey("CheckupId");

                    b.HasOne("VDMJasminka.Core.Inventory.MedicamentAggregate.Medicament", "Medicament")
                        .WithMany()
                        .HasForeignKey("MedicamentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VDMJasminka.Core.Ambulance.OwnerAggregate.Owner", b =>
                {
                    b.OwnsOne("VDMJasminka.Core.Ambulance.ValueObjects.OwnerInfo", "OwnerInformation", b1 =>
                        {
                            b1.Property<int>("OwnerId")
                                .HasColumnType("counter");

                            b1.Property<string>("AdditionalInfo")
                                .HasColumnName("notes")
                                .HasColumnType("text");

                            b1.Property<string>("Address")
                                .HasColumnName("street_address")
                                .HasColumnType("character varying(60)")
                                .HasMaxLength(60);

                            b1.Property<string>("Email")
                                .HasColumnName("email")
                                .HasColumnType("character varying(256)")
                                .HasMaxLength(256);

                            b1.Property<string>("FullName")
                                .IsRequired()
                                .HasColumnName("name")
                                .HasColumnType("character varying(40)")
                                .HasMaxLength(40);

                            b1.Property<string>("Location")
                                .HasColumnName("location")
                                .HasColumnType("text");

                            b1.Property<string>("Municipality")
                                .HasColumnName("municipality")
                                .HasColumnType("text");

                            b1.Property<string>("PhoneNumber")
                                .HasColumnName("phone_number")
                                .HasColumnType("character varying(24)")
                                .HasMaxLength(24);

                            b1.Property<string>("SSN")
                                .HasColumnName("ssn")
                                .HasColumnType("text");

                            b1.HasKey("OwnerId");

                            b1.HasIndex("FullName")
                                .HasName("full_name");

                            b1.ToTable("owners");

                            b1.WithOwner()
                                .HasForeignKey("OwnerId");
                        });
                });

            modelBuilder.Entity("VDMJasminka.Core.Ambulance.OwnerAggregate.Pet", b =>
                {
                    b.HasOne("VDMJasminka.Core.Entities.Breed", "Breed")
                        .WithMany()
                        .HasForeignKey("BreedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VDMJasminka.Core.Ambulance.OwnerAggregate.Owner", null)
                        .WithMany("Pets")
                        .HasForeignKey("OwnerId");

                    b.HasOne("VDMJasminka.Core.Species", "Species")
                        .WithMany("Pets")
                        .HasForeignKey("SpeciesId")
                        .HasConstraintName("VrsteZivotinjaPets")
                        .IsRequired();

                    b.OwnsOne("VDMJasminka.Core.Ambulance.ValueObjects.Chip", "Chip", b1 =>
                        {
                            b1.Property<int>("PetId")
                                .HasColumnType("counter");

                            b1.Property<string>("Number")
                                .HasColumnName("chip_number")
                                .HasColumnType("text");

                            b1.HasKey("PetId");

                            b1.ToTable("pets");

                            b1.WithOwner()
                                .HasForeignKey("PetId");
                        });

                    b.OwnsOne("VDMJasminka.Core.Ambulance.ValueObjects.PetInfo", "PetInformation", b1 =>
                        {
                            b1.Property<int>("PetId")
                                .HasColumnType("counter");

                            b1.Property<string>("Alergy")
                                .HasColumnName("alergy")
                                .HasColumnType("character varying(50)")
                                .HasMaxLength(50);

                            b1.Property<DateTime?>("DateOfBirth")
                                .HasColumnName("date_of_birth")
                                .HasColumnType("timestamp without time zone");

                            b1.Property<string>("InvoiceNumber")
                                .HasColumnName("description")
                                .HasColumnType("text");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnName("name")
                                .HasColumnType("character varying(20)")
                                .HasMaxLength(20);

                            b1.Property<string>("Sex")
                                .HasColumnName("sex")
                                .HasColumnType("character varying(1)")
                                .HasMaxLength(1);

                            b1.HasKey("PetId");

                            b1.ToTable("pets");

                            b1.WithOwner()
                                .HasForeignKey("PetId");
                        });
                });

            modelBuilder.Entity("VDMJasminka.Core.Entities.Breed", b =>
                {
                    b.HasOne("VDMJasminka.Core.Species", "Species")
                        .WithMany("Breeds")
                        .HasForeignKey("AnimalTypeId")
                        .HasConstraintName("VrsteZivotinjaRase")
                        .IsRequired();
                });

            modelBuilder.Entity("VDMJasminka.Core.Inventory.MedicamentAggregate.InventoryAdjustment", b =>
                {
                    b.HasOne("VDMJasminka.Core.Inventory.MedicamentAggregate.Medicament", null)
                        .WithMany("InventoryAdjustments")
                        .HasForeignKey("MedicamentId1");
                });

            modelBuilder.Entity("VDMJasminka.Core.Inventory.MedicamentAggregate.Medicament", b =>
                {
                    b.OwnsOne("VDMJasminka.Core.Inventory.ValueObjects.MedicamentDetails", "MedicamentDetails", b1 =>
                        {
                            b1.Property<int>("MedicamentId")
                                .HasColumnType("counter");

                            b1.Property<bool?>("IsMaterial")
                                .HasColumnName("material")
                                .HasColumnType("boolean");

                            b1.Property<string>("Name")
                                .HasColumnName("name")
                                .HasColumnType("text");

                            b1.Property<string>("Uom")
                                .HasColumnName("uom")
                                .HasColumnType("character varying(20)")
                                .HasMaxLength(20);

                            b1.HasKey("MedicamentId");

                            b1.HasIndex("Name")
                                .HasName("ProductName");

                            b1.ToTable("medicaments");

                            b1.WithOwner()
                                .HasForeignKey("MedicamentId");
                        });

                    b.OwnsOne("VDMJasminka.Core.Inventory.ValueObjects.MedicamentMinimalAmount", "MinimalAmount", b1 =>
                        {
                            b1.Property<int>("MedicamentId")
                                .HasColumnType("counter");

                            b1.Property<int>("Value")
                                .ValueGeneratedOnAdd()
                                .HasColumnName("minimal_amount")
                                .HasColumnType("integer")
                                .HasDefaultValueSql("0");

                            b1.HasKey("MedicamentId");

                            b1.ToTable("medicaments");

                            b1.WithOwner()
                                .HasForeignKey("MedicamentId");
                        });

                    b.OwnsOne("VDMJasminka.Core.Inventory.ValueObjects.MedicamentPrice", "Price", b1 =>
                        {
                            b1.Property<int>("MedicamentId")
                                .HasColumnType("counter");

                            b1.Property<double>("Value")
                                .HasColumnName("price")
                                .HasColumnType("double precision");

                            b1.HasKey("MedicamentId");

                            b1.ToTable("medicaments");

                            b1.WithOwner()
                                .HasForeignKey("MedicamentId");
                        });
                });

            modelBuilder.Entity("VDMJasminka.Core.Inventory.SupplierAggregate.SupplierOrder", b =>
                {
                    b.HasOne("VDMJasminka.Core.Inventory.SupplierAggregate.Supplier", null)
                        .WithMany("OrderRecievments")
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VDMJasminka.Core.Inventory.SupplierAggregate.SupplierOrderItemEntityConfiguration", b =>
                {
                    b.HasOne("VDMJasminka.Core.Inventory.SupplierAggregate.SupplierOrder", null)
                        .WithMany("Items")
                        .HasForeignKey("SupplierOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VDMJasminka.Core.Inventory.SupplierAggregate.Supplier", b =>
                {
                    b.OwnsOne("VDMJasminka.Core.Inventory.ValueObjects.SupplierAddress", "Address", b1 =>
                        {
                            b1.Property<int>("SupplierId")
                                .HasColumnType("integer");

                            b1.Property<string>("Address")
                                .HasColumnName("street_address")
                                .HasColumnType("text");

                            b1.Property<string>("Location")
                                .HasColumnName("location")
                                .HasColumnType("text");

                            b1.Property<string>("PhoneNumber")
                                .HasColumnName("phone_number")
                                .HasColumnType("text");

                            b1.HasKey("SupplierId");

                            b1.ToTable("suppliers");

                            b1.WithOwner()
                                .HasForeignKey("SupplierId");
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
