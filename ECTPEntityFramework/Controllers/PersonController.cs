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
            
            // TODO: Using LINQ, filter the "people" query to only include matches where the first name or last name contains the search string

            return View(people.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Person person = null;

            // TODO: Use the Find() method provided by the People DbSet to return the person with the given ID

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
                // TODO: Add the person entity passed into this endpoint into the database
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
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // TODO: 

            //var personToUpdate = db.People.Find(id);
            //if (TryUpdateModel(personToUpdate, "",
            //        new string[] { "LastName", "FirstName", "BirthDate" }))
            //{
            //    db.SaveChanges();

            //    return RedirectToAction("Index");
            //}

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
            // TODO: Find the person with the given ID in the database and remove that record

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
