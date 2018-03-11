using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ContactFormDB.Models
{
    public class Context : DbContext
    {
        public Context () : base ("Default")
        {

        }

        public DbSet<ContactForm> ContactForm { get; set; }
    }
}