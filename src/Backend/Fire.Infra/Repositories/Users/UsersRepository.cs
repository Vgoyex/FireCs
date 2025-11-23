using Microsoft.EntityFrameworkCore;
using Fire.Infra.Data;
using Fire.Domain.Entities;
using Fire.Domain.Pagination;
using Fire.Infra.Helpers;
namespace Fire.Infra.Repositories.Users
{
    public class UsersRepository : IUsersRepository
    {
        
        private readonly AppDbContext _appDbContext;
        public UsersRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<PagedList<UsersEntity>> GetAllAsync(int pageNumber, int pageSize)
        {
            var query =  _appDbContext.Users;
            return await PaginationHelper.CreateAsync(query, pageNumber, pageSize);
        }


        public async Task<UsersEntity> FindByEmailAsync(string email)
        {
            return await _appDbContext.Users.FirstOrDefaultAsync(u => u.email == email);
        }

        public async Task<UsersEntity> SaveUserAsync(UsersEntity user)
        {
            await _appDbContext.Users.AddAsync(user);
            await _appDbContext.SaveChangesAsync();
            return user;
        }

        

        public async Task<UsersEntity> EditarUsuario(UsersEntity user)
        {
            _appDbContext.Users.Update(user);
            await _appDbContext.SaveChangesAsync();
            return user;
        }

        public async Task<UsersEntity> GetById(Guid id)
        {
            var user = await _appDbContext.Users.FindAsync(id);
            if (user is not null) {
                return user;
            }
            return null;
        }

        public async Task<string> DeleteUserById(Guid id)
        {
            var user = await _appDbContext.Users.FindAsync(id);
            if (user is not null)
            {
                 _appDbContext.Users.Remove(user);
                await _appDbContext.SaveChangesAsync();
                return  $"Id: {id} deletado";
            }
            return $"Id: {id} não existe";
        }
    }
}
