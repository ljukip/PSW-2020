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

        public DbSet<Doctor> doctors { get; set; }

        public DbSet<Appointment> appointments { get; set; }

        public DbSet<Referral> referral { get; set; }

        public DbSet<Feedback> feedbacks { get; set; }

        public DbSet<Perscription> perscriptions { get; set; }


    }
}
