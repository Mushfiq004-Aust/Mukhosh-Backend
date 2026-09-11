using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Comment;
using api.Models;

namespace api.Mappers
{
    public static class CommentMapper
    {
        public static CommentInView ToCommentView(this Comment comment)
        {
            return new CommentInView
            {
                Content = comment.Content,
                CreatedAt = comment.CreatedAt,
                CommentId = comment.CommentId
            };
        }

        public static Comment ToCommentCreate(this CommentInCreate commentInCreate)
        {
            return new Comment
            {
                Content = commentInCreate.Content,
                PostId = commentInCreate.PostId
            };
        }

        public static Comment ToCommentUpdate(this CommentInUpdate commentInUpdate, Comment comment)
        {
            comment.Content = commentInUpdate.Content;
            return comment;
        }
    }
}