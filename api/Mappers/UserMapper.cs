using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.User;
using api.Interfaces;
using api.Models;

//Mapper is a class that maps the data from the database model to the response model, so that the response model can be sent to the client.
namespace api.Mappers
{
    // its an extension class, so it should be static, because it will not be instantiated, but its methods will be called directly on the class itself.
    public static class UserMapper
    {
        public static UserInView ToSingleUserInView(this User user) // response model to the user
        {
            return new UserInView
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                Institution = user.Institution,
                PhoneNumber = user.PhoneNumber,
                CreatedAt = user.CreatedAt,
                Post = user.Post.Select(p => p.ToPostView()).ToList(),
                UserId = user.Id
            };
        }

        public static UserInView ToUserInView(this User user) // response model to the user
        {
            return new UserInView
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                Institution = user.Institution,
                PhoneNumber = user.PhoneNumber,
                CreatedAt = user.CreatedAt,
                UserId = user.Id
            };
        }

        public static User ToUserInCreate(this UserInCreate user) // response model to the user
        {
            return new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                PasswordHash = user.Password,
                Institution = user.Institution,
                PhoneNumber = user.PhoneNumber
            };
        }

        public static User ToUserInUpdate(this UserInUpdate userInUpdate, User user) // response model to the user
        {
            //does not really do anything, but it is here for consistency and future use, in case we want to add more properties to the UserInUpdate model.
            user.FirstName = userInUpdate.FirstName;
            user.LastName = userInUpdate.LastName;
            user.UserName = userInUpdate.UserName;
            user.PhoneNumber = userInUpdate.PhoneNumber;
            return user;
        }

        public static UserToken ToUserToken(this User user, ITokenService tokenService)
        {
            return new UserToken
            {
                UserName = user.UserName,
                Email = user.Email,
                Id = user.Id,
                Token = tokenService.CreateToken(user)
            };
        }
    }
}