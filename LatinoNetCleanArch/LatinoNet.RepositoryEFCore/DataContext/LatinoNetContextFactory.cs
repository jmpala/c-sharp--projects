using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LatinoNet.RepositoryEFCore.DataContext
{
    internal class LatinoNetContextFactory : IDesignTimeDbContextFactory<LatinoNetContext>
    {
        public LatinoNetContext CreateDbContext(string[] args)
        {
            var OptionBuilder = new DbContextOptionsBuilder<LatinoNetContext>();
            OptionBuilder.UseSqlServer("server=(localdb)\\mssqllocaldb;database=LatinoNet");
            return new LatinoNetContext(OptionBuilder.Options);
        }
    }
}
