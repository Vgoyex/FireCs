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

        //private readonly ILogger<UsersController> _logger;
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

        //Teste
        [HttpGet("usuario_unico/{id}")]
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
            var errosUsuario = await _usersService.ValidErrosUsuario(usuario);
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
            string jwtToken = "TESTE";
            var userOptional = req.Adapt<UsersEntity>();
           
            var result = await _usersService.ReturnLogin(userOptional, jwtToken);
            return Ok(result);
        }


        [HttpPut("editar_usuario/{id}")]
        public async Task<IActionResult> EditarUsuario([FromBody]RegisterUserRequest req, Guid id)
        {
            var userOptional = req.Adapt<UsersEntity>();
            var usuario = _usersService.GetById(id);
            if(usuario is not null)
            {
                await _usersService.EditarUsuario(userOptional);
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





//@PostMapping("/login")
//    public ResponseEntity<?> login(@RequestBody UsersLoginRequest req)
//{
//    Optional<UsersModel> userOptional = usersService.findByEmail(req.email());
//    if (userOptional.isEmpty() ||
//            !BCrypt.checkpw(req.password(), userOptional.get().getPassword()))
//    {
//        return ResponseEntity.status(HttpStatus.UNAUTHORIZED).body("Credenciais inválidas.");
//    }
//    String jwtToken = JwtUtil.generate(req.email());

//    var result = usersService.returnLogin(userOptional, jwtToken);

//    return ResponseEntity.ok(result);
//}

//@PutMapping("/editar_usuario/{id}")
//    public ResponseEntity<?> editarUsuario(@PathVariable(value= "id")UUID id, @RequestBody UsersRegisterRequest req){
//        var usuario = usersService.findById(id);
//if (usuario.isEmpty())
//    return ResponseEntity.status(HttpStatus.NOT_FOUND).body("Id: " + id + " não encontrado!");
//var usuarioResult = usuario.get();
//BeanUtils.copyProperties(req, usuarioResult);
//return ResponseEntity.status(HttpStatus.OK).body(usersService.save(usuarioResult));
//    }

//    @DeleteMapping("/deletar_usuario/{id}")
//    public ResponseEntity<?> deletarUsuario(@PathVariable(value = "id") UUID id) {
//        //! Ao apagar usuário todos os posts vão junto. Possui ON DELETE CASCADE em posts
//        usersService.deleteById(id);
//return ResponseEntity.status(HttpStatus.OK).body("Id:" + id + " deletado!");
//    }