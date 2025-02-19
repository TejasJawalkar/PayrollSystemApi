﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PayrollSystem.Data.Common;

#nullable disable

namespace PayrollSystem.Migrations
{
    [DbContext(typeof(DbsContext))]
    [Migration("20241226131735_SecondMigration")]
    partial class SecondMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PayrollSystem.Entity.Models.Employee.DailyTimeSheet", b =>
                {
                    b.Property<long>("TimeSheetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("TimeSheetId"), 1L, 1);

                    b.Property<string>("AttendanceFlag")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("EmployeeId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("LogOutTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("LoginLocation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LoginTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("LogoutLocation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TodayDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("TotalHoursWorked")
                        .HasColumnType("int");

                    b.HasKey("TimeSheetId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("DailyTimeSheet");
                });

            modelBuilder.Entity("PayrollSystem.Entity.Models.Employee.Department", b =>
                {
                    b.Property<long>("DepartmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("DepartmentId"), 1L, 1);

                    b.Property<string>("DepartementName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("DepartmentId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("PayrollSystem.Entity.Models.Employee.Designation", b =>
                {
                    b.Property<long>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("RoleId"), 1L, 1);

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<long>("Stamp")
                        .HasMaxLength(10)
                        .HasColumnType("bigint");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("PayrollSystem.Entity.Models.Employee.Employee", b =>
                {
                    b.Property<long>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("EmployeeId"), 1L, 1);

                    b.Property<long>("DepartmentId")
                        .HasColumnType("bigint");

                    b.Property<long>("OrganizationId")
                        .HasColumnType("bigint");

                    b.Property<long>("PaymentID")
                        .HasColumnType("bigint");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.HasKey("EmployeeId");

                    b.HasIndex("DepartmentId")
                        .IsUnique();

                    b.HasIndex("OrganizationId");

                    b.HasIndex("PaymentID")
                        .IsUnique();

                    b.HasIndex("RoleId")
                        .IsUnique();

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("PayrollSystem.Entity.Models.Employee.EmployeeDetails", b =>
                {
                    b.Property<long>("EmployeeDetails_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("EmployeeDetails_Id"), 1L, 1);

                    b.Property<long>("EmployeeId")
                        .HasColumnType("bigint");

                    b.Property<string>("EmployeeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPasswordChangeActive")
                        .HasColumnType("bit");

                    b.Property<string>("Mobile")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("MobileNoCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrganisationEmail")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PersonalEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmployeeDetails_Id");

                    b.HasIndex("EmployeeId")
                        .IsUnique();

                    b.ToTable("EmployeeDetails");
                });

            modelBuilder.Entity("PayrollSystem.Entity.Models.Employee.EmployeeManagers", b =>
                {
                    b.Property<long>("EmployeeId")
                        .HasColumnType("bigint");

                    b.Property<long>("ManagerId")
                        .HasColumnType("bigint");

                    b.HasKey("EmployeeId", "ManagerId");

                    b.HasIndex("ManagerId");

                    b.ToTable("EmployeeManagers");
                });

            modelBuilder.Entity("PayrollSystem.Entity.Models.Employee.EmployeeSecurity", b =>
                {
                    b.Property<long>("UserSecurityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("UserSecurityId"), 1L, 1);

                    b.Property<long>("EmployeeId")
                        .HasColumnType("bigint");

                    b.Property<byte[]>("UserPassword")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.HasKey("UserSecurityId");

                    b.HasIndex("EmployeeId")
                        .IsUnique();

                    b.ToTable("EmployeeSecurity");
                });

            modelBuilder.Entity("PayrollSystem.Entity.Models.Employee.Orgnisations", b =>
                {
                    b.Property<long>("OrgnisationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("OrgnisationID"), 1L, 1);

                    b.Property<string>("CeoEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CeoMobileNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DirectorEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DirectorMobileNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("OrgnisationAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrgnisationCeo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrgnisationCountry")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrgnisationDirectorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrgnisationEndTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrgnisationGstNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrgnisationPincode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OrgnisationStartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("OrgnisationStartTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrgnisationState")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrgnizationName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SystemRegisteredDate")
                        .HasColumnType("datetime2");

                    b.HasKey("OrgnisationID");

                    b.ToTable("Oragnizations");
                });

            modelBuilder.Entity("PayrollSystem.Entity.Models.Employee.PaymentData", b =>
                {
                    b.Property<long>("PaymentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("PaymentID"), 1L, 1);

                    b.Property<double>("CTC")
                        .HasColumnType("float");

                    b.Property<double>("GrossPay")
                        .HasColumnType("float");

                    b.Property<double>("NetPay")
                        .HasColumnType("float");

                    b.HasKey("PaymentID");

                    b.ToTable("PaymentDetails");
                });

            modelBuilder.Entity("PayrollSystem.Entity.Models.Employee.ReportingManagers", b =>
                {
                    b.Property<long>("ManagerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ManagerID"), 1L, 1);

                    b.Property<long>("EmployeeId")
                        .HasColumnType("bigint");

                    b.HasKey("ManagerID");

                    b.HasIndex("EmployeeId")
                        .IsUnique();

                    b.ToTable("Managers");
                });

            modelBuilder.Entity("PayrollSystem.Entity.Models.Employee.UserLeave", b =>
                {
                    b.Property<long>("LeaveId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("LeaveId"), 1L, 1);

                    b.Property<DateTime>("AppliedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("AppliedReason")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("ApprovedBy")
                        .HasColumnType("int");

                    b.Property<long>("EmployeeId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("NoofDays")
                        .HasColumnType("float");

                    b.Property<string>("RejectedReason")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("Status")
                        .HasMaxLength(10)
                        .HasColumnType("int");

                    b.Property<DateTime>("ToDate")
                        .HasColumnType("datetime2");

                    b.HasKey("LeaveId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("UserLeaves");
                });

            modelBuilder.Entity("PayrollSystem.Entity.Models.Logging.ExceptionLog", b =>
                {
                    b.Property<long>("ExceptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ExceptionId"), 1L, 1);

                    b.Property<string>("ActionName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("ClassName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("ExceptionMessage")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("SiteName")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("ExceptionId");

                    b.ToTable("ExceptionLogs");
                });

            modelBuilder.Entity("PayrollSystem.Entity.Models.Logging.UserLogs", b =>
                {
                    b.Property<long>("LogID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("LogID"), 1L, 1);

                    b.Property<string>("BrowswerUsed")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("IdAddress")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("LogID");

                    b.ToTable("UserLogs");
                });

            modelBuilder.Entity("PayrollSystem.Entity.Models.Employee.DailyTimeSheet", b =>
                {
                    b.HasOne("PayrollSystem.Entity.Models.Employee.Employee", "Employee")
                        .WithMany("DailyTimeSheets")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("PayrollSystem.Entity.Models.Employee.Employee", b =>
                {
                    b.HasOne("PayrollSystem.Entity.Models.Employee.Department", "Department")
                        .WithOne("Employee")
                        .HasForeignKey("PayrollSystem.Entity.Models.Employee.Employee", "DepartmentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PayrollSystem.Entity.Models.Employee.Orgnisations", "Orgnisations")
                        .WithMany("Employees")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PayrollSystem.Entity.Models.Employee.PaymentData", "PaymentData")
                        .WithOne("Employee")
                        .HasForeignKey("PayrollSystem.Entity.Models.Employee.Employee", "PaymentID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PayrollSystem.Entity.Models.Employee.Designation", "Designation")
                        .WithOne("Employee")
                        .HasForeignKey("PayrollSystem.Entity.Models.Employee.Employee", "RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Designation");

                    b.Navigation("Orgnisations");

                    b.Navigation("PaymentData");
                });

            modelBuilder.Entity("PayrollSystem.Entity.Models.Employee.EmployeeDetails", b =>
                {
                    b.HasOne("PayrollSystem.Entity.Models.Employee.Employee", "Employee")
                        .WithOne("EmployeeDetails")
                        .HasForeignKey("PayrollSystem.Entity.Models.Employee.EmployeeDetails", "EmployeeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("PayrollSystem.Entity.Models.Employee.EmployeeManagers", b =>
                {
                    b.HasOne("PayrollSystem.Entity.Models.Employee.Employee", "Employee")
                        .WithMany("EmployeeManagers")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PayrollSystem.Entity.Models.Employee.ReportingManagers", "ReportingManagers")
                        .WithMany("EmployeeManagers")
                        .HasForeignKey("ManagerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("ReportingManagers");
                });

            modelBuilder.Entity("PayrollSystem.Entity.Models.Employee.EmployeeSecurity", b =>
                {
                    b.HasOne("PayrollSystem.Entity.Models.Employee.Employee", "Employee")
                        .WithOne("EmployeeSecurity")
                        .HasForeignKey("PayrollSystem.Entity.Models.Employee.EmployeeSecurity", "EmployeeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("PayrollSystem.Entity.Models.Employee.ReportingManagers", b =>
                {
                    b.HasOne("PayrollSystem.Entity.Models.Employee.Employee", "Employee")
                        .WithOne("ReportingManagers")
                        .HasForeignKey("PayrollSystem.Entity.Models.Employee.ReportingManagers", "EmployeeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("PayrollSystem.Entity.Models.Employee.UserLeave", b =>
                {
                    b.HasOne("PayrollSystem.Entity.Models.Employee.Employee", "Employee")
                        .WithMany("UserLeave")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("PayrollSystem.Entity.Models.Employee.Department", b =>
                {
                    b.Navigation("Employee")
                        .IsRequired();
                });

            modelBuilder.Entity("PayrollSystem.Entity.Models.Employee.Designation", b =>
                {
                    b.Navigation("Employee")
                        .IsRequired();
                });

            modelBuilder.Entity("PayrollSystem.Entity.Models.Employee.Employee", b =>
                {
                    b.Navigation("DailyTimeSheets");

                    b.Navigation("EmployeeDetails")
                        .IsRequired();

                    b.Navigation("EmployeeManagers");

                    b.Navigation("EmployeeSecurity")
                        .IsRequired();

                    b.Navigation("ReportingManagers")
                        .IsRequired();

                    b.Navigation("UserLeave");
                });

            modelBuilder.Entity("PayrollSystem.Entity.Models.Employee.Orgnisations", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("PayrollSystem.Entity.Models.Employee.PaymentData", b =>
                {
                    b.Navigation("Employee")
                        .IsRequired();
                });

            modelBuilder.Entity("PayrollSystem.Entity.Models.Employee.ReportingManagers", b =>
                {
                    b.Navigation("EmployeeManagers");
                });
#pragma warning restore 612, 618
        }
    }
}
