using System;
using System.ComponentModel.DataAnnotations;

namespace LogCleaner.Models
{
    public class Table
    {
        [Key]
        public int LogId { get; set; }        // veri tabanındaki tablomuz ile eşleştirme için id mutlaka olmalı

        public DateTime Date { get; set; }    // tablomuzda bulunan Date sütununa göre silme işlemi yapacağımız için aynı tipte tanımlanmalı
    }
}