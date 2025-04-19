using Domain.Models;
using Infrasitructure.Data;
using Infrasitructure.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrasitructure.Repository
{
    public class PostRepo : IPostRepo
    {
        private readonly AppDbContext _context;
        public PostRepo(AppDbContext context)
        {
            _context = context;
        }
        public void AddPost(Post post)
        {
            throw new NotImplementedException();
        }

        public void DeletePost(int id)
        {
            throw new NotImplementedException();
        }

        public List<Post> GetAllPosts()
        {
            return _context.Posts.Include(p => p.Category).ToList();
        }

        public Post GetPostById(int id)
        {
            var post = _context.Posts.Find(id);

            if (post != null)
            {
                _context.Entry(post).Reference(p => p.Category).Load(); // object
                _context.Entry(post).Collection(p => p.Comments).Load(); // collection , list
            }
            return post;
        }

        public void UpdatePost(Post post)
        {
            throw new NotImplementedException();
        }
    }
}
