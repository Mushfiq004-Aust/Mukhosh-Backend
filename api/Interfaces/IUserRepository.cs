using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.User;
using api.Models;

//Interface for User Repository
//Here we define the contract for the User Repository
//specifying the methods that any implementation of this interface must provide.
//This allows for flexibility and easier testing, as we can swap out different implementations of IUserRepository
//without changing the code that depends on it.

namespace api.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsersAsync();
        //Method to retrieve all users from the repository. 
        //It returns a Task that resolves to a List of User objects, allowing for asynchronous operation.

        Task<User?> GetUserByIdAsync(Guid userId);
        //Method to retrieve a single user by their unique identifier (id). can return null if the user is not found, hence the nullable User type.

        Task<User> CreateUserAsync(UserInCreate user);
        //Method to create a new user in the repository.

        Task<User?> UpdateUserAsync(Guid userId, UserInUpdate user);
        //Method to update an existing user's information based on their unique identifier (id).

        Task<User?> DeleteUserAsync(Guid userId);
        //Method to delete a user from the repository based on their unique identifier (id).
    }
}