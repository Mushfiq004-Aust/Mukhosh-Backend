using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Post;
using api.Helper;
using api.Models;

namespace api.Interfaces
{
    public interface IPostRepository
    {
        Task<List<Post>> GetAllPostsAsync(QueryObject query);
        Task<Post?> GetPostByIdAsync(Guid postId);
        Task<Post> CreatePostAsync(PostInCreate post);
        Task<Post?> UpdatePostAsync(Guid postId, PostInUpdate post);
        Task<Post?> DeletePostAsync(Guid postId);
    }
}