using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Post;
using api.Models;

namespace api.Mappers
{
    public static class PostMapper
    {
        public static PostInView ToSinglePostView(this Post post)
        {
            return new PostInView
            {
                Title = post.Title,
                Content = post.Content,
                CreatedAt = post.CreatedAt,
                Vibe = post.Vibe,
                Comments = post.Comment.Select(c => c.ToCommentView()).ToList(),
                PostId = post.PostId
            };
        }

        public static PostInView ToPostView(this Post post)
        {
            return new PostInView
            {
                Title = post.Title,
                Content = post.Content,
                CreatedAt = post.CreatedAt,
                Vibe = post.Vibe,
                PostId = post.PostId
            };
        }

        public static Post ToPostCreate(this PostInCreate postInCreate)
        {
            return new Post
            {
                Title = postInCreate.Title,
                Content = postInCreate.Content,
                Vibe = postInCreate.Vibe,
                UserId = postInCreate.UserId
            };
        }

        public static Post ToPostUpdate(this PostInUpdate postInUpdate, Post post)
        {
            post.Title = postInUpdate.Title;
            post.Content = postInUpdate.Content;
            post.Vibe = postInUpdate.Vibe;
            return post;
        }
    }
}