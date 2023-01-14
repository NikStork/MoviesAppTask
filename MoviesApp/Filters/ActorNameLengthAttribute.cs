using System;
using System.ComponentModel.DataAnnotations;

namespace MoviesApp.Filters;

public class ActorNameLengthAttribute : ValidationAttribute
{
    public ActorNameLengthAttribute(int length)
    {
        lengthName = length;
    }

    private int lengthName { get; }

    public string GetErrorMessage() =>
        $"The length of the name must be at least {lengthName} characters.";

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is string)
        {
            var releaseLength = ((string) value).Length;

            if (releaseLength < lengthName)
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }
        else
        {
            return new ValidationResult("Incorrect data type.");
        }
    }
}