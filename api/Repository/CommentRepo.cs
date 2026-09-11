using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Database;
using api.DTOs.Comment;
using api.Helper;
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

        public async Task<List<Comment>> GetAllCommentsAsync(QueryObject query)
        {
            var Comments = _context.Comment.AsQueryable();

            Comments = query.OldestFirst ? Comments.OrderByDescending(p => p.CreatedAt) : Comments.OrderBy(p => p.CreatedAt);

            var SkipSize = (query.PageNumber - 1) * query.PageSize;
            return await Comments.Skip(SkipSize).Take(query.PageSize).ToListAsync();
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

            existingComment = comment.ToCommentUpdate(existingComment);

            await _context.SaveChangesAsync();
            return existingComment;
        }
    }
}