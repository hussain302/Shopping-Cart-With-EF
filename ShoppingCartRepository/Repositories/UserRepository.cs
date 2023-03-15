using Microsoft.EntityFrameworkCore;
using ShoppingCartDataAccessLayer.ShoppingCartContext;
using ShoppingCartInterfaces.IRepositories;
using ShoppingCartModels.DbModels;

namespace ShoppingCartRepository.Repositories
{
    public class UserRepository : BaseRepository<AdminUser>, IUserRepository
    {
        private ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<AdminUser> CreateUser(AdminUser adminUser)
        {
            try
            {
                var findUser = await _context.AdminUsers
                    .Where(x => x.Username == adminUser.Username)
                    .FirstOrDefaultAsync();

                if (findUser == null)
                {
                    string hashedPassword = PasswordHasher.HashPassword(adminUser.Password);
                    adminUser.Password = hashedPassword;
                    adminUser.CreatedBy = adminUser.FullName;
                    adminUser.CreatedOn = DateTime.Now;
                    adminUser.IsApproved = false;
                    adminUser.IsDeleted = false;
                    adminUser.Role = (string.IsNullOrWhiteSpace(adminUser.Role)) ? "Admin" : adminUser.Role;
                    await _context.AddAsync(adminUser);
                    await _context.SaveChangesAsync();
                    return null;
                }
                
                return findUser;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<AdminUser> Login(string username, string password)
        {
            try
            {
                bool isMatch = false;
                var user = await _context.AdminUsers
                    .Where(x => x.Username == username)
                    .FirstOrDefaultAsync();
                
                if(user == null) { return null; }

                isMatch = PasswordHasher.VerifyPassword(password, user.Password);
                
                if (isMatch)
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<AdminUser> Validate(AdminUser adminUser)
        {
            try
            {
                var user = await _context.AdminUsers
                    .Where(x => x.Username == adminUser.Username && x.Password == adminUser.Password)
                    .FirstOrDefaultAsync();

                if (user == null) return null;

                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
