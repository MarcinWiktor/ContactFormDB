using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ContactFormDB.Models;
using ContactFormDB.Repository;
using ContactFormDB.ServiceLogic;
namespace ContactFormDB.Controllers
{
    public class ContactFormsController : Controller
    {
        private readonly ContactFormRepository _contactFormRepository;
        private Context db = new Context();
        private readonly EmailService _sendEmail;
        public ContactFormsController()
        {
            _sendEmail = new EmailService();
            _contactFormRepository = new ContactFormRepository();
        }
        public ActionResult Index()
        {
            return View(_contactFormRepository.GetAll());
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ContactForm contactForm = _contactFormRepository.GetById(id.Value);
            if (contactForm == null)
            {
                return HttpNotFound();
            }
            return View(contactForm);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ContactForm contactForm)
        {
            if (ModelState.IsValid)
            {
                contactForm.SendTime = DateTime.Now;
               _contactFormRepository.Create(contactForm);
                var message = _sendEmail.CreateMailMessage(contactForm);
                _sendEmail.SendEmail(message);
                message = _sendEmail.CreateMailMessage(contactForm, true);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contactForm);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ContactForm contactForm = _contactFormRepository.GetById(id.Value);
            if (contactForm == null)
            {
                return HttpNotFound();
            }
            return View(contactForm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ContactForm contactForm)
        {
            if (ModelState.IsValid)
            {
                contactForm.SendTime = DateTime.Now;
                db.Entry(contactForm).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contactForm);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactForm contactForm = _contactFormRepository.GetById(id.Value);
            if (contactForm == null)
            {
                return HttpNotFound();
            }
            return View(contactForm);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ContactForm contactForm = _contactFormRepository.GetById(id);
            _contactFormRepository.Delete(contactForm);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}


