using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [Required (ErrorMessage="the title is required")]
        [MaxLength(100,ErrorMessage ="maxlength is 100"),MinLength(2, ErrorMessage = "minlength is 2")]
        public string Title { get; set; }
        [MinLength(10, ErrorMessage = "minlength is 10")]
        public string Content { get; set; }
        [ValidateNever]
        public string Image { get; set; } // IFormFile => view
        public DateTime CreatedAt { get; set; }= DateTime.Now;
        [ForeignKey("Category")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        [ValidateNever]
        public virtual Category Category { get; set; }
        [ValidateNever]
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
