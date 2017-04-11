using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace ConsoleApplicationEFTest.Model
{
    public class AppContext : DbContext
    {
        internal BaseQueryFilterInterceptor SoftDeletableFilter;
        public AppContext() : base("name=ContextString")
        {
            this.SoftDeletableFilter = this.Filter<TestEntity>(x => x.Where(a => a.Deleted == false));
        }

        public DbSet<TestEntity> Entities { get; set; }
    }
}
