using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactFormDB.Models
{
    public class ContactForm
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Content { get; set; }

        public DateTime SendTime { get; set; }
    }
}