﻿using DogGo.Models;
using DogGo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DogGo.Controllers
{
    public class DogController : Controller
    {
        private readonly IDogRepository _dogRepository;

        public DogController(IDogRepository dogRepo)
        {
            _dogRepository = dogRepo;
        }

        // GET: DogController
        public ActionResult Index()
        {
            List<Dog> dogs = _dogRepository.GetAllDogs();
            return View(dogs);
        }

        // GET: DogController/Details/5
        public ActionResult Details(int id)
        {
            Dog dog = _dogRepository.GetDogById(id);
            return View(dog);
        }

        // GET: DogController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DogController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Dog dog)
        {
            try
            {
                _dogRepository.AddDog(dog);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(dog);
            }
        }

        // GET: DogController/Edit/5
        public ActionResult Edit(int id)
        {
            Dog dog = _dogRepository.GetDogById(id);
            return View(dog);
        }

        // POST: DogController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Dog dog)
        {
            try
            {
                _dogRepository.UpdateDog(dog);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(dog);
            }
        }

        // GET: DogController/Delete/5
        public ActionResult Delete(int id)
        {
            Dog dog = _dogRepository.GetDogById(id);
            return View(dog);
        }

        // POST: DogController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Dog dog)
        {
            try
            {
                _dogRepository.DeleteDog(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(dog);
            }
        }
    }
}
