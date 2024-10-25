using BusinessObjects;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Repositories.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ILibraryRepository _libraryRepository;

        public UserRepository(ILibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await UserDAO.Instance.GetAllUsers();
        }

        public async Task<User> Authenticate(string username, string password)
        {
            return await UserDAO.Instance.Authenticate(username, password);
        }

        public async Task<User> GetUserById(Guid userId)
        {
            return await UserDAO.Instance.GetUserById(userId);
        }

        public async Task<Guid> GenerateUniqueUserIdAsync()
        {
            Guid userId;
            User exists;
            do
            {
                userId = Guid.NewGuid();
                exists = await UserDAO.Instance.GetUserById(userId);
            } while (exists != null);
            return userId;
        }

        public async Task AddUser(User user)
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    user.Id = await GenerateUniqueUserIdAsync();

                    // Add User to repository
                    await UserDAO.Instance.Add(user);
                    Console.WriteLine("User added: " + user.Id);

                    //// Create Library with a different unique ID
                    //var library = new Library
                    //{
                    //    Id = Guid.NewGuid(), // Generate a new GUID for Library
                    //    User = user // Link Library to User
                    //};

                    //// Add Library to repository
                    //await _libraryRepository.AddLibrary(library);
                    //Console.WriteLine("Library added: " + library.Id);

                    // Complete the transaction
                    transaction.Complete();
                }
                catch (Exception ex)
                {
                    // Log the error (logging mechanism not shown here)
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    throw new Exception("An error occurred while adding the user and library.", ex);
                }
            }
        }

        public async Task UpdateUser(User user)
        {
            await UserDAO.Instance.Update(user);
        }

        public async Task DeleteUser(Guid userId)
        {
            await UserDAO.Instance.Delete(userId);
        }

        public async Task<User> FindByNameAsync(string username)
        {
            return await UserDAO.Instance.FindByNameAsync(username);
        }
    }
}