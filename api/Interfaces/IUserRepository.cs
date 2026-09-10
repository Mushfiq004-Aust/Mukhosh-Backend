using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

//Interface for User Repository
//Here we define the contract for the User Repository
//specifying the methods that any implementation of this interface must provide.
//This allows for flexibility and easier testing, as we can swap out different implementations of IUserRepository
//without changing the code that depends on it.

namespace api
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsers();
        //Method to retrieve all users from the repository. 
        //It returns a Task that resolves to a List of User objects, allowing for asynchronous operation.
    }
}