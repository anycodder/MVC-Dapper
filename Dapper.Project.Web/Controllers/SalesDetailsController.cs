using Dapper.Project.Core.Entity;
using Dapper.Project.Data;
using DapperProject.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace DapperProject.Web.Controllers
{
    public class SalesDetailsController : Controller
    {
        private readonly IRepository<Sales_Details> _salesDetailsRepository;

        public SalesDetailsController(IRepository<Sales_Details> salesDetailsRepository)
        {
            _salesDetailsRepository = salesDetailsRepository;
        }

        // GET: SalesDetails
        public IActionResult Index()
        {
            try
            {
                var salesDetails = _salesDetailsRepository.GetAll();
                return View(salesDetails);
            }
            catch (Exception ex)
            {
                var errorModel = new ErrorViewModel("ERROR", ex.Message);
                return RedirectToAction("Error", "Home", errorModel);
            }
        }

        // GET: SalesDetails/Create
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

        // POST: SalesDetails/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("sales_title_id, product_id, sales_detail_unit_price_of_product, sales_detail_number_of_product, sales_detail_product_payment")] Sales_Details salesDetails)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _salesDetailsRepository.Insert(salesDetails);
                    return RedirectToAction(nameof(Index));
                }

                return View(salesDetails);
            }
            catch (Exception ex)
            {
                var errorModel = new ErrorViewModel("ERROR", ex.Message);
                return RedirectToAction("Error", "Home", errorModel);
            }
        }

        // GET: SalesDetails/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var salesDetails = await _salesDetailsRepository.GetById(id);
                return View(salesDetails);
            }
            catch (Exception ex)
            {
                var errorModel = new ErrorViewModel("ERROR", ex.Message);
                return RedirectToAction("Error", "Home", errorModel);
            }
        }

        // POST: SalesDetails/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("id, sales_title_id, product_id, sales_detail_unit_price_of_product, sales_detail_number_of_product, sales_detail_product_payment")] Sales_Details salesDetails)
        {
            try
            {
                if (id != salesDetails.id)
                {
                    return NotFound();
                }

                if (!ModelState.IsValid)
                {
                    _salesDetailsRepository.Update(salesDetails);
                    return RedirectToAction(nameof(Index));
                }

                return View(salesDetails);
            }
            catch (Exception ex)
            {
                var errorModel = new ErrorViewModel("ERROR", ex.Message);
                return RedirectToAction("Error", "Home", errorModel);
            }
        }

        // GET: SalesDetails/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var salesDetails = await _salesDetailsRepository.GetById(id);

                if (salesDetails == null)
                {
                    return NotFound();
                }

                return View(salesDetails);
            }
            catch (Exception ex)
            {
                var errorModel = new ErrorViewModel("ERROR", ex.Message);
                return RedirectToAction("Error", "Home", errorModel);
            }
        }

        // POST: SalesDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _salesDetailsRepository.Delete(id);
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

