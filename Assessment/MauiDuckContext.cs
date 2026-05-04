using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TestLib.Temp;

namespace Assessment
{
    public class MauiDuckContext : DuckContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=C:\Users\della\uni\DAT154\assignments\DuckCare\SQL\DuckCare.db");
        }
    }
}
