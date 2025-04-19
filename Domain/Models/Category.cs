using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
     public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "the name is required")]
        [MaxLength(200,ErrorMessage ="Max Length is 200")]
        public string Name { get; set; }

        public string? Description { get; set; }
        [ValidateNever]
        public virtual ICollection<Post> Posts { get; set; }
    }
}
