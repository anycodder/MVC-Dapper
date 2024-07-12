using Dapper.Project.Core.Entity;
using Dapper.Project.Data;
using DapperProject.Web.Models;
using Microsoft.AspNetCore.Mvc;
namespace DapperProject.Web.Controllers;

public class UsersController : Controller
{
    private readonly IRepository<Users> _userRepository;

    public UsersController(IRepository<Users> userRepository)
    {
        _userRepository = userRepository;
    }

    // GET
    public IActionResult Index()
    {

        try
        {
            var users = _userRepository.GetAll(); // Tüm yorumları al
            return View(users);
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
    public IActionResult Create([Bind("user_name,user_email,user_password,user_type")] Users user)
    {
        try
        {
            if (ModelState.IsValid)
            {
                _userRepository.Insert(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }
        catch(Exception ex )
        {
            var error_model = new ErrorViewModel("ERROR", ex.Message); 
            return RedirectToAction("Error", "Home",error_model); 
        }
    }
    
    // GET
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var user = await _userRepository.GetById(id);
            return View(user);
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
    public IActionResult Edit(int id, [Bind("id,user_name,user_email,user_password,user_type")] Users user)
    {
        try
            {
                if (id != user.id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    _userRepository.Update(user);
                    return RedirectToAction(nameof(Index));
                }
                return View(user);
            }
            catch(Exception ex )
            {
                var error_model = new ErrorViewModel("ERROR", ex.Message); 
                return RedirectToAction("Error", "Home",error_model); 
            }
        
    }
    
    // GET
    public async Task<IActionResult> Delete(int id)
    {
            try
            {
                var user = await _userRepository.GetById(id);
            
                if (user == null)
                {
                    return NotFound();
                }
            
                return View(user);
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
            _userRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
        catch(Exception ex )
        {
            var error_model = new ErrorViewModel("ERROR", ex.Message); 
            return RedirectToAction("Error", "Home",error_model); 
            

        }
    }
    
    
}