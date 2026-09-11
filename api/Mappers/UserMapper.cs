using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.User;
using api.Models;

//Mapper is a class that maps the data from the database model to the response model, so that the response model can be sent to the client.
namespace api.Mappers
{
    // its an extension class, so it should be static, because it will not be instantiated, but its methods will be called directly on the class itself.
    public static class UserMapper
    {
        public static UserInView ToUserInView(this User user) // response model to the user
        {
            return new UserInView
            {
                Name = user.Name,
                Email = user.Email,
                Institution = user.Institution,
                Phone = user.Phone,
                CreatedAt = user.CreatedAt,
                Post = user.Post.Select(p => p.ToPostView()).ToList(),
                UserId = user.UserId
            };
        }

        public static User ToUserInCreate(this UserInCreate user) // response model to the user
        {
            return new User
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                Institution = user.Institution,
                Phone = user.Phone
            };
        }

        public static User ToUserInUpdate(this UserInUpdate userInUpdate, User user) // response model to the user
        {
            //does not really do anything, but it is here for consistency and future use, in case we want to add more properties to the UserInUpdate model.
            user.Name = userInUpdate.Name;
            user.Phone = userInUpdate.Phone;
            return user;
        }
    }
}