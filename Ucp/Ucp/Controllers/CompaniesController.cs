using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ucp.Models;
using Ucp.Models.Data;
using PagedList.Mvc;
using PagedList;

namespace Ucp.Controllers
{
    public class CompaniesController : Controller
    {
        private UcpContext db = new UcpContext();

        // GET: Companies
        public ActionResult Index(string searchString, int? page)
        {
            var companies = db.Companies.Include(c => c.ContractStatus);

            // Поиск по именам компаний и статусам контрактов, если строка поиска не пустая.
            if (!String.IsNullOrEmpty(searchString))
            {
                companies = companies.Where(c => c.CompanyName.ToUpper().Contains(searchString.ToUpper())
                                                || c.ContractStatus.ContrStatusVal.ToUpper().Contains(searchString.ToUpper()));
            }

            // Разбиение по страницам
            int pageSize = 3;
            int pageNumber = page ?? 1;
            // Сортировка
            companies = companies.OrderBy(s => s.CompanyName);

            return View(companies.ToPagedList(pageNumber,pageSize));
        }

        // GET: Companies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // GET: Companies/Create
        public ActionResult Create()
        {
            ViewBag.ContractStatusId = new SelectList(db.Statuses, "ContractStatusId", "ContrStatusVal");
            return View();
        }

        // POST: Companies/Create        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CompanyId,CompanyName,ContractStatusId")] Company company)
        {
            // Проверка на совпадение введенного названия компании с существующими в БД названиями
            var anyCom = db.Companies.Any(c => string.Compare(c.CompanyName, company.CompanyName, false) == 0);
            if (anyCom)
            {
                ModelState.AddModelError("CompanyName", "Компания с таким названием уже существует");
            }

            if (ModelState.IsValid)
            {
                db.Companies.Add(company);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ContractStatusId = new SelectList(db.Statuses, "ContractStatusId", "ContrStatusVal", company.ContractStatusId);
            return View(company);
        }

        // GET: Companies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContractStatusId = new SelectList(db.Statuses, "ContractStatusId", "ContrStatusVal", company.ContractStatusId);
            return View(company);
        }

        // POST: Companies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CompanyId,CompanyName,ContractStatusId")] Company company)
        {
            // Проверка на совпадение введенного названия компании с существующими в БД названиями. Будет реагировать на старое значение, если не изменено, что плохо. =(
            var anyCom = db.Companies.Any(c => string.Compare(c.CompanyName, company.CompanyName) == 0);
            
            if (anyCom)
            {
                ModelState.AddModelError("CompanyName", "Компания с таким названием уже существует");
            }

            if (ModelState.IsValid)
            {
                db.Entry(company).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OldCompanyName = company.CompanyName;
            ViewBag.ContractStatusId = new SelectList(db.Statuses, "ContractStatusId", "ContrStatusVal", company.ContractStatusId);
            return View(company);
        }

        // GET: Companies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Company company = db.Companies.Find(id);
            db.Companies.Remove(company);
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
