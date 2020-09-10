using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using LogCleaner.BackgroundService;
using LogCleaner.Models;

namespace LogCleaner.ScheduleTask
{
    public class CleanerTask : ScheduledProcessor
    {
        public CleanerTask(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory) { }

        DateTime dateTime = DateTime.Now;

        DatabaseContext _context = new DatabaseContext();
        protected override string Schedule => "0 0 * * *";        // görevin her gün 00:00'da çalışacağını tanımlıyoruz (cron format şeklinde araştırma yaparak istediğiniz zaman diliminde çalışması için ayarlayabilirsiniz)

        public override Task ProcessInScope(IServiceProvider serviceProvider)
        {
            Console.WriteLine("Göreve başladı");

            long count = _context.Log.Where(record => record.Date <= dateTime.AddMonths(-1)).ToList().Count;
            
            for (int i = 0; i <= count; i += 1000)
            {
                var result = _context.Log.Where(record => record.Date <= dateTime.AddMonths(-1)).Take(1000).ToList();        // tüm kayıtları alırsak çok uzun süreceği ve sunucuyu yoracağı için 1000'lik kayıtlar halinde listeleyerek yükü azaltmaya çalışıyoruz
                
                    _context.Log.RemoveRange(result);
                    _context.SaveChanges();
                    Console.WriteLine("1000 tane silindi : " + DateTime.Now);
            }
            
            Console.WriteLine("Hepsi silindi");

            return Task.CompletedTask;
        }
    }
}
