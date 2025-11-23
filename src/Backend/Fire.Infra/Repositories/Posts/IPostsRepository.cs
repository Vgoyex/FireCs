using Fire.Domain.Entities;
using Fire.Domain.Pagination;

namespace Fire.Infra.Repositories.Posts
{
    public interface IPostsRepository
    {
        Task<PagedList<PostsEntity>> GetAllAsync(int pageNumber, int pageSize);
        Task<PagedList<PostsEntity>> FindByUserId(Guid user_id, int pageNumber, int pageSize);
        Task<PagedList<PostsEntity>> FindByUserEmail(string email, int pageNumber, int pageSize);
        Task<PagedList<PostsEntity>> FindByNickname(string nickname, int pageNumber, int pageSize);
        Task<PagedList<PostsEntity>> FindActivePostsByNickname(string nickname, int pageNumber, int pageSize);
        Task<PostsEntity> FindById(Guid id);
        Task<PostsEntity> EditarPost(PostsEntity post);
        Task<string> DeletarPost(Guid id);
        Task<PostsEntity> SavePostAsync(PostsEntity post);
    }
}
