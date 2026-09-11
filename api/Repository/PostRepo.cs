using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Database;
using api.DTOs.Post;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class PostRepo : IPostRepository
    {
        private readonly ApplicationDBContext _context;

        public PostRepo(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Post> CreatePostAsync(PostInCreate post)
        {
            var newPost = post.ToPostCreate(); //map the request body to the Post model using the mapper class
            await _context.Post.AddAsync(newPost);
            await _context.SaveChangesAsync();
            return newPost;
        }

        public async Task<Post?> DeletePostAsync(Guid postId)
        {
            var post = await _context.Post.FindAsync(postId);
            if (post == null)
            {
                return null;
            }

            _context.Post.Remove(post);
            await _context.SaveChangesAsync();
            return post;
        }

        public async Task<List<Post>> GetAllPostsAsync()
        {
            return await _context.Post.ToListAsync();
        }

        public async Task<Post?> GetPostByIdAsync(Guid postId)
        {
            return await _context.Post.FindAsync(postId);
        }

        public async Task<Post?> UpdatePostAsync(Guid postId, PostInUpdate post)
        {
            var existingPost = await _context.Post.FindAsync(postId);
            if (existingPost == null)
            {
                return null;
            }

            existingPost = post.ToPostUpdate(existingPost); //map the request body to the existing Post model using the mapper class
            await _context.SaveChangesAsync();
            return existingPost;
        }
    }
}