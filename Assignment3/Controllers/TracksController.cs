using Assignment3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment3.Controllers
{
    public class TracksController : Controller
    {
        // Reference to a manager object
        private Manager m = new Manager();

        // GET: Tracks
        public ActionResult Index()
        {
            return View(m.TrackGetAll());
        }

        public ActionResult Pop()
        {
            return View("Index", m.TrackGetAllPop());
        }

        public ActionResult DeepPurple()
        {
            return View("Index", m.TrackGetAllDeepPurple());
        }

        public ActionResult Longest()
        {
            return View("Index", m.TrackGetAllTop100Longest());
        }

        // GET: Tracks/Details/5
        public ActionResult Details(int? id)
        {
            var obj = m.TrackGetByIdWithDetail(id.GetValueOrDefault());

            if (obj == null)
                return HttpNotFound();
            else
                return View(obj);
        }

        // GET: Tracks/Create
        public ActionResult Create()
        {
            var form = new TrackAddFormViewModel();
            form.AlbumList = new SelectList(m.AlbumGetAll(), "AlbumId", "Title");
            form.MediaTypeList = new SelectList(m.MediaTypeGetAll(), "MediaTypeId", "Name");
            return View(form);
        }

        // POST: Tracks/Create
        [HttpPost]
        public ActionResult Create(TrackAddViewModel newItem)
        {
            if (!ModelState.IsValid)
            {
                var form = m.mapper.Map<TrackAddViewModel, TrackAddFormViewModel>(newItem);
                form.AlbumList = new SelectList(m.AlbumGetAll(), "AlbumId", "Title");
                form.MediaTypeList = new SelectList(m.MediaTypeGetAll(), "MediaTypeId", "Name");
                return View(form);
            }

            try
            {
                var addedItem = m.TrackAdd(newItem);
                if (addedItem == null)
                {
                    var form = m.mapper.Map<TrackAddViewModel, TrackAddFormViewModel>(newItem);
                    form.AlbumList = new SelectList(m.AlbumGetAll(), "AlbumId", "Title");
                    form.MediaTypeList = new SelectList(m.MediaTypeGetAll(), "MediaTypeId", "Name");
                    return View(form);
                }
                else
                {
                    return RedirectToAction("Details", new { id = addedItem.TrackId });
                }
            }
            catch
            {
                var form = m.mapper.Map<TrackAddViewModel, TrackAddFormViewModel>(newItem);
                form.AlbumList = new SelectList(m.AlbumGetAll(), "AlbumId", "Title");
                form.MediaTypeList = new SelectList(m.MediaTypeGetAll(), "MediaTypeId", "Name");
                return View(form);
            }
        }

        // GET: Tracks/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Tracks/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Tracks/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Tracks/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
