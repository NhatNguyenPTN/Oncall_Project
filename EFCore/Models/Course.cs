using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore.Model
{
    [Table("course")]
    public class Course
    {
        [Key]
        public Guid CourseId { get; set; }        
        public string Name { get; set; }
        public int Price { get; set; }
        //public virtual ICollection<User> Users { get; set; }
    }
}
