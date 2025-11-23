using Fire.Domain.Entities;
using Fire.Domain.Pagination;
using Fire.Infra.Repositories.Posts;

namespace Fire.Application.Services
{
    public class PostsService
    {
        private readonly IPostsRepository _postsRepository;

        public PostsService(IPostsRepository postsRepository) {
            _postsRepository = postsRepository;
        }

        public async Task<PagedList<PostsEntity>> FindAllPosts(int pageNumber, int pageSize)
        {
            var posts =  await _postsRepository.GetAllAsync(pageNumber, pageSize);
            return new PagedList<PostsEntity>(posts, pageNumber, pageSize, posts.totalCount);
        }

        public async Task<PagedList<PostsEntity>> FindByUserId(Guid user_id, int pageNumber, int pageSize)
        {
            var posts = await _postsRepository.FindByUserId(user_id, pageNumber, pageSize);
            return new PagedList<PostsEntity>(posts, pageNumber, pageSize, posts.totalCount);
        }

        public async Task<PostsEntity> FindById(Guid id)
        {
            return await _postsRepository.FindById(id);
        }

        public async Task<PostsEntity> EditarPost(PostsEntity post)
        {
            await _postsRepository.EditarPost(post);
            return post;
        }

        public async Task<string> DeletarPost(Guid id)
        {
            return await _postsRepository.DeletarPost(id);
        }

        public async Task<PostsEntity> DeleteLogico(Guid id)
        {
            var post = await FindById(id);
            if(post is not null)
            {
                post.active = false;
                return await _postsRepository.EditarPost(post);
            }
            return null;
        }

        public async Task<PostsEntity> AtivarPost(Guid id)
        {
            var post = await FindById(id);
            if(post is not null)
            {
                post.active = true; 
                return await _postsRepository.EditarPost(post);
            }
            return null;
        }

        public async Task<PostsEntity> SavePostAsync(PostsEntity post)
        {
            return await _postsRepository.SavePostAsync(post);
        }

        public async Task<PagedList<PostsEntity>> FindByNickname(string nickname, int pageNumber, int pageSize)
        {
            var posts =  await _postsRepository.FindByNickname(nickname, pageNumber, pageSize);
            return new PagedList<PostsEntity>(posts, pageNumber, pageSize, posts.totalCount);
        }

        public async Task<PagedList<PostsEntity>> FindActivePostsByNickname(string nickname, int pageNumber, int pageSize)
        {
            var posts = await _postsRepository.FindActivePostsByNickname(nickname, pageNumber, pageSize);
            return new PagedList<PostsEntity>(posts, pageNumber, pageSize, posts.totalCount);
        }
    }

    }
