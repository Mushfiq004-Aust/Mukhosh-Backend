using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Comment;
using api.Helper;
using api.Models;

//Interface for Comment Repository
//Here we define the contract for the Comment Repository
//specifying the methods that any implementation of this interface must provide.
//This allows for flexibility and easier testing, as we can swap out different implementations of ICommentRepository
//without changing the code that depends on it.

namespace api.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllCommentsAsync(QueryObject query);
        Task<Comment?> GetCommentByIdAsync(Guid commentId);
        Task<Comment> CreateCommentAsync(CommentInCreate comment);
        Task<Comment?> UpdateCommentAsync(Guid commentId, CommentInUpdate comment);
        Task<Comment?> DeleteCommentAsync(Guid commentId);
    }
}