using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProductValidation.Core.Models;

namespace ProductValidation.API.Filters;

public class ModelBindingValidationActionFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid)
        {
            return;
        }

        var validationErrorResult = context.ModelState.Select(modelKeyValue =>
        {
            var fieldName = modelKeyValue.Key.Contains('$') ? modelKeyValue.Key.Substring(2) : modelKeyValue.Key;
            return new FieldValidationResult
            {
                FieldName = fieldName,
                ErrorMessage = $"Provided value for {fieldName} is incorrect."
            };
        }).ToList();

        context.Result = new BadRequestObjectResult(validationErrorResult);
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}