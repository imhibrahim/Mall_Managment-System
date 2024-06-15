using Mall_Managment_System.Migrations.ContactDb;
using Mall_Managment_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mall_Managment_System.Controllers
{
    [Authorize(Roles = "1")]
    public class ContactController : Controller
    {
        ApplicationDbContext Contact_context;
        IWebHostEnvironment env;

        public ContactController(ApplicationDbContext Contact, IWebHostEnvironment hc)
        {
            this.Contact_context = Contact;
            env = hc;
        }
        [Authorize(Roles = "1")]
        public IActionResult Index()
        {
            return View(Contact_context.contact.ToList());
        }
        [Authorize(Roles = "1")]
        public IActionResult AddContact()
        {
            return View();
        }
        [Authorize(Roles = "1")]
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

        [Authorize(Roles = "1")]
        public IActionResult details(int id) {
        
            var data= Contact_context.contact.FirstOrDefault(x=>x.id==id);
            return View(data);
        }

        [Authorize(Roles = "1")]
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
