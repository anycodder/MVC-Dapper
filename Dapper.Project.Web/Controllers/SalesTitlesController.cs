using Dapper.Project.Core.Entity;
using Dapper.Project.Data;
using DapperProject.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace DapperProject.Web.Controllers
{
    public class SalesTitlesController : Controller
    {
        private readonly IRepository<Sales_Titles> _salesTitlesRepository;

        public SalesTitlesController(IRepository<Sales_Titles> salesTitlesRepository)
        {
            _salesTitlesRepository = salesTitlesRepository;
        }

        // GET: SalesTitles
        public IActionResult Index()
        {
            try
            {
                var salesTitles = _salesTitlesRepository.GetAll();
                return View(salesTitles);
            }
            catch (Exception ex)
            {
                var errorModel = new ErrorViewModel("ERROR", ex.Message);
                return RedirectToAction("Error", "Home", errorModel);
            }
        }

        // GET: SalesTitles/Create
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

        // POST: SalesTitles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("user_id, sales_titles_date, sales_titles_total_price, sales_titles_order_status")] Sales_Titles salesTitles)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _salesTitlesRepository.Insert(salesTitles);
                    return RedirectToAction(nameof(Index));
                }

                return View(salesTitles);
            }
            catch (Exception ex)
            {
                var errorModel = new ErrorViewModel("ERROR", ex.Message);
                return RedirectToAction("Error", "Home", errorModel);
            }
        }

        // GET: SalesTitles/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var salesTitles = await _salesTitlesRepository.GetById(id);
                return View(salesTitles);
            }
            catch (Exception ex)
            {
                var errorModel = new ErrorViewModel("ERROR", ex.Message);
                return RedirectToAction("Error", "Home", errorModel);
            }
        }

        // POST: SalesTitles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("id, user_id, sales_titles_date, sales_titles_total_price, sales_titles_order_status")] Sales_Titles salesTitles)
        {
            try
            {
                if (id != salesTitles.id)
                {
                    return NotFound();
                }

                if (!ModelState.IsValid)
                {
                    _salesTitlesRepository.Update(salesTitles);
                    return RedirectToAction(nameof(Index));
                }

                return View(salesTitles);
            }
            catch (Exception ex)
            {
                var errorModel = new ErrorViewModel("ERROR", ex.Message);
                return RedirectToAction("Error", "Home", errorModel);
            }
        }

        // GET: SalesTitles/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var salesTitles = await _salesTitlesRepository.GetById(id);

                if (salesTitles == null)
                {
                    return NotFound();
                }

                return View(salesTitles);
            }
            catch (Exception ex)
            {
                var errorModel = new ErrorViewModel("ERROR", ex.Message);
                return RedirectToAction("Error", "Home", errorModel);
            }
        }

        // POST: SalesTitles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _salesTitlesRepository.Delete(id);
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
