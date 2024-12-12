using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    using BusinessObjects;
    using global::Service.IServices;
    using Microsoft.Extensions.Logging;
    using Repositories.UnitOfWork;
    using System;
    using System.Threading.Tasks;
    using System.Transactions;

    namespace Service.Services
    {
        public class UserService : IUserService
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ILogger<UserService> _logger;

            public UserService(IUnitOfWork unitOfWork, ILogger<UserService> logger)
            {
                _unitOfWork = unitOfWork;
                _logger = logger;
            }

            public async Task<User> Authenticate(string username, string password)
            {
                try
                {
                    var user = await _unitOfWork.Users.Authenticate(username, password);
                    if (user == null)
                    {
                        _logger.LogWarning($"Authentication failed for username: {username}");
                        return null;
                    }
                    return user;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error authenticating user: {username}");
                    throw;
                }
            }

            public async Task<User> FindByNameAsync(string username)
            {
                try
                {
                    var user = await _unitOfWork.Users.FindByNameAsync(username);
                    if (user == null)
                    {
                        _logger.LogWarning($"User not found with username: {username}");
                    }
                    return user;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error finding user by username: {username}");
                    throw;
                }
            }

            public async Task<Guid> GenerateUniqueUserIdAsync()
            {
                try
                {
                    return await _unitOfWork.Users.GenerateUniqueUserIdAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error generating unique user ID");
                    throw;
                }
            }

            public async Task<User> FindByEmailAsync(string email)
            {
                try
                {
                    var user = await _unitOfWork.Users.FindByEmailAsync(email);
                    if (user == null)
                    {
                        _logger.LogWarning($"User not found with email: {email}");
                    }
                    return user;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error finding user by email: {email}");
                    throw;
                }
            }
            public async Task AddUser(User user)
            {
                using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    try
                    {
                        await _unitOfWork.Users.Add(user);
                        Console.WriteLine("User added: " + user.Id);

                        var library = new Library
                        {
                            Id = Guid.NewGuid(),
                            UserId = user.Id
                        };

                        await _unitOfWork.Libraries.Add(library);
                        Console.WriteLine("Library added: " + library.Id);
                        await _unitOfWork.Complete();


                        transaction.Complete();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred: {ex.Message}");
                        throw new Exception("An error occurred while adding the user and library.", ex);
                    }
                }
            }

            public async Task AddUserGoogle(User user)
            {
                try
                {
                    await AddUser(user);
                    _logger.LogInformation($"Google user added successfully: {user.Id}");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error adding Google user: {user.Email}");
                    throw;
                }
            }

            public async Task UpdatePassword(Guid userId, string newPassword)
            {
                try
                {
                    await _unitOfWork.Users.UpdatePassword(userId, newPassword);
                    await _unitOfWork.Complete();
                    _logger.LogInformation($"Password updated successfully for user: {userId}");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error updating password for user: {userId}");
                    throw;
                }
            }

            public async Task<int> TotalUser()
            {
                try
                {
                    return await _unitOfWork.Users.TotalUser();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error getting total user count");
                    throw;
                }
            }
        }
    }
}
