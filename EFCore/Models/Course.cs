using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.Model
{
    [Table("course")]
    public class Course
    {
        [Key]
        public Guid CourseId { get; set; }        
        public string Name { get; set; }
        public int Price { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
