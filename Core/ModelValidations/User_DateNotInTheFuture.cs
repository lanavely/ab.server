using System;
using System.ComponentModel.DataAnnotations;

namespace Core.ModelValidations
{
    public class User_DateNotInTheFuture : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            return !(value is DateTime date && date > DateTime.Now);
        }
    }
}