using Domain.Models;
using Infrasitructure.IRepository;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Services
{
    public class PostService
    {
        //private readonly IBaseRepository<Post> _postRepo;
        //private readonly IPostRepo _post;
        private readonly IUnitOfWork _unitOfWork;

        private readonly Action<string> _logAction;

        public PostService(/*IBaseRepository<Post> postRepo,IPostRepo post*/ IUnitOfWork unitOfWork)
        {
            //_postRepo = postRepo;
            //_post = post;
            _unitOfWork = unitOfWork;
            _logAction = message => Console.WriteLine(message);
        }
        public async Task<List<Post>> GetAllPosts()
        {
            //return await _postRepo.GetAll();
            return _unitOfWork._postRepo.GetAllPosts();
        }
        public async Task<List<Post>> GetAllPosts(
            Expression<Func<Post,bool>> criteria = null, // where
            Expression<Func<Post, object>>[] includes = null // include
            )
        {
            //return _post.GetAllPosts();
            return await _unitOfWork._post.GetAllAsync(criteria,includes);
        }

        public Post GetPostById(int id)
        {
            return _unitOfWork._post.GetById(id);
        }
        public void AddPost(Post post)
        {
            _unitOfWork._post.Add(post, _logAction);
        }
        public void UpdatePost(Post post)
        {
            _unitOfWork._post.Update(post, _logAction);
        }
        public void DeletePost(int id)
        {
            _unitOfWork._post.Delete(id, _logAction);

        }
    }
}
