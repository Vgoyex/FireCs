using Fire.API.Extensions;
using Fire.API.Params;
using Fire.Application.Services;
using Fire.Communication.Requests;
using Fire.Domain.Entities;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Fire.API.Controllers
{
    [ApiController]
    [Route("posts")]
    public class PostsController : ControllerBase
    {
        private readonly PostsService _postsService;
        private readonly UsersService _usersService;
        private readonly ContentService _contentService;
        public PostsController(
            PostsService postsService,
            UsersService usersService,
            ContentService contentService
            )
        {
            _postsService = postsService;
            _usersService = usersService;
            _contentService = contentService;
        }

        [HttpGet("listar_posts")]
        public async Task<IActionResult> ListarPosts([FromQuery] PaginationParams paginationParams) {
            var posts = await _postsService.FindAllPosts(paginationParams.pageNumber, paginationParams.pageSize);
            Response.AddPaginationHeader(
                new PaginationHeader(posts.pageNumber, posts.pageSize, posts.totalCount, posts.totalPages)
                );
            if (posts is not null)
                return Ok(posts);

            return BadRequest();
        }

        [HttpGet("/user_id/{user_id}")]
        public async Task<IActionResult> GetByUserId(
            [FromQuery] PaginationParams paginationParams,
            Guid user_id)
        {
            var posts = await _postsService.FindByUserId(user_id,paginationParams.pageNumber, paginationParams.pageSize);
            Response.AddPaginationHeader(
               new PaginationHeader(posts.pageNumber, posts.pageSize, posts.totalCount, posts.totalPages)
            );
            if (posts is not null)
                return Ok(posts);
            return BadRequest();
        }

       //Testar o algoritmo
       [HttpGet("home")]
        public async Task<IActionResult> Home([FromQuery] PaginationParams paginationParams, Guid userId)
        {
            var posts = await _postsService.FindAllPosts(paginationParams.pageNumber, paginationParams.pageSize);
            Response.AddPaginationHeader(new PaginationHeader
            (posts.pageNumber, posts.pageSize, posts.totalCount, posts.totalPages));
            //var userId = GetUserId(); // pegue do JWT ou auth

            //var posts = await _postsService.GetHybridFeedAsync(
            //    userId,
            //    paginationParams.pageNumber,
            //    paginationParams.pageSize
            //);

            return Ok(posts);
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetPostById( Guid id)
        {
            var post = await _postsService.FindById(id);
            if (post is not null)
                return Ok(post);

            return BadRequest();
        }

        //[HttpPost("registrar_post")]
        //public async Task<IActionResult> RegistrarPost([FromBody] RegistrarPostRequest body)
        //{
        //    var post = body.Adapt<PostsEntity>();
        //    var validUsuario = await _usersService.GetById(body.user_id);
        //    if(validUsuario is not null)
        //    {
        //        await _postsService.SavePostAsync(post);
        //        return Created("",post);
        //    }
        //    return BadRequest();
        //}

        [HttpPost("registrar_post")]
        public async Task<IActionResult> RegistrarPostComFiles(
            [FromForm] RegistrarPostRequest req
            //[FromForm] List<IFormFile>? files
            )
        {
            var validUsuario = await _usersService.GetById(req.user_id);
            if (validUsuario is null)
                return BadRequest("Usuário não encontrado");

            if (req is not null)
            {
                var post = req.Adapt<PostsEntity>();
                //var files = req.files;
                if (req?.files is not null)
                {
                    //var uploadBucket = await _contentService.UploadBucket(files);
                    post.list_contents = await _contentService.UploadBucket(req.files);
                }
                else
                    post.list_contents = [];
                await _postsService.SavePostAsync(post);
                return Created("",post);
            }
            return BadRequest();
        }

        [HttpGet("nickname/{nickname}")]
        public async Task<IActionResult> ListarPorNickname(string nickname, [FromQuery] PaginationParams paginationParams)
        {
            var posts = await _postsService.FindActivePostsByNickname(nickname, paginationParams.pageNumber, paginationParams.pageSize);
            Response.AddPaginationHeader(new PaginationHeader
           (posts.pageNumber, posts.pageSize, posts.totalCount, posts.totalPages));
            if (posts is not null)
                return Ok(posts);
            return BadRequest();
        }

        [HttpPut("editar_post/{id}")]
        public async Task<IActionResult> EditarPost(Guid id, [FromBody] RegistrarPostRequest body)
        {
            var post = _postsService.FindById(id);
            if(post is not null)
            {
                var postToSave = body.Adapt<PostsEntity>();
                await _postsService.EditarPost(postToSave);
                return Ok("");
            }
            return BadRequest("Erro ao editar o usuário!");
        }

        [HttpDelete("deletar_post/{id}")]
        public async Task<IActionResult> DeletarPost(Guid id)
        {
            var post = await _postsService.DeletarPost(id);
            return Ok(post);
        }

        [HttpDelete("delete_logico/{id}")]
        public async Task<IActionResult> DeleteLogico(Guid id)
        {
            var post = await _postsService.DeleteLogico(id);
            if (post is not null)
            {
                return Ok(post);
            }
            return BadRequest("Erro ao atualizar post");
        }

        [HttpPatch("alterar_ativo/{id}")]
        public async Task<IActionResult> AtivarPost(Guid id)
        {
            var post = await _postsService.AtivarPost(id);
            if (post is not null)
            {
                return Ok(post);
            }
            return BadRequest("Erro ao atualizar post");
        }
    }
}