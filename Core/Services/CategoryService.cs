using Domain.Models;
using Infrasitructure.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Core.Services
{
    public class CategoryService
    {
        //private readonly IBaseRepository<Category> _catRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly Action<string> _logAction;

        public CategoryService(/*IBaseRepository<Category> catRepo*/ IUnitOfWork unitOfWork)
        {
            //_catRepo = catRepo;
            _unitOfWork = unitOfWork;
            _logAction = message => Console.WriteLine(message);
        }


        public async Task<IEnumerable<SelectListItem>> GetCategoriesWithSelectListItem()
        {
            var categories = await _unitOfWork._category.GetAll();
            return categories.Select(c => new SelectListItem {
                Value = c.Id.ToString(),
                Text = c.Name
            });
        }
        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _unitOfWork._category.GetAll();
        }
    }
}
// category   {id, name , description}
// in view make a select list
// text for user  => name
// value for Db => id
// class SelectListItem{
//{  text ;
// value;
// }

// new SelectListItem (value: id,text: name)


// post       {id, title, content, categoryId}