using Dapper.Project.Core.Entity;
using Dapper.Project.Data;
using DapperProject.Web.Models;
using Microsoft.AspNetCore.Mvc;
namespace DapperProject.Web.Controllers;

public class SuppliersController : Controller
{
    private readonly IRepository<Suppliers> _supplierRepository;

    public SuppliersController(IRepository<Suppliers> supplierRepository)
    {
        _supplierRepository = supplierRepository;
        
    } 
    
    
    // GET
    public IActionResult Index()
    {
        try
        {
            var supplier = _supplierRepository.GetAll();
            return View(supplier);
        }
        catch(Exception ex )
        {
            var errorModel = new ErrorViewModel("ERROR", ex.Message);
            return RedirectToAction("Error", "Home",errorModel); 
        }
    }
    

    // GET
    public IActionResult Create()
    {
        try
        {
            return View();
        }
        catch(Exception ex )
        {
            var errorModel = new ErrorViewModel("ERROR", ex.Message); 
            return RedirectToAction("Error", "Home",errorModel); 
        }
        
    }

    // POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("product_id, suppliers_product_brand,suppliers_communication_information,suppliers_address,suppliers_iban")] Suppliers supplier)
    {
        try
        {
            if (!ModelState.IsValid) 
            {
                _supplierRepository.Insert(supplier);
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
            

        }
        
        catch(Exception ex )
        {
            var errorModel = new ErrorViewModel("ERROR", ex.Message); 
            return RedirectToAction("Error", "Home",errorModel);
        }
    }
    
    // GET
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var supplier = await _supplierRepository.GetById(id);
            return View(supplier);
        }
        catch(Exception ex )
        {
            var errorModel = new ErrorViewModel("ERROR", ex.Message); 
            return RedirectToAction("Error", "Home",errorModel); 
        }
    }
    
    // POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("id,suppliers_product_brand,suppliers_communication_information,suppliers_address,suppliers_iban")] Suppliers supplier)
    {
        try
        {
            if (id != supplier.id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) 
            {
                _supplierRepository.Update(supplier);
                return RedirectToAction(nameof(Index));
            }

            return View(supplier);

        }
        catch(Exception ex )
        {
            var errorModel = new ErrorViewModel("ERROR", ex.Message); 
            return RedirectToAction("Error", "Home",errorModel); 
        }
        
    }
    
    
    // GET
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var supplier = await _supplierRepository.GetById(id);
            
            if (supplier == null)
            {
                return NotFound();
            }
            
            return View(supplier);
        }
        catch(Exception ex )
        {
            var errorModel = new ErrorViewModel("ERROR", ex.Message); 
            return RedirectToAction("Error", "Home",errorModel);
        }
        
        
    }

    // POST
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        try
        {
            _supplierRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
        catch(Exception ex )
        {
            var errorModel = new ErrorViewModel("ERROR", ex.Message); 
            return RedirectToAction("Error", "Home",errorModel); 
            

        }
    }
}