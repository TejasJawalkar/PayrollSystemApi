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
        public DbSet<UserLeave> UserLeaves { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Designation> Roles { get; set; }
        public DbSet<ReportingManagers> Managers { get; set; }
        public DbSet<EmployeeManagers> EmployeeManagers { get; set; }
        #endregion

        #region OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region OneToOneRelations
            modelBuilder.Entity<Employee>()
                .HasOne(o => o.Orgnisations)
                .WithOne(e => e.Employee)
                .HasForeignKey<Orgnisations>(fk => fk.OrgnisationID);

            modelBuilder.Entity<Employee>()
                .HasOne(d => d.Department)
                .WithOne(e => e.Employee)
                .HasForeignKey<Department>(fk => fk.DepartmentId);

            modelBuilder.Entity<Designation>()
                .HasOne(e => e.Employee)
                .WithOne(d => d.Designation)
                .HasForeignKey<Employee>(fk => fk.EmployeeId);

            modelBuilder.Entity<EmployeeSecurity>()
              .HasOne(e => e.Employee)
              .WithOne(d => d.EmployeeSecurity)
              .HasForeignKey<Employee>(fk => fk.EmployeeId);

            modelBuilder.Entity<Employee>()
             .HasOne(e => e.ReportingManagers)
             .WithOne(d => d.Employee)
             .HasForeignKey<ReportingManagers>(fk => fk.EmployeeId);

            modelBuilder.Entity<Employee>()
             .HasOne(e => e.PaymentData)
             .WithOne(d => d.Employee)
             .HasForeignKey<PaymentData>(fk => fk.EmployeeId);

            modelBuilder.Entity<EmployeeManagers>()
            .HasOne(e => e.Employee)
            .WithOne(em => em.EmployeeManagers)
            .HasForeignKey<Employee>(e => e.EmployeeId);
            #endregion

            #region OneToManyRelations
            modelBuilder.Entity<UserLeave>()
               .HasOne(e => e.Employee)
               .WithMany(ul => ul.UserLeaves)
               .HasForeignKey(ul => ul.EmployeeId);

            modelBuilder.Entity<DailyTimeSheet>()
                .HasOne(e => e.Employee)
                .WithMany(dts => dts.DailyTimeSheets)
                .HasForeignKey(ds => ds.EmployeeId);
            #endregion

            #region ManyToManyRelations
            modelBuilder.Entity<EmployeeManagers>()
                .HasOne(m=>m.ReportingManagers)
                .WithMany(em=>em.EmployeeManagers)
                .HasForeignKey(e => e.ManagerId);
            #endregion

        }
        #endregion

    }
}
