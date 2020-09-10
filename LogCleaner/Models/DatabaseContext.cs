using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LogCleaner.Models
{
    public class DatabaseContext : DbContext
    {
        private IConfiguration configuration;
        
        public DatabaseContext()
        {
            configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();    //konfigürasyon için kullanacağımız dosya adı tanımlanmalı
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);    //konfigürasyon dosyamızda veri tabanı bilgilerinin hangi değişken adı altında verildiği tanımlanmalı
        }
        
        public DbSet<Table> Log { get; set; }        // Log => bağlanacağımız veri tabanında işlem yapacağımz tablonun adı tanımlanmalı (farklı tabloda işlem yapmak için yeni kullanılacak tablo adı ile değiştirilebilir)
    }
}