using System.Data.Entity;

namespace UnityCareHospital.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("DefaultConnection") { }

        public DbSet<User> Users { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Contact> Contacts { get; set; }

       
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>()
                .HasRequired(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserID)
                .WillCascadeOnDelete(false); 

            modelBuilder.Entity<Doctor>()
                .HasRequired(d => d.User)
                .WithMany()
                .HasForeignKey(d => d.UserID)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder); 
        }
    }
}
