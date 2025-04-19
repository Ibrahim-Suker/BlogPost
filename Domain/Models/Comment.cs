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
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "the name is required")]
        [MaxLength(200, ErrorMessage = "Max Length is 200")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "the content is required")]
        [MaxLength(300, ErrorMessage = "Max Length is 300")]
        public string Content { get; set; }
        [DataType(DataType.Date)]
        public DateTime CommentDate { get; set; } = DateTime.Now;
        [ForeignKey("Post")]
        public int PostId { get; set; }
        [ValidateNever]
        public virtual Post Post { get; set; }
    }
}
