using Assignment3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment3.Controllers
{
    public class PlaylistsController : Controller
    {
        // Reference to a manager object
        private Manager m = new Manager();

        // GET: Playlists
        public ActionResult Index()
        {
            return View(m.PlaylistGetAll());
        }

        // GET: Playlists/Details/5
        public ActionResult Details(int? id)
        {
            var obj = m.PlaylistGetByIdWithDetail(id.GetValueOrDefault());

            if (obj == null)
                return HttpNotFound();
            else
                return View(obj);
        }

        // GET: Playlists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Playlists/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Playlists/Edit/5
        public ActionResult Edit(int? id)
        {
            var o = m.PlaylistGetByIdWithDetail(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                var editForm = m.mapper.Map<PlaylistWithDetailViewModel, PlaylistEditTracksFormViewModel>(o);
                var selectedTracks = o.Tracks.Select(t => t.TrackId);

                editForm.TrackList = new MultiSelectList
                    (items: m.TrackGetAll(),
                    dataValueField: "TrackId",
                    dataTextField: "NameFull",
                    selectedValues: selectedTracks);

                editForm.TracksExisted = o.Tracks.OrderBy(t => t.Name);

                return View(editForm);
            }
        }

        // POST: Playlists/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, PlaylistEditTracksViewModel newItem)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                return RedirectToAction("edit", new { id = newItem.PlaylistId });
            }

            if (id.GetValueOrDefault() != newItem.PlaylistId)
            {
                return RedirectToAction("index");
            }

            var editedItem = m.PlaylistEditTracks(newItem);

            if (editedItem == null)
            {
                return RedirectToAction("edit", new { id = newItem.PlaylistId });
            }
            else
            {
                return RedirectToAction("details", new { id = newItem.PlaylistId });
            }
        }

        // GET: Playlists/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Playlists/Delete/5
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
