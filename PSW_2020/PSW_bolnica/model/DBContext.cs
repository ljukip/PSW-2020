using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PSW_bolnica.model;

namespace PSW_bolnica
{
    public partial class DBContext : DbContext
    {
        public DBContext()
        {
        }

        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> user { get; set; }

       /* protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning     optionsBuilder.UseSqlServer("Data Source=desktop-jiqcmc3\\sqlexpress;initial catalog=PSW_dataBase;Integrated Security=True;ConnectRetryCount=0");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("users");

                entity.Property(e => e.address)
                    .HasColumnName("address")
                    .HasMaxLength(46)
                    .IsFixedLength();

                entity.Property(e => e.gender)
                    .HasColumnName("gender")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.id)
                    .HasColumnName("id")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.name)
                    .HasColumnName("name")
                    .HasMaxLength(46)
                    .IsFixedLength();

                entity.Property(e => e.password)
                    .HasColumnName("password")
                    .HasMaxLength(26)
                    .IsFixedLength();

                entity.Property(e => e.phoneNumber)
                    .HasColumnName("phoneNumber")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.role)
                    .HasColumnName("role")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.surname)
                    .HasColumnName("surname")
                    .HasMaxLength(46)
                    .IsFixedLength();

                entity.Property(e => e.username)
                    .HasColumnName("username")
                    .IsRequired(true)
                    .HasMaxLength(26)
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder); */
    }
}
