using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ContactFormDB.Models;

namespace ContactFormDB.Repository
{
    public class ContactFormRepository
    {
        public List<ContactForm> GetAll()
        {
            using (var context = new Context())
            {
                return context.ContactForm.ToList();
            }
        }
        public ContactForm GetById(int id)
        {
            using (var context = new Context())
            {
                return context.ContactForm.FirstOrDefault(x => x.Id == id);
            }
        }
        public void Create(ContactForm model)
        {
            using (var context = new Context())
            {
                context.ContactForm.Add(model);
                context.SaveChanges();
            }
        }
        public void Delete(ContactForm model)
        {
            using (var context = new Context())
            {
                context.ContactForm.Remove(model);
                context.SaveChanges();
            }
        }
        public void Edit(ContactForm model)
        {
            using (var context = new Context())
            {
                context.Entry(model).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}