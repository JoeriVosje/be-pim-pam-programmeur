using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PimPamProgrammeur.Data;
using PimPamProgrammeur.Model;

namespace PimPamProgrammeur.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly PimPamProgrammeurContext _context;

        public UserRepository(PimPamProgrammeurContext context)
        {
            _context = context;
        }

        public async Task<User> SaveUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task DeleteUser(Guid id)
        {
            var user = GetUser(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public User GetUser(Guid id)
        {
            return _context.Users.Include(x => x.ClassRoom).FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<User> GetUsers()
        {
            return _context.Users.Include(x => x.ClassRoom).ToList();
        }

        public User FindUser(string email, string password)
        {
            return _context.Users.FirstOrDefault(e => e.Email == email && e.Password == password);
        }

        public User FindUser(string email)
        {
            return _context.Users.FirstOrDefault(e => e.Email == email);
        }

        public IEnumerable<User> GetUserByClassroomId(Guid ClassroomId)
        {
            return _context.Users.Where(e => e.ClassroomId == ClassroomId);
        }
    }
}
