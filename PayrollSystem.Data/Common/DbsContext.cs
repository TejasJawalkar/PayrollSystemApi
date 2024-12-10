using Microsoft.EntityFrameworkCore;
using PayrollSystem.Entity.Models.Employee;
using PayrollSystem.Entity.Models.Logging;


namespace PayrollSystem.Data.Common
{
    public class DbsContext : DbContext
    {
        public DbsContext(DbContextOptions<DbsContext> options) : base(options){}
        public DbSet<Employee> Employee { get; set; }
        public DbSet<PaymentData> Payments { get; set; }
        public DbSet<Orgnisations> Orgnisations { get; set; }
        public DbSet<ExceptionLog> ExceptionLogs { get; set; }
        public DbSet<UserLogs> UserLogs { get; set; }
        public DbSet<DailyTimeSheet> DailyTimeSheet { get; set; }
        public DbSet<UserLeave> UserLeaves { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
               .HasOne(e => e.orgnisations)
               .WithMany(o => o.employees)
               .HasForeignKey(e => e.OrgnisationID)
               .OnDelete(DeleteBehavior.Cascade);

            // Configure Employee to PaymentData relationship
            modelBuilder.Entity<PaymentData>()
                .HasOne(p => p.Employee)
                .WithMany(e => e.paymentDatas)
                .HasForeignKey(p => p.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Employee>().HasIndex(p => p.OrganisationEmail).IsUnique();

            modelBuilder.Entity<DailyTimeSheet>()
                .HasOne(p => p.employee)
                .WithMany(p => p.dailyTimeSheets)
                .HasForeignKey(p => p.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserLeave>()
                .HasOne(p => p.employees)
                .WithMany(p => p.userLeaves)
                .HasForeignKey(p => p.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Employee>()
              .HasOne(e => e.Departments)
              .WithMany(o => o.Employees)
              .HasForeignKey(e => e.DepartmentId)
              .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
