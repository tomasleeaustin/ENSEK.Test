using ENSEK.Data.Access.DbContexts.Interfaces;
using ENSEK.Data.Access.Entities;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ENSEK.Data.Access.DbContexts
{
    public partial class EnsekDbContext : DbContext, IEnsekDbContext
    {
        private string _connectionString;

        public EnsekDbContext()
        {
        }

        public EnsekDbContext(DbContextOptions<EnsekDbContext> options)
            : base(options)
        {
        }

        public EnsekDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<MeterReading> MeterReadings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(_connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "English_United Kingdom.1252");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("account", "customers");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('customers.user_account_account_id_seq'::regclass)");

                entity.Property(e => e.FirstName)
                    .HasColumnType("character varying")
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .HasColumnType("character varying")
                    .HasColumnName("last_name");
            });

            modelBuilder.Entity<MeterReading>(entity =>
            {
                entity.ToTable("meter_reading", "customers");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.DateTime).HasColumnName("date_time");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('customers.meter_reading_id_seq'::regclass)");

                entity.Property(e => e.Value)
                    .HasPrecision(5)
                    .HasColumnName("value");

                entity.HasOne(d => d.Account)
                    .WithMany()
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("meter_reading_fk");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
