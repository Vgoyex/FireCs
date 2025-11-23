using Fire.Domain.Entities;
using Fire.Domain.Pagination;

namespace Fire.Infra.Repositories.Users
{
    public interface IUsersRepository
    {
        Task<UsersEntity> FindByEmailAsync(string email);
        Task<UsersEntity> SaveUserAsync(UsersEntity user);
        Task<PagedList<UsersEntity>> GetAllAsync(int pageNumber, int pageSize);
        Task<UsersEntity> EditarUsuario(UsersEntity user);
        Task<UsersEntity> GetById(Guid id);
        Task<string> DeleteUserById(Guid id);
    }
}
