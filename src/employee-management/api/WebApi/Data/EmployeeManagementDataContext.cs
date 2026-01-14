using Microsoft.EntityFrameworkCore;
using WebApi.Models.Entities;
using Task = WebApi.Models.Entities.Task;

namespace WebApi.Data
{
    public class EmployeeManagementDataContext : DbContext
    {
        public EmployeeManagementDataContext(DbContextOptions<EmployeeManagementDataContext> options) : base(options)
        {
        }

        public DbSet<Consultant> Consultants { get; set; }
        public DbSet<ConsultantTask> ConsultantTasks { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<RoleRate> RoleRates { get; set; }
    }
}
