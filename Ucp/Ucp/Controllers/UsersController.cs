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
    public class UsersController : Controller
    {
        private UcpContext db = new UcpContext();

        // GET: Users
        public ActionResult Index(string searchString, int? page)
        {
            var users = db.Users.Include(u => u.Company);

            // Поиск по именам, логинам и названиям компаний.            
            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(u => u.UserName.ToUpper().Contains(searchString.ToUpper())
                                      || u.Login.ToUpper().Contains(searchString.ToUpper())
                                      || u.Company.CompanyName.ToUpper().Contains(searchString.ToUpper())
                                      );
            }
            
            // Разбиение по страницам                     
            int pageSize = 5; // Кол-во элементов на странице
            int pageNumber = page ?? 1; // Номер страницы

            // Сортировка
            users = users.OrderBy(s => s.UserName);
            
            return View(users.ToPagedList(pageNumber, pageSize));
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName");
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,UserName,Login,Password,CompanyId")] User user)
        {
            // Проверки на совпадение:
            // введенного имени пользователя с существующими в БД именами
            var anyUName = db.Users.Any(u => string.Compare(u.UserName, user.UserName,false) == 0);
            // введенного логина с существующими в БД логинами
            var anyLogin = db.Users.Any(u => string.Compare(u.Login, user.Login,false) == 0);

            // Если есть совпадения, выводятся соответствующие ошибки.
            if (anyUName)
            {
                ModelState.AddModelError("UserName", "Пользователь с таким именем уже существует");
            }
            if (anyLogin)
            {
                ModelState.AddModelError("Login", "Пользователь с таким логином уже существует");
            }

            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName", user.CompanyId);
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName", user.CompanyId);
            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,UserName,Login,Password,CompanyId")] User user)
        {
            // Проверки на совпадение:
            // ( будут срабатывать на старых значениях, если не было изменений =( )
            // введенного имени пользователя с существующими в БД именами
            var anyUName = db.Users.Any(u => string.Compare(u.UserName, user.UserName, false) == 0);
            // введенного логина с существующими в БД логинами
            var anyLogin = db.Users.Any(u => string.Compare(u.Login, user.Login, false) == 0);

            // Если есть совпадения, выводятся соответствующие ошибки.
            if (anyUName)
            {
                ModelState.AddModelError("UserName", "Пользователь с таким именем уже существует");
            }
            if (anyLogin)
            {
                ModelState.AddModelError("Login", "Пользователь с таким логином уже существует");
            }

            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName", user.CompanyId);
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
