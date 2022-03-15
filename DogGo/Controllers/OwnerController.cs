using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DogGo.Repositories;
using DogGo.Models;

namespace DogGo.Controllers
{
    public class OwnerController : Controller
    {
        private readonly IOwnerRepository _ownerRepository;

        public OwnerController(IOwnerRepository ownerRepo)
        {
            _ownerRepository = ownerRepo;
        }

        // GET: OwnerController
        public ActionResult Index()
        {
            List<Owner> owners = _ownerRepository.getAllOwners();

            return View(owners);
        }

        // GET: OwnerController/Details/5
        public ActionResult Details(int id)
        {
            Owner owner = _ownerRepository.getOwnerById(id);

            return View(owner);
        }

        // GET: OwnerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OwnerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OwnerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OwnerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OwnerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OwnerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
