using Core.Services;
using Domain.Models;
using Domain.ViewModels;
using Infrasitructure.Helpers;
using Infrasitructure.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class PostsController : Controller
    {
        private readonly PostService _postService;
        private readonly CategoryService _catService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly string[] _allowedExtentions = { ".jpg", ".png", ".jpeg" };
        private readonly IWebHostEnvironment _webHost;
        public PostsController(PostService postService,CategoryService categoryService,IWebHostEnvironment webHost,IUnitOfWork unitOfWork)
        {
            _postService = postService;
            _catService = categoryService;
            _webHost = webHost;
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int? categoryId) // get all
        {

            ViewBag.Categories = await _catService.GetCategories();
            if (categoryId.HasValue)
            {
                // get posts by category  
                // where Post.CategoryId == categoryId
                // include category


                var postsData = await _postService.GetAllPosts(
                    // where
                    p => p.CategoryId == categoryId
                    ,
                    // include
                    new System.Linq.Expressions.Expression<Func<Post, object>>[]
                    {
                        p => p.Category
                    }
                    );

                return View(postsData.ToList());
            }




            var posts = await _postService.GetAllPosts();
            //return View(); // controller : Posts =>  view : index
            //return View("GetAll"); // controller : Posts =>  view : GetAll
            return View(posts.ToList()); // controller : Posts =>  view : index   var : posts
            //return View("getAll",posts);// controller : Posts => view : getAll var : posts
        }
        public IActionResult Details (int id) // get by id
        {
            var post = _postService.GetPostById(id);
            return View(post);
        }




        [HttpGet]
        public async Task<IActionResult> Create() // add
        {
            var postViewModel = new PostViewModel();
            postViewModel.Categories = await _catService.GetCategoriesWithSelectListItem();
            return View(postViewModel);
        }

        [HttpPost] // get data from user
        public async Task<IActionResult> Create(/*Post post*/ PostViewModel postViewModel) // add
        {
            if (ModelState.IsValid)
            {
                // extention validation 
                var fileExtention = Path.GetExtension(postViewModel.File.FileName).ToLower();
                bool isAllowed = _allowedExtentions.Contains(fileExtention);
                if (!isAllowed)
                {
                    ModelState.AddModelError("File", "File extention is not allowed");
                    postViewModel.Categories = await _catService.GetCategoriesWithSelectListItem();
                    return View(postViewModel);
                }
                // upload file
                postViewModel.Post.Image = await UploadFiles.UploadFile(postViewModel.File,"Posts",_webHost.WebRootPath);
                _postService.AddPost(postViewModel.Post);
                return RedirectToAction(nameof(Index));
            }
            postViewModel.Categories = await _catService.GetCategoriesWithSelectListItem();
            return View(postViewModel);
        }

        public IActionResult Edit(int id)
        {
            var post = _postService.GetPostById(id);
            return View(post);
        }
        [HttpPost]
        public IActionResult Edit(Post post)
        {
            if (!ModelState.IsValid)
            {
                return View(post);
            }
            _postService.UpdatePost(post);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> EditWithTransaction(Post post)
        {
            // using transaction
            // 1. begin transaction
            await _unitOfWork.BeginTransactionAsync();
            // 2. update post
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(post);
                }
                _postService.UpdatePost(post);
                await _unitOfWork.CompleteAsync();
                // 3. commit transaction
                await _unitOfWork.CommitTransactionAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // rollback transaction
                await _unitOfWork.RollbackTransactionAsync(); // reset data
                Console.WriteLine(ex.Message);
                throw;
            }






        }
        public IActionResult Delete(int id)
        {
            var post = _postService.GetPostById(id);
            return View(post);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeletePost(int id)
        {
            if (ModelState.IsValid)
            {
                // get item 
                var PostData = _postService.GetPostById(id);
                // delete image
                string filePath = Path.Combine(_webHost.WebRootPath, PostData.Image);
                if(System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                _postService.DeletePost(id);
                return RedirectToAction(nameof(Index));
            }
            var post = _postService.GetPostById(id);
            return View(post);

        }

    }
}
