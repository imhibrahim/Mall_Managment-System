﻿using Mall_Managment_System.Migrations.ContactDb;
using Mall_Managment_System.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mall_Managment_System.Controllers
{
    public class ContactController : Controller
    {
        ApplicationDbContext Contact_context;
        IWebHostEnvironment env;

        public ContactController(ApplicationDbContext Contact, IWebHostEnvironment hc)
        {
            this.Contact_context = Contact;
            env = hc;
        }

        public IActionResult Index()
        {
            return View(Contact_context.contact.ToList());
        }

        public IActionResult AddContact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddContact(Contact contact)
        {
        
            Contact Contact = new Contact
            {
                Name= contact.Name,
                Email= contact.Email,
                Number= contact.Number,
                Massage = contact.Massage
             
               
            };

            Contact_context.contact.Add(Contact);
            Contact_context.SaveChanges();
            ViewBag.success = "Recoard inserted";

            return RedirectToAction("index");

        }


        public IActionResult details(int id) {
        
            var data= Contact_context.contact.FirstOrDefault(x=>x.id==id);
            return View(data);
        }


        public IActionResult Delete(int id)
        {
            var contact = Contact_context.contact.Find(id);
            if (contact == null)
            {
                return RedirectToAction("Index");
            }

            Contact_context.contact.Remove(contact);
            Contact_context.SaveChanges(true);
            return RedirectToAction("Index");
        }

    }
}
