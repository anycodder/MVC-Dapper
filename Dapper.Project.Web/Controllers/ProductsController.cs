using Dapper.Project.Core.Entity;
using Dapper.Project.Data;
using DapperProject.Web.Models;
using Microsoft.AspNetCore.Mvc;
namespace DapperProject.Web.Controllers;

public class ProductsController : Controller
{
    private readonly IRepository<Products> _productRepository;

    public ProductsController(IRepository<Products> productRepository)
    {
        _productRepository = productRepository;
    }
    
    // GET
    public IActionResult Index()
    {
        try
        {
            var products = _productRepository.GetAll();
            return View(products);
        }
        catch(Exception ex )
        {
            var error_model = new ErrorViewModel("ERROR", ex.Message); 
            return RedirectToAction("Error", "Home",error_model); 
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
            var error_model = new ErrorViewModel("ERROR", ex.Message); 
            return RedirectToAction("Error", "Home",error_model); 
        }
        
    }

    // POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("products_name,products_description,products_price")] Products products)
    {
        try
        {
            if (ModelState.IsValid)
            {
                _productRepository.Insert(products);
                return RedirectToAction(nameof(Index));
            }
            return View(products);
        }
        catch(Exception ex )
        {
            var error_model = new ErrorViewModel("ERROR", ex.Message); 
            return RedirectToAction("Error", "Home",error_model);
        }
    }
    
    // GET
    public async Task<IActionResult>  Edit(int id)
    {
        try
        {
            var products = await _productRepository.GetById(id);
            if (products == null)
            {
                return NotFound();
            }
            return View(products);
        }
        catch(Exception ex )
        {
            var error_model = new ErrorViewModel("ERROR", ex.Message); 
            return RedirectToAction("Error", "Home",error_model);
        }
        
        
    }

    // POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("id,products_name,products_description,products_price")] Products products)
    {
        try
        {
            if (id != products.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _productRepository.Update(products);
                return RedirectToAction(nameof(Index));
            }
            return View(products);
        }
        catch(Exception ex )
        {
            var error_model = new ErrorViewModel("ERROR", ex.Message); 
            return RedirectToAction("Error", "Home",error_model);
        }
    }
    
    // GET
    public async Task<IActionResult>  Delete(int id)
    {
        
        try
        {
            Products products = await _productRepository.GetById(id);
            if (products == null)
            {
                return NotFound();
            }
            return View(products);
        }
        catch(Exception ex )
        {
            var error_model = new ErrorViewModel("ERROR", ex.Message); 
            return RedirectToAction("Error", "Home",error_model);
        }
    }

    // POST
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        try
        {
            _productRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
        catch(Exception ex )
        {
            var error_model = new ErrorViewModel("ERROR", ex.Message); 
            return RedirectToAction("Error", "Home",error_model); 
        }
    }
}