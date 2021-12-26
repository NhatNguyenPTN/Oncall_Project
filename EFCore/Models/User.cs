using EFCore.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore.Model
{
    [Table("users")]
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public Roles Role { get; set; }
       
    }
}
