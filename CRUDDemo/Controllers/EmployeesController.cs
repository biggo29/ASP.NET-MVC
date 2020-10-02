using CRUDDemo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CRUDDemo.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly CrudContext _crudContext = new CrudContext();
        // GET: Employees
        public ActionResult Index()
        {
            var employees = _crudContext.Employees.ToList();
            return View(employees);
        }

        //GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        //POST: Create
        [HttpPost]
        public ActionResult Create( Employee employee)
        {
            if(ModelState.IsValid)
            {
                employee.EmployeeId = 1;
                _crudContext.Employees.Add(employee);
                _crudContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        //GET: Employees/Edit(id)
        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var employee = _crudContext.Employees.FirstOrDefault(e => e.EmployeeId == id);
            if(employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        //POST: Employees/Edit(id)
        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            if(ModelState.IsValid)
            {
                _crudContext.Entry(employee).State = EntityState.Modified;
                _crudContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        //GET: Employee/Details(id)
        public ActionResult Detail(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var employee = _crudContext.Employees.FirstOrDefault(x => x.EmployeeId == id);
            if(employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        //GET: Employee/Delete(id)
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var employee = _crudContext.Employees.SingleOrDefault(e => e.EmployeeId == id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        //POST: Employee/Delete(id)
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var employee = _crudContext.Employees.SingleOrDefault(x => x.EmployeeId == id);
            _crudContext.Employees.Remove(employee ?? throw new InvalidOperationException());
            _crudContext.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _crudContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}