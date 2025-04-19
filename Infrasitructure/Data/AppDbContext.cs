using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrasitructure.Data
{
    // DbContext 
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1,Name="tech"},
                new Category { Id = 2,Name="test"}
                );


            // Posts
            modelBuilder.Entity<Post>().HasData(
                new Post
                {
                    Id = 1,
                    Title="title1",
                    CategoryId=1,
                    Content ="post content",
                    CreatedAt = new DateTime(2024,8,5 ),
                    Image ="x.png"
                },
                new Post
                {
                    Id = 2,
                    Title = "title1",
                    CategoryId = 2,
                    Content = "post content",
                    CreatedAt = new DateTime(2024,8,5),
                    Image = "x.png"
                }
                );

            // Comments
            modelBuilder.Entity<Comment>().HasData(
                new Comment
                {
                    Id = 1,
                    Content ="comment1",
                    CommentDate = new DateTime(2024,8,5),
                    UserName = "reem",
                    PostId = 1,
                },
                new Comment
                {
                    Id = 2,
                    Content = "comment1",
                    CommentDate = new DateTime(2024,8,5 ),
                    UserName = "reem",
                    PostId = 1,
                }, new Comment
                {
                    Id = 3,
                    Content = "comment1",
                    CommentDate = new DateTime(2024,8,5 ),
                    UserName = "reem",
                    PostId = 1,
                }, new Comment
                {
                    Id = 4,
                    Content = "comment1",
                    CommentDate = new DateTime(2024,8,5 ),
                    UserName = "reem",
                    PostId = 2,
                }
                );
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //optionsBuilder.UseLazyLoadingProxies();
        }
        
    }
}
