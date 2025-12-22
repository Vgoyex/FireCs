using Fire.Domain.Entities;
using Fire.Domain.Pagination;
using Fire.Infra.Data;
using Fire.Infra.Helpers;
using Microsoft.EntityFrameworkCore;
namespace Fire.Infra.Repositories.Posts
{
    public class PostsRepository : IPostsRepository
    {
        private readonly AppDbContext _appDbContext;
        public PostsRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        //chamando assim _appDbContext.Posts ja estou gerando uma query, logo não preciso do .AsQueryable()
        public async Task<PagedList<PostsEntity>> GetAllAsync(int pageNumber, int pageSize)
        {
            var query = _appDbContext.Posts.AsQueryable();
            return await PaginationHelper.CreateAsync(query, pageNumber, pageSize);
        }

        public async Task<PagedList<PostsEntity>> FindByUserId(Guid user_id, int pageNumber, int pageSize)
        {
            var query = _appDbContext.Posts.
                Where(x => x.user_id == user_id);

            return await PaginationHelper.CreateAsync(query, pageNumber, pageSize);
        }

        public async Task<PagedList<PostsEntity>> FindByUserEmail(string email, int pageNumber, int pageSize)
        {
            var query = _appDbContext.Posts
            .Join(
                _appDbContext.Users,
                post => post.user_id,
                user => user.id,
                (post, user) => new { Post = post, User = user }
            )
            .Where(x => x.User.email == email)
            .Select(x => x.Post);

            return await PaginationHelper.CreateAsync(query, pageNumber, pageSize);
        }

        public async Task<PagedList<PostsEntity>> FindByNickname(string nickname, int pageNumber, int pageSize)
        {
            var query = _appDbContext.Posts
            .Join(
                _appDbContext.Users,
                post => post.user_id,
                user => user.id,
                (post, user) => new { Post = post, User = user }
            )
            .Where(x => x.User.nickname == nickname)
            .Select(x => x.Post);

            return await PaginationHelper.CreateAsync(query, pageNumber, pageSize);
        }


        public async Task<PagedList<PostsEntity>> FindActivePostsByNickname(string nickname, int pageNumber, int pageSize)
        {
            var query = _appDbContext.Posts
            .Join(
                _appDbContext.Users,
                post => post.user_id,
                user => user.id,
                (post, user) => new { Post = post, User = user }
            )
            .Where(x => x.User.nickname == nickname && x.Post.active == true && x.User.active == true)
            .Select(x => x.Post);

            return await PaginationHelper.CreateAsync(query, pageNumber, pageSize);
        }

        //public async Task<PagedList<PostsEntity>> FindActivePostsWithActiveUserByNickname(string nickname, int pageNumber, int pageSize)
        //{
        //    var query = _appDbContext.Posts
        //    .Join(
        //        _appDbContext.Users,
        //        post => post.user_id,
        //        user => user.id,
        //        (post, user) => new { Post = post, User = user }
        //    )
        //    .Where(x => x.User.nickname == nickname && x.Post.active == true && x.User.active == true)
        //    .Select(x => x.Post);

        //    return await PaginationHelper.CreateAsync(query, pageNumber, pageSize);
        //}

        public async Task<PostsEntity> FindById(Guid id)
        {
            return await _appDbContext.Posts.FindAsync(id);
        }

        public async Task<PostsEntity> EditarPost(PostsEntity post)
        {
            _appDbContext.Posts.Update(post);
            await _appDbContext.SaveChangesAsync();
            return post;
        }


        public async Task<PostsEntity> SavePostAsync(PostsEntity post)
        {
            await _appDbContext.AddAsync(post);
            await _appDbContext.SaveChangesAsync();
            return post;
        }

        public async Task<string> DeletarPost(Guid id)
        {
            var post = await _appDbContext.Posts.FindAsync(id);
            if (post is not null)
            {
                _appDbContext.Remove(id);
                await _appDbContext.SaveChangesAsync();
                return $"Id: {id} deletado";
            }
            return $"Id: {id} não existe";
        }


        //Testar/Revisar o banco
        //public async Task<PagedList<PostsEntity>> GetHybridFeedAsyncNew(Guid userId, int pageNumber, int pageSize)
        //{
        //    var dateNow = DateTime.UtcNow;

        //    var query = _appDbContext.Posts
        //        .Where(p => p.active)
        //        .Select(p => new
        //        {
        //            PostsLikes = _appDbContext.PostsLikes
        //                .Count(pl => pl.post_id == p.id),

        //            RecencyScore = (dateNow - p.created_at).TotalHours,

        //            AffinityScore = _appDbContext.UserInteractions
        //                .Where(ui => ui.user_id == userId && ui.author_id == p.user_id)
        //                .Select(ui => ui.count)
        //                .FirstOrDefault()
        //        })
        //        .OrderByDescending(x =>
        //            (1.0 / (1 + x.RecencyScore)) * 0.5 +
        //            (x.PostsLikes * 0.3) +
        //            (x.AffinityScore * 0.2)
        //        );
        //    //.Skip((pageNumber - 1) * pageSize)
        //    //    .Take(pageSize).ToListAsync();
        //    return await PaginationHelper.CreateAsync(query, pageNumber, pageSize);
        //    //return await query.Select(x => x.Post).ToListAsync();
        //}


    }
}
