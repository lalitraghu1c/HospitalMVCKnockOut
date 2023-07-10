using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HospitalManagement;

namespace HospitalManagement.Controllers
{
    public class HospitalsController : Controller
    {
        private HospitalEntities db = new HospitalEntities();

        // GET: Hospitals
        public ActionResult Index()
        {
            var res = db.Hospital.AsNoTracking().AsEnumerable().Select(x => new Hospital
            {
                HospitalId = x.HospitalId,
                HospitalName = x.HospitalName,
                HospitalAddress =x.HospitalAddress,
                HospialCity = x.HospialCity,
                HospitalContactNumber = x.HospitalContactNumber,
                HospitalSpeciality = x.HospitalSpeciality,
                HospitalArea = x.HospitalArea,
                DateOfJoinin = x.DateOfJoinin

            }).ToList();
            return View(res);
        }

        // GET: Hospitals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hospital hospital = db.Hospital.Find(id);
            if (hospital == null)
            {
                return HttpNotFound();
            }
            ViewData["ListValue"] = db.Hospital.ToList();

            return View(hospital);
        }

        // GET: Hospitals/Create

        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                List<Hospital> listData = new List<Hospital>();
            listData = db.Hospital.ToList();

            ViewData["ListValue"] = db.Hospital.ToList();
            return View();
            }
            catch (Exception e)
            {
                
                throw e;
            }
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HospitalId,HospitalName,HospitalSpeciality,HospitalAddress,HospialCity,HospitalContactNumber,HospitalArea,DateOfJoinin")] Hospital hospital)
        {
            if (ModelState.IsValid)
            {
                Hospital data = new Hospital();

                data.DateOfJoinin = hospital.DateOfJoinin;
                data.HospitalName = hospital.HospitalName;
                data.HospitalSpeciality = hospital.HospitalSpeciality;
                data.HospitalAddress = hospital.HospitalAddress;
                data.HospitalArea = hospital.HospitalArea;
                data.HospialCity = hospital.HospialCity;
                data.HospitalContactNumber = hospital.HospitalContactNumber;
                db.Hospital.Add(data);
                db.SaveChanges();
                ViewData["ListValue"] = data;

                return RedirectToAction("Create");
            }

            return View(hospital);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hospital hospital = db.Hospital.Find(id);
            if (hospital == null)
            {
                return HttpNotFound();
            }
            ViewData["ListValue"] = db.Hospital.ToList();

            return View(hospital);
        }

        // POST: Hospitals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HospitalId,HospitalName,HospitalSpeciality,HospitalAddress,HospialCity,HospitalContactNumber,HospitalArea,DateOfJoinin")] Hospital hospital)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hospital).State = EntityState.Modified;
                db.SaveChanges();
                ViewData["ListValue"] = db.Hospital.ToList();

                return RedirectToAction("Edit");
            }
            return View(hospital);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hospital hospital = db.Hospital.Find(id);
            if (hospital == null)
            {
                return HttpNotFound();
            }
            ViewData["ListValue"] = db.Hospital.ToList();

            return View(hospital);
        }

        // POST: Hospitals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Hospital hospital = db.Hospital.Find(id);
            db.Hospital.Remove(hospital);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
