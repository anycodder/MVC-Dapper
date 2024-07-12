using Dapper.Project.Core.Entity;
using Dapper.Project.Data;
using DapperProject.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace DapperProject.Web.Controllers
{
    public class FinanceController : Controller
    {
        private readonly IRepository<Finance> _financeRepository;

        public FinanceController(IRepository<Finance> financeRepository)
        {
            _financeRepository = financeRepository;
        }

        // GET: Finance
        public IActionResult Index()
        {
            try
            {
                var finances = _financeRepository.GetAll();
                return View(finances);
            }
            catch (Exception ex)
            {
                var errorModel = new ErrorViewModel("ERROR", ex.Message);
                return RedirectToAction("Error", "Home", errorModel);
            }
        }

        // GET: Finance/Create
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

        // POST: Finance/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("sales_titles_id, suppliers_id, finance_date, finance_earnings, finance_commissions")] Finance finance)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _financeRepository.Insert(finance);
                    return RedirectToAction(nameof(Index));
                }

                return View(finance);
            }
            catch (Exception ex)
            {
                var errorModel = new ErrorViewModel("ERROR", ex.Message);
                return RedirectToAction("Error", "Home", errorModel);
            }
        }

        // GET: Finance/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var finance = await _financeRepository.GetById(id);
                return View(finance);
            }
            catch (Exception ex)
            {
                var errorModel = new ErrorViewModel("ERROR", ex.Message);
                return RedirectToAction("Error", "Home", errorModel);
            }
        }

        // POST: Finance/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("id, sales_titles_id, suppliers_id, finance_date, finance_earnings, finance_commissions")] Finance finance)
        {
            try
            {
                if (id != finance.id)
                {
                    return NotFound();
                }

                if (!ModelState.IsValid)
                {
                    _financeRepository.Update(finance);
                    return RedirectToAction(nameof(Index));
                }

                return View(finance);
            }
            catch (Exception ex)
            {
                var errorModel = new ErrorViewModel("ERROR", ex.Message);
                return RedirectToAction("Error", "Home", errorModel);
            }
        }

        // GET: Finance/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var finance = await _financeRepository.GetById(id);

                if (finance == null)
                {
                    return NotFound();
                }

                return View(finance);
            }
            catch (Exception ex)
            {
                var errorModel = new ErrorViewModel("ERROR", ex.Message);
                return RedirectToAction("Error", "Home", errorModel);
            }
        }

        // POST: Finance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _financeRepository.Delete(id);
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
