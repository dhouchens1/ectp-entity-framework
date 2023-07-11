using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ECTPEntityFramework.DataAccess;
using ECTPEntityFramework.Entities;

namespace ECTPEntityFramework.Controllers
{
    public class PersonController : Controller
    {
        private readonly EctpContext db = new EctpContext();

        public ViewResult Index(string currentFilter, string searchString)
        {
            ViewBag.CurrentFilter = currentFilter;

            var people = db.People.AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                people = people.Where(s => s.LastName.Contains(searchString)
                                           || s.FirstName.Contains(searchString));
            }

            return View(people.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LastName, FirstName, BirthDate")]Person person)
        {
            if (ModelState.IsValid)
            {
                db.People.Add(person);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(person);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id, string firstName, string lastName, DateTime birthDate)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var personToUpdate = db.People.Find(id);

            personToUpdate.FirstName = firstName;
            personToUpdate.LastName = lastName;
            personToUpdate.BirthDate = birthDate;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }

            var person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var person = db.People.Find(id);
            db.People.Remove(person);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
