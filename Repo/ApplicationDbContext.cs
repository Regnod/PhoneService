using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.IO;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Repo
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext> 
    { 
        public ApplicationDbContext CreateDbContext(string[] args) 
        { 
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory() + "/../MVCPhoneServiceWeb/appsettings.json").Build(); 
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>(); 
            var connectionString = configuration.GetConnectionString("DefaultConnection"); 
            builder.UseSqlServer(connectionString); 
            return new ApplicationDbContext(builder.Options); 
        } 
    }
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<MobilePhone> MobilePhones { get; set; }
        public DbSet<PhoneLine> PhoneLines { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<CallingPlan> CallingPlans { get; set; }
        public DbSet<DataPlan> DataPlans { get; set; }
        public DbSet<MobilePhoneEmployee> MobilePhoneEmployees { get; set; }
        public DbSet<PhoneLineEmployee> PhoneLineEmployees { get; set; }
        public DbSet<MobilePhoneCall> MobilePhoneCalls { get; set; }
        public DbSet<MobilePhoneDataPlanAssignment> MobilePhoneDataPlanAssignments { get; set; }
        public DbSet<MobilePhoneCallingPlanAssignment> MobilePhoneCallingPlanAssignments { get; set; }
        public DbSet<LandlinePhoneCall> LandLinePhoneCalls { get; set; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
             base.OnModelCreating(builder);
            builder.Entity<MobilePhoneEmployee>()
                .HasKey(a => a.IMEI);
            builder.Entity<MobilePhoneEmployee>()
                .HasOne(a => a.MobilePhone)
                .WithMany(a => a.MobilePhoneEmployee)
                .HasForeignKey(b => b.IMEI);
            builder.Entity<MobilePhoneEmployee>()
            .HasOne(a => a.Employee)
            .WithMany(a => a.MobilePhoneEmployees)
            .HasForeignKey(a => a.EmployeeId);
        
            builder.Entity<PhoneLineEmployee>()
                .HasKey(a => a.PhoneNumber);
            builder.Entity<PhoneLineEmployee>()
                .HasOne(a => a.PhoneLine)
                .WithMany(a => a.PhoneLineEmployees)
                .HasForeignKey(a => a.PhoneNumber);
            builder.Entity<PhoneLineEmployee>()
                .HasOne(a => a.Employee)
                .WithMany(a => a.PhoneLineEmployees)
                .HasForeignKey(a => a.EmployeeId);
        
            builder.Entity<MobilePhoneCall>()
                .HasKey(a => new {a.PhoneNumber, a.IMEI, a.DateTime});
            builder.Entity<MobilePhoneCall>()
                .HasOne(a => a.PhoneLine)
                .WithMany(a => a.MobilePhoneCalls)
                .HasForeignKey(a => a.PhoneNumber);
            builder.Entity<MobilePhoneCall>()
                .HasOne(a => a.MobilePhone)
                .WithMany(a => a.MobilePhoneCalls)
                .HasForeignKey(a => a.IMEI);
        
            builder.Entity<MobilePhoneDataPlanAssignment>()
                .HasKey(a => new {a.PhoneNumber, a.DataPlanAssignmentDateTime, a.DataPlanId});
            builder.Entity<MobilePhoneDataPlanAssignment>()
                .HasOne(a => a.PhoneLine)
                .WithMany(a => a.MobilePhoneDataPlanAssignments)
                .HasForeignKey(a => a.PhoneNumber);
            builder.Entity<MobilePhoneDataPlanAssignment>()
                .HasOne(a => a.DataPlan)
                .WithMany(a => a.MobilePhoneDataPlanAssignments)
                .HasForeignKey(a => a.DataPlanId);

            builder.Entity<MobilePhoneCallingPlanAssignment>()
                .HasKey(a => new {a.PhoneNumber, a.CallingPlanAssignmentDateTime, a.CallingPlanId});
            builder.Entity<MobilePhoneCallingPlanAssignment>()
                .HasOne(a => a.PhoneLine)
                .WithMany(a => a.MobilePhoneCallingPlanAssignments)
                .HasForeignKey(a => a.PhoneNumber);
            builder.Entity<MobilePhoneCallingPlanAssignment>()
                .HasOne(a => a.CallingPlan)
                .WithMany(a => a.MobilePhoneCallingPlanAssignments)
                .HasForeignKey(a => a.CallingPlanId);

            builder.Entity<LandlinePhoneCall>()
                .HasKey(a => new {a.Extension, a.LandlinePhoneCallDateTime, a.EmployeeId});
            builder.Entity<LandlinePhoneCall>()
                .HasOne(a => a.Employee)
                .WithMany(a => a.LandlinePhoneCalls)
                .HasForeignKey(a => a.EmployeeId);
            
            
        }
    }
}