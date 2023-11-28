using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using sahand.Models.Database;
public class Application : DbContext
    {
        public Application(DbContextOptions<Application> options) : base(options)
        {
        }
        //تنظیمات معرفی جدول
         public DbSet<Tbl_Header> tbl_Headers {get;set;}
        public DbSet<Tbl_Company> tbl_Companies {get;set;}
        
        public DbSet<Tbl_AboutUs> tbl_AboutUs {get;set;}

        public DbSet<Tbl_AboutUsitem> tbl_AboutUsitems {get;set;}
    }

    public class ToDoContextFactory : IDesignTimeDbContextFactory<Application>
    {
        public Application CreateDbContext(string[] args)
        {
            //نام جدول
            var builder = new DbContextOptionsBuilder<Application>();
            builder.UseSqlServer("Data Source=.;initial Catalog=sahand;integrated Security=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=True");
            return new Application(builder.Options);
        }
    }