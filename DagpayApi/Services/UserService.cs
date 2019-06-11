using System;
using System.Linq;
using DagpayApi.Models;
using DagpayApi.ViewModels;

namespace DagpayApi.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        User Create(UserViewModel userViewModel);
        User GetById(int id);
    }

    public class UserService : IUserService
    {
        private AzureDatabaseContext _context;

        public UserService(AzureDatabaseContext context)
        {
            _context = context;
        }

        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrWhiteSpace(password))
            {
                return null;
            }

            Console.WriteLine("*** username/password passed null/empty/whitespace validation");

            var user = _context.Users.SingleOrDefault(u => u.Username == username);

            if (user == null)
            {
                return null;
            }

            Console.WriteLine("*** user retrieved: username " + user.Username + ", password " + System.Text.Encoding.UTF8.GetString(user.PasswordHash));

            if (!VerifyHashedPassword(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }

            return user;
        }

        public User Create(UserViewModel userViewModel)
        {
            if (userViewModel.Password == null)
            {
                throw new ArgumentNullException("password");
            }
            if (string.IsNullOrWhiteSpace(userViewModel.Password))
            {
                throw new ArgumentException("Invalid/empty/whitespace-only value.", "password");
            }

            CreateHashedPassword(userViewModel.Password, out byte[] hash, out byte[] salt);

            User user = new User
            {
                Username = userViewModel.Username,
                PasswordHash = hash,
                PasswordSalt = salt
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public User GetById(int id)
        {
            return _context.Users.Find(id);
        }

        private static void CreateHashedPassword(string password, out byte[] hash, out byte[] salt)
        {
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Invalid/empty/whitespace-only value.", "password");
            }

            using (var hashedMessageAuthenticationCode = new System.Security.Cryptography.HMACSHA512())
            {
                salt = hashedMessageAuthenticationCode.Key;
                hash = hashedMessageAuthenticationCode.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyHashedPassword(string password, byte[] hash, byte[] salt)
        {
            // password hash verification courtesy of https://jasonwatmore.com/post/2018/06/26/aspnet-core-21-simple-api-for-authentication-registration-and-user-management#user-service-cs
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Invalid/empty/whitespace-only value.", "password");
            }
            if (hash.Length != 64)
            {
                throw new ArgumentException("Password hash length is invalid (expecting 64 bytes).", "passwordHash");
            }
            if (salt.Length != 128)
            {
                throw new ArgumentException("Password salt length is invalid (expecting 128 bytes).", "passwordSalt");
            }

            using (var hashedMessageAuthenticationCode = new System.Security.Cryptography.HMACSHA512(salt))
            {
                var computedHash = hashedMessageAuthenticationCode.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                Console.WriteLine("*** computedHash: " + computedHash);

                for (int i = 0; i <computedHash.Length; i++)
                {
                    if (computedHash[i] != hash[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
