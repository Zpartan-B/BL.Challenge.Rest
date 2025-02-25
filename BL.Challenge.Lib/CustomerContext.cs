using BL.Challenge.Lib.Schema;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Challenge.Lib
{
    public class CustomerContext : DbContext
    {
        private readonly string _dbPath = "test.db";
        private readonly string _createTableScript = @"
        CREATE TABLE IF NOT EXISTS Customer (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            FirstName TEXT NOT NULL,
            MiddleName TEXT,
            LastName TEXT NOT NULL,
            EmailAddress TEXT UNIQUE NOT NULL,
            Phone TEXT
        )";

        public CustomerContext()
        {
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());

            // Create the database file if it doesn't exist
            if (!File.Exists(_dbPath))
            {
                using var connection = new SqliteConnection($"Data Source={_dbPath}");
                connection.Open();

                using var command = connection.CreateCommand();
                command.CommandText = _createTableScript;
                command.ExecuteNonQuery();
            }
        }

        public CustomerContext(DbContextOptions<CustomerContext> options)
        : base(options)
        {
            InitializeDatabase();
        }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={_dbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Customer>()
                .HasIndex(x => x.EmailAddress)
                .IsUnique();
        }
    }
}
