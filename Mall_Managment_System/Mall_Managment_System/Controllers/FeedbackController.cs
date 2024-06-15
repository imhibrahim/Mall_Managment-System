using Mall_Managment_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mall_Managment_System.Controllers
{
    [Authorize(Roles = "1")]
    public class FeedbackController : Controller
    {

        private readonly ApplicationDbContext feed_context;
        private readonly IWebHostEnvironment env;


        public FeedbackController(ApplicationDbContext Feedback, IWebHostEnvironment hc)
        {
            this.feed_context = Feedback;
            env = hc;
        }



        public IActionResult Index()
        {
            return View(feed_context.Feedback.ToList());
        }




        public IActionResult Delete(int id)
        {
            var feedback = feed_context.Feedback.Find(id);
            if (feedback == null)
            {
                return RedirectToAction("Index");
            }
            feed_context.Feedback.Remove(feedback);
            feed_context.SaveChanges(true);
            return RedirectToAction("Index");
        }
    }
}
