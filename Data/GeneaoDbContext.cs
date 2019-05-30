using CQELight.DAL.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geneao.Data
{
    public class GeneaoDbContextFactory : IDesignTimeDbContextFactory<GeneaoDbContext>
    {
        public GeneaoDbContext CreateDbContext(string[] args)
            => new GeneaoDbContext(new DbContextOptionsBuilder()
                .UseSqlite("Filename=test.db",
                    opt => opt.MigrationsAssembly(typeof(Program).Assembly.GetName().Name))
                .Options);
    }
    public class GeneaoDbContext : BaseDbContext
    {
        public GeneaoDbContext(DbContextOptions options) 
            : base(options)
        {
        }

    }
}
