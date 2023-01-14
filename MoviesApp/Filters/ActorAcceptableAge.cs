using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MoviesApp.Filters;

public class ActorAcceptableAge: Attribute, IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var acceptableDate = DateTime.Parse(context.HttpContext.Request.Form["BirthDate"]);
        var currentYear = DateTime.Now.Year - acceptableDate.Year;
        
        if (currentYear < 7 || currentYear > 99)
        {
            context.Result = new BadRequestResult();
        }
    }
        
    public void OnActionExecuted(ActionExecutedContext context)
    {
            
    }
}