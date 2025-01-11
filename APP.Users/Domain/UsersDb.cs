﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace APP.Users.Domain
{
    public class UsersDb : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public UsersDb(DbContextOptions options) : base(options)
        {
        }
    }

    public class UsersDbFactory : IDesignTimeDbContextFactory<UsersDb>
    {
        public UsersDb CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<UsersDb>();
            optionsBuilder.UseSqlServer("server=127.0.0.1,1433;database=PMSUsersDB;user id=sa;password=Cagil123!;trustservercertificate=true;");
            return new UsersDb(optionsBuilder.Options);
        }
    }
}
