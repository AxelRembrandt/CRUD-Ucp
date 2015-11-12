using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ucp.Models.Data;

namespace Ucp.Controllers
{
    public class ContractStatusController : Controller
    {
        private UcpContext db = new UcpContext();

        // GET: ContractStatus
        public ActionResult Index()
        {
            return View(db.Statuses.ToList());
        }

        // GET: ContractStatus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContractStatus contractStatus = db.Statuses.Find(id);
            if (contractStatus == null)
            {
                return HttpNotFound();
            }
            return View(contractStatus);
        }

        // GET: ContractStatus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContractStatus/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ContractStatusId,ContrStatusVal")] ContractStatus contractStatus)
        {
            if (ModelState.IsValid)
            {
                db.Statuses.Add(contractStatus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contractStatus);
        }

        // GET: ContractStatus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContractStatus contractStatus = db.Statuses.Find(id);
            if (contractStatus == null)
            {
                return HttpNotFound();
            }
            return View(contractStatus);
        }

        // POST: ContractStatus/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ContractStatusId,ContrStatusVal")] ContractStatus contractStatus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contractStatus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contractStatus);
        }

        // GET: ContractStatus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContractStatus contractStatus = db.Statuses.Find(id);
            if (contractStatus == null)
            {
                return HttpNotFound();
            }
            return View(contractStatus);
        }

        // POST: ContractStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ContractStatus contractStatus = db.Statuses.Find(id);
            db.Statuses.Remove(contractStatus);
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
