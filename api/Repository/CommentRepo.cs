using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Database;
using api.DTOs.Comment;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CommentRepo : ICommentRepository
    {
        private readonly ApplicationDBContext _context;
        public CommentRepo(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Comment>> GetAllCommentsAsync()
        {
            return await _context.Comment.ToListAsync();
        }

        public async Task<Comment?> GetCommentByIdAsync(Guid commentId)
        {
            return await _context.Comment.FindAsync(commentId);
        }

        public async Task<Comment> CreateCommentAsync(CommentInCreate comment)
        {
            var newComment = comment.ToCommentCreate();//map the request body to the Comment model using the mapper class
            await _context.Comment.AddAsync(newComment);
            await _context.SaveChangesAsync();
            return newComment;
        }

        public async Task<Comment?> DeleteCommentAsync(Guid commentId)
        {
            var comment = await _context.Comment.FindAsync(commentId);
            if (comment == null)
            {
                return null;
            }

            _context.Comment.Remove(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment?> UpdateCommentAsync(Guid commentId, CommentInUpdate comment)
        {
            var existingComment = await _context.Comment.FindAsync(commentId);
            if (existingComment == null)
            {
                return null;
            }

            existingComment.Content = comment.Content;

            await _context.SaveChangesAsync();
            return existingComment;
        }
    }
}