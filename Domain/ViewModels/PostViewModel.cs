using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Domain.ViewModels
{
    public class PostViewModel
    {
        [Display(Name = "Post Image")]
        public IFormFile File { get; set; }
        public Post Post { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> Categories { get; set; } // value , text

    }
}
