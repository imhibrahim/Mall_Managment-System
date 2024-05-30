using Mall_Managment_System.Migrations.ContactDb;
using Mall_Managment_System.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mall_Managment_System.Controllers
{
    public class ContactController : Controller
    {
        ContactDbContext Contact_context;
        IWebHostEnvironment env;

        public ContactController(ContactDbContext Contact, IWebHostEnvironment hc)
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
        
            Contact C = new Contact
            {
             
               
            };

            Contact_context.contact.Add(C);
            Contact_context.SaveChanges();
            ViewBag.success = "Recoard inserted";

            return RedirectToAction("index");
        }

    }
}
