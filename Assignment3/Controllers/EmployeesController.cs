using Assignment3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment3.Controllers
{
    public class EmployeesController : Controller
    {
        // Reference to a manager object
        private Manager m = new Manager();

        // GET: Employees
        public ActionResult Index()
        {
            return View(m.EmployeeGetAll());
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            var obj = m.EmployeeGetById(id.GetValueOrDefault());

            if (obj == null)
                return HttpNotFound();
            else
                return View(obj);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View(new EmployeeAddViewModel());
        }

        // POST: Employees/Create
        [HttpPost]
        public ActionResult Create(EmployeeAddViewModel newItem)
        {
            // Validate the input
            if (!ModelState.IsValid)
                return View(newItem);

            try
            {
                // Process the input
                var addedItem = m.EmployeeAdd(newItem);

                // If the item was not added, return the user to the Create page
                // otherwise redirect them to the Details page.
                if (addedItem == null)
                    return View(newItem);
                else
                    return RedirectToAction("Details", new { id = addedItem.EmployeeId });
            }
            catch
            {
                return View(newItem);
            }
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            var o = m.EmployeeGetById(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                var editForm = m.mapper.Map<EmployeeEditFormViewModel>(o);
                return View(editForm);
            }
        }

        // POST: Employees/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, EmployeeEditViewModel newItem)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                return RedirectToAction("edit", new { id = newItem.EmployeeId });
            }

            if (id.GetValueOrDefault() != newItem.EmployeeId)
            {
                return RedirectToAction("index");
            }

            var editedItem = m.EmployeeEdit(newItem);

            if (editedItem == null)
            {
                return RedirectToAction("edit", new { id = newItem.EmployeeId });
            }
            else
            {
                return RedirectToAction("details", new { id = newItem.EmployeeId });
            }
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            var itemToDelete = m.EmployeeGetById(id.GetValueOrDefault());

            if (itemToDelete == null)
            {
                // Don't leak info about the delete attempt
                // Simply redirect
                return RedirectToAction("index");
            }
            else
            {
                return View(itemToDelete);
            }
        }

        // POST: Employees/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, FormCollection collection)
        {
            var result = m.EmployeeDelete(id.GetValueOrDefault());

            return RedirectToAction("index");
        }
    }
}
