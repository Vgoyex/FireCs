using Fire.Communication.Requests;
using Fire.Application.Services;
using Fire.Domain.Entities;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Fire.API.Params;
using Fire.API.Extensions;

namespace Fire.API.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {

        private readonly UsersService _usersService;

        public UsersController(UsersService usersService)
        {
            _usersService = usersService;
        }


        [HttpGet("listar_usuarios")]
        public async Task<IActionResult> ListarUsuarios([FromQuery]PaginationParams paginationParams)
        {
            var usuarios = await _usersService.GetAllAsync
                (paginationParams.pageNumber, paginationParams.pageSize);
            Response.AddPaginationHeader(new PaginationHeader
                (usuarios.pageNumber, usuarios.pageSize, usuarios.totalCount, usuarios.totalPages));
            return Ok(usuarios);
        }

        [HttpGet("user_id/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _usersService.GetById(id);
            if(user is not null)
            {
                return Ok(user);
            }
            return BadRequest();
        }

        [HttpPost("registrar_usuario")]
        public async Task<IActionResult> RegistrarUsuario([FromBody]RegisterUserRequest req)
        {
            var usuario = req.Adapt<UsersEntity>();
            var errosUsuario = await _usersService.ValidarRegistroUsuario(usuario);
            if (errosUsuario.ContainsKey("error_email"))
            {
                return BadRequest(errosUsuario);
            }
            string senhaHash = BCrypt.Net.BCrypt.HashPassword(usuario.password);
            usuario.password = senhaHash;
            await _usersService.SaveUserAsync(usuario);
            return Ok(usuario);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest req)
        {
            var userOptional = req.Adapt<UsersEntity>();
            var result = await _usersService.ValidLogin(userOptional);
            if(result.ContainsKey("erro")){
                return BadRequest(result);
            }
                return Ok(result);
        }


        [HttpPut("editar_usuario/{id}")]
        public async Task<IActionResult> EditarUsuario([FromBody]RegisterUserRequest req, Guid id)
        {
            var userOptional = req.Adapt<UsersEntity>();
            var usuario = await _usersService.GetById(id);
            if (usuario is not null)
            {
                string novaSenha = req.password;
                await _usersService.EditarUsuario(userOptional, novaSenha);
                return Ok(userOptional);
            }
            return BadRequest("Erro ao editar o usuário!");
        }


        [HttpDelete("deletar_usuario/{id}")]
        public async Task<IActionResult> DeletarUsuario(Guid id)
        {
            var result = await _usersService.DeleteUserById(id);
            return Ok(result);
        }
    }
}