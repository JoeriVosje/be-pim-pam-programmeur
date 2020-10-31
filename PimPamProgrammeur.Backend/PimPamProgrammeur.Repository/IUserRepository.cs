using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PimPamProgrammeur.Model;

namespace PimPamProgrammeur.Repository
{
    public interface IUserRepository
    {
        Task<User> SaveUser(User user);
        Task DeleteUser(Guid id);
        User GetUser(Guid id);
        IEnumerable<User> GetUsers();
        User FindUser(string email, string password);

        User FindUser(string email);
    }
}