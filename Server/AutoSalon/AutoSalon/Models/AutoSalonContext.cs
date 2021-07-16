using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace AutoSalon.Models
{
    public partial class AutoSalonContext : DbContext
    {
        public AutoSalonContext()
        {
        }

        public AutoSalonContext(DbContextOptions<AutoSalonContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Korisnik> Korisniks { get; set; }
        public virtual DbSet<Kupac> Kupacs { get; set; }
        public virtual DbSet<Model> Models { get; set; }
        public virtual DbSet<Proizvodjac> Proizvodjacs { get; set; }
        public virtual DbSet<Slike> Slikes { get; set; }
        public virtual DbSet<Vozilo> Vozilos { get; set; }
        public virtual DbSet<VrstaGoriva> VrstaGorivas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=AutoSalonDB");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Korisnik>(entity =>
            {
                entity.HasKey(e => e.Korisnickoime);

                entity.ToTable("KORISNIK");

                entity.Property(e => e.Korisnickoime)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("KORISNICKOIME");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Lozinka)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("LOZINKA");
            });

            modelBuilder.Entity<Kupac>(entity =>
            {
                entity.ToTable("KUPAC");

                entity.Property(e => e.Kupacid)
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("KUPACID");

                entity.Property(e => e.Adresa)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("ADRESA");

                entity.Property(e => e.Ime)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("IME");

                entity.Property(e => e.Prezime)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("PREZIME");

                entity.Property(e => e.Telefon)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("TELEFON");
            });

            modelBuilder.Entity<Model>(entity =>
            {
                entity.HasKey(e => new { e.Proizvodjacid, e.Modelid });

                entity.ToTable("MODEL");

                entity.HasIndex(e => e.Proizvodjacid, "PROIZVODJAC_MODELA_FK");

                entity.Property(e => e.Proizvodjacid)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("PROIZVODJACID");

                entity.Property(e => e.Modelid)
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("MODELID");

                entity.Property(e => e.Nazivmodela)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("NAZIVMODELA");

                entity.HasOne(d => d.Proizvodjac)
                    .WithMany(p => p.Models)
                    .HasForeignKey(d => d.Proizvodjacid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MODEL_PROIZVODJ_PROIZVOD");
            });

            modelBuilder.Entity<Proizvodjac>(entity =>
            {
                entity.ToTable("PROIZVODJAC");

                entity.Property(e => e.Proizvodjacid)
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("PROIZVODJACID");

                entity.Property(e => e.Nazivproizvodjaca)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("NAZIVPROIZVODJACA");
            });

            modelBuilder.Entity<Slike>(entity =>
            {
                entity.ToTable("SLIKE");

                entity.HasIndex(e => e.Voziloid, "SLIKE_VOZILA_FK");

                entity.Property(e => e.Slikeid)
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("SLIKEID");

                entity.Property(e => e.Nazivslike)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NAZIVSLIKE");

                entity.Property(e => e.Voziloid)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("VOZILOID");

                entity.HasOne(d => d.Vozilo)
                    .WithMany(p => p.Slikes)
                    .HasForeignKey(d => d.Voziloid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SLIKE_SLIKE_VOZ_VOZILO");
            });

            modelBuilder.Entity<Vozilo>(entity =>
            {
                entity.ToTable("VOZILO");

                entity.HasIndex(e => e.Kupacid, "KUPAC_VOZILA_FK");

                entity.HasIndex(e => new { e.Proizvodjacid, e.Modelid }, "MODEL_VOZILA_FK");

                entity.HasIndex(e => e.Vrstagorivaid, "VRSTA_GORIVA_VOZILA_FK");

                entity.Property(e => e.Voziloid)
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("VOZILOID");

                entity.Property(e => e.Cena)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("CENA");

                entity.Property(e => e.Datumdolaska)
                    .HasColumnType("datetime")
                    .HasColumnName("DATUMDOLASKA");

                entity.Property(e => e.Datumprodaje)
                    .HasColumnType("datetime")
                    .HasColumnName("DATUMPRODAJE");

                entity.Property(e => e.Kilometraza)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("KILOMETRAZA");

                entity.Property(e => e.Kupacid)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("KUPACID");

                entity.Property(e => e.Modelid)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("MODELID");

                entity.Property(e => e.Opis)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("OPIS");

                entity.Property(e => e.Proizvodjacid)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("PROIZVODJACID");

                entity.Property(e => e.Snagamotora).HasColumnName("SNAGAMOTORA");

                entity.Property(e => e.Vrstagorivaid)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("VRSTAGORIVAID");

                entity.HasOne(d => d.Kupac)
                    .WithMany(p => p.Vozilos)
                    .HasForeignKey(d => d.Kupacid)
                    .HasConstraintName("FK_VOZILO_KUPAC_VOZ_KUPAC");

                entity.HasOne(d => d.Vrstagoriva)
                    .WithMany(p => p.Vozilos)
                    .HasForeignKey(d => d.Vrstagorivaid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VOZILO_VRSTA_GOR_VRSTA_GO");

                entity.HasOne(d => d.Model)
                    .WithMany(p => p.Vozilos)
                    .HasForeignKey(d => new { d.Proizvodjacid, d.Modelid })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VOZILO_MODEL_VOZ_MODEL");
            });

            modelBuilder.Entity<VrstaGoriva>(entity =>
            {
                entity.ToTable("VRSTA_GORIVA");

                entity.Property(e => e.Vrstagorivaid)
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("VRSTAGORIVAID");

                entity.Property(e => e.Nazivvrstegoriva)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("NAZIVVRSTEGORIVA");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
