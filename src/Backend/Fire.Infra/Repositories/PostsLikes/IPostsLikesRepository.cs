using Fire.Domain.Entities;
using Fire.Domain.Pagination;

namespace Fire.Infra.Repositories.PostsLikes
{
    public interface IPostsLikesRepository
    {
        Task<PagedList<PostsLikesEntity>> FindByUserId(Guid user_id, int pageNumber, int pageSize);
    }
}
