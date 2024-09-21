using MCFBackend.Services.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace MCFBackend.Context
{
    public class ApplicationContext : DbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<tr_bpkb> tr_bpkb { get; set; }
        public DbSet<ms_storage_location> ms_storage_location { get; set; }
        public DbSet<ms_user> ms_user { get; set; }


        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
