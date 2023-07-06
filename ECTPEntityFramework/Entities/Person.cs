using System;
using System.ComponentModel.DataAnnotations;

namespace ECTPEntityFramework.Entities
{
    public class Person
    {
        public int PersonId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }
    }
}