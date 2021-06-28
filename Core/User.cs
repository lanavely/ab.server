using System;
using System.ComponentModel.DataAnnotations;
using Core.ModelValidations;

namespace Core
{
    public class User
    {
        public int? Id { get; set; }

        [Required] 
        [User_DateNotInTheFuture] 
        public DateTime DateRegistration { get; set; }

        [Required]
        [User_DateNotInTheFuture]
        [User_LastActivityLaterThenDateRegistration]
        public DateTime DateLastActivity { get; set; }
    }
}