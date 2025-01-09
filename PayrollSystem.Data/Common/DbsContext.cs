using Microsoft.EntityFrameworkCore;
using PayrollSystem.Entity.Models.Employee;
using PayrollSystem.Entity.Models.Logging;


namespace PayrollSystem.Data.Common
{
    public class DbsContext : DbContext
    {
        #region Constructor
        public DbsContext(DbContextOptions<DbsContext> options) : base(options) { }
        #endregion

        #region All DbSet
        public DbSet<Employee> Employee { get; set; }
        public DbSet<PaymentData> PaymentDetails { get; set; }
        public DbSet<Orgnisations> Oragnizations { get; set; }
        public DbSet<ExceptionLog> ExceptionLogs { get; set; }
        public DbSet<UserLogs> UserLogs { get; set; }
        public DbSet<DailyTimeSheet> DailyTimeSheet { get; set; }
        public DbSet<UserLeave> EmployeeLeaves { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Designation> Roles { get; set; }
        public DbSet<ReportingManagers> Managers { get; set; }
        public DbSet<EmployeeManagers> EmployeeManagers { get; set; }
        public DbSet<EmployeeDetails> EmployeeDetails { get; set; }
        #endregion

        #region OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region One To One Relationship
            modelBuilder.Entity<Employee>()
                .HasOne(ed => ed.EmployeeDetails)
                .WithOne(e => e.Employee)
                .HasForeignKey<EmployeeDetails>(fk => fk.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employee>()
                .HasOne(ed => ed.EmployeeSecurity)
                .WithOne(e => e.Employee)
                .HasForeignKey<EmployeeSecurity>(fk => fk.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employee>()
               .HasOne(ed => ed.PaymentData)
               .WithOne(e => e.Employee)
               .HasForeignKey<Employee>(fk => fk.PaymentID)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.ReportingManagers)
                .WithOne(m => m.Employee)
                .HasForeignKey<ReportingManagers>(fk => fk.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region One To Many Relationship
            modelBuilder.Entity<Employee>()
               .HasOne(ed => ed.Department)
               .WithMany(e => e.Employee)
               .HasForeignKey(fk => fk.DepartmentId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employee>()
                .HasOne(ed => ed.Designation)
                .WithMany(e => e.Employee)
                .HasForeignKey(fk => fk.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employee>()
                .HasOne(o => o.Orgnisations)
                .WithMany(e => e.Employees)
                .HasForeignKey(fk => fk.OrganizationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DailyTimeSheet>()
                .HasOne(e => e.Employee)
                .WithMany(edt => edt.DailyTimeSheets)
                .HasForeignKey(fk => fk.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserLeave>()
                .HasOne(e => e.Employee)
                .WithMany(ul => ul.UserLeave)
                .HasForeignKey(fk => fk.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region Many To Many 
            modelBuilder.Entity<EmployeeManagers>().HasKey(ek => new { ek.EmployeeId, ek.ManagerId });

            modelBuilder.Entity<EmployeeManagers>()
                .HasOne(e => e.Employee)
                .WithMany(e => e.EmployeeManagers)
                .HasForeignKey(fk => fk.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EmployeeManagers>()
                .HasOne(e => e.ReportingManagers)
                .WithMany(e => e.EmployeeManagers)
                .HasForeignKey(fk => fk.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion
        }
        #endregion
    }
}
