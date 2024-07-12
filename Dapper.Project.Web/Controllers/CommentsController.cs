using Dapper.Project.Core.Entity;
using Dapper.Project.Data;
using DapperProject.Web.Models;
using Microsoft.AspNetCore.Mvc;
namespace DapperProject.Web.Controllers;

public class CommentsController : Controller
{
    private readonly IRepository<Comments> _commentRepository;

    public CommentsController(IRepository<Comments> commentRepository)
    {
        _commentRepository = commentRepository;
        
    } 
    
    // GET
    public IActionResult Index()
    {
        try
        {
            var comment = _commentRepository.GetAll();
            return View(comment);
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
    public IActionResult Create([Bind("product_id, user_id, answer_id, comment_text, comment_date, comment_type, comment_score")] Comments comments)
    {
        try
        {
            if (!ModelState.IsValid) ////??????????????????????
            {
                _commentRepository.Insert(comments);
                return RedirectToAction(nameof(Index));
            }
            return View(comments);
            
            /*if (!ModelState.IsValid)
            {
                var Mesaj = "";
                foreach (var item in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Mesaj += item.ErrorMessage + "<br />";
                }
                return RedirectToAction("Error", "Home",Mesaj);
            }
            _commentRepository.Insert(comments);
            return RedirectToAction(nameof(Index));*/

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
            var comment = await _commentRepository.GetById(id);
            return View(comment);
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
    public IActionResult Edit(int id, [Bind("id,comment_text, comment_date, comment_score")] Comments comment)
    {
        try
        {
            if (id != comment.id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) /////???????????
            {
                _commentRepository.Update(comment);
                return RedirectToAction(nameof(Index));
            }

            return View(comment);

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
            var comment = await _commentRepository.GetById(id);
            
            if (comment == null)
            {
                return NotFound();
            }
            
            return View(comment);
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
            _commentRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
        catch(Exception ex )
        {
            var error_model = new ErrorViewModel("ERROR", ex.Message); 
            return RedirectToAction("Error", "Home",error_model); 
            

        }
    }
}