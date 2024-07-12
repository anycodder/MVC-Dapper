using Dapper.Project.Core.Entity;
using Dapper.Project.Data;
using DapperProject.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace DapperProject.Web.Controllers

{
    public class CargoController : Controller
    {
        private readonly IRepository<Cargo> _cargoRepository;

        public CargoController(IRepository<Cargo> cargoRepository)
        {
            _cargoRepository = cargoRepository;
        }

        // GET: Cargo
        public IActionResult Index()
        {
            try
            {
                var cargos = _cargoRepository.GetAll();
                return View(cargos);
            }
            catch (Exception ex)
            {
                var errorModel = new ErrorViewModel("ERROR", ex.Message);
                return RedirectToAction("Error", "Home", errorModel);
            }
        }

        // GET: Cargo/Create
        public IActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                var errorModel = new ErrorViewModel("ERROR", ex.Message);
                return RedirectToAction("Error", "Home", errorModel);
            }
        }

        // POST: Cargo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("sales_titles_id, user_id, cargo_brand, cargo_status, cargo_estimated_delivery_date")] Cargo cargo)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _cargoRepository.Insert(cargo);
                    return RedirectToAction(nameof(Index));
                }

                return View(cargo);
            }
            catch (Exception ex)
            {
                var errorModel = new ErrorViewModel("ERROR", ex.Message);
                return RedirectToAction("Error", "Home", errorModel);
            }
        }

        // GET: Cargo/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var cargo = await _cargoRepository.GetById(id);
                return View(cargo);
            }
            catch (Exception ex)
            {
                var errorModel = new ErrorViewModel("ERROR", ex.Message);
                return RedirectToAction("Error", "Home", errorModel);
            }
        }

        // POST: Cargo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("id, sales_titles_id, user_id, cargo_brand, cargo_status, cargo_estimated_delivery_date")] Cargo cargo)
        {
            try
            {
                if (id != cargo.id)
                {
                    return NotFound();
                }

                if (!ModelState.IsValid)
                {
                    _cargoRepository.Update(cargo);
                    return RedirectToAction(nameof(Index));
                }

                return View(cargo);
            }
            catch (Exception ex)
            {
                var errorModel = new ErrorViewModel("ERROR", ex.Message);
                return RedirectToAction("Error", "Home", errorModel);
            }
        }

        // GET: Cargo/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var cargo = await _cargoRepository.GetById(id);

                if (cargo == null)
                {
                    return NotFound();
                }

                return View(cargo);
            }
            catch (Exception ex)
            {
                var errorModel = new ErrorViewModel("ERROR", ex.Message);
                return RedirectToAction("Error", "Home", errorModel);
            }
        }

        // POST: Cargo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _cargoRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                var errorModel = new ErrorViewModel("ERROR", ex.Message);
                return RedirectToAction("Error", "Home", errorModel);
            }
        }
    }
}
