using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarInsurance.Models;

namespace CarInsurance.Controllers
{
    public class InsureeController : Controller
    {
        private InsuranceEntities db = new InsuranceEntities();

        // GET: Insuree
        public ActionResult Index()
        {
            return View(db.Insurees.ToList());
        }

        // GET: Insuree
        public ActionResult Admin()
        {
            return View(db.Insurees.ToList());
        }

        // GET: Insuree/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null)
            {
                return HttpNotFound();
            }
            return View(insuree);
        }

        // GET: Insuree/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Insuree/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,DUI,SpeedingTickets,CoverageType,Quote")] Insuree insuree)
        {
            decimal quote = 50.00m;
            System.Diagnostics.Debug.WriteLine(DateTime.Now);
            System.Diagnostics.Debug.WriteLine(insuree.DateOfBirth);
            DateTime zeroTime = new DateTime(1, 1, 1);
            TimeSpan span = DateTime.Now - insuree.DateOfBirth;
            int age = (zeroTime + span).Year;
            System.Diagnostics.Debug.WriteLine("Entry is {0} years old.",age);
            if(age <= 18)
            {
                quote += 100;
            } else if (age >= 19 && age <= 25)
            {
                quote += 50;
            } else if (age >= 26)
            {
                quote += 25;
            }

            if (insuree.CarYear < 2000)
            {
                quote += 25;
            } else if (insuree.CarYear > 2015)
            {
                quote += 25;
            }

            if (insuree.CarMake == "Porsche")
            {
                if (insuree.CarModel == "911 Carera")
                {
                    quote += 50;
                }
                else
                {
                    quote += 25;
                }
            }

            quote += 10 * insuree.SpeedingTickets;

            if(insuree.DUI)
            {
                quote *= 1.25m;
            }

            if(insuree.CoverageType)
            {
                quote *= 1.5m;
            }

            insuree.Quote = quote;





            //int millisecondsAlive = Convert.ToInt32(DateTime.Now - insuree.DateOfBirth);
            //System.Diagnostics.Debug.WriteLine(millisecondsAlive);
            //if (DateTime.Now - insuree.DateOfBirth >= )

            System.Diagnostics.Debug.WriteLine(insuree.Quote);
            if (ModelState.IsValid)
            {
                db.Insurees.Add(insuree);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(insuree);
        }

        // GET: Insuree/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null)
            {
                return HttpNotFound();
            }
            return View(insuree);
        }

        // POST: Insuree/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,DUI,SpeedingTickets,CoverageType,Quote")] Insuree insuree)
        {
            if (ModelState.IsValid)
            {
                db.Entry(insuree).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(insuree);
        }

        // GET: Insuree/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null)
            {
                return HttpNotFound();
            }
            return View(insuree);
        }

        // POST: Insuree/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Insuree insuree = db.Insurees.Find(id);
            db.Insurees.Remove(insuree);
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
