using Fire.Domain.Entities;
using Fire.Domain.Pagination;
using Fire.Infra.Data;
using Fire.Infra.Helpers;

namespace Fire.Infra.Repositories.PostsLikes
{
    public class PostsLikesRepository : IPostsLikesRepository
    {
        private readonly AppDbContext _appDbContext;
        public PostsLikesRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<PagedList<PostsLikesEntity>> FindByUserId(Guid user_id, int pageNumber, int pageSize)
        {
            var query = _appDbContext.PostsLikes.
                Where(x => x.user_id == user_id);

            return await PaginationHelper.CreateAsync(query, pageNumber, pageSize);
        }
    }
}
