using Fire.Domain.Entities;
using Fire.Domain.Pagination;
using Fire.Infra.Repositories.Users;
using FireCs.JWT;

namespace Fire.Application.Services
{
    public class UsersService
    {
        private readonly IUsersRepository _usersRepository;

        private readonly JWTService _jwt;
        public UsersService(IUsersRepository repository, JWTService jwt)
        {
            _usersRepository = repository;
            _jwt = jwt;
        }

        public async Task<PagedList<UsersEntity>> GetAllAsync(int pageNumber, int pageSize)
        {
            var users =  await _usersRepository.GetAllAsync(pageNumber, pageSize);

            return new PagedList<UsersEntity>(users, pageNumber, pageSize, users.totalCount);
        }
        public async Task SaveUserAsync(UsersEntity user)
         {
            await _usersRepository.SaveUserAsync(user);
        }

        public async Task<UsersEntity> FindByEmailAsync(string email)
        {
            return await _usersRepository.FindByEmailAsync(email);
        }

        public async Task<string> DeleteUserById(Guid id)
        {
            return await _usersRepository.DeleteUserById(id);
        }

        public async Task<UsersEntity> GetById(Guid id)
        {
            return await _usersRepository.GetById(id);
        }


        public static bool ValidarEmail(String email)
        {
            string regex = "^[A-Za-z0-9+_.-]+@[A-Za-z0-9.-]+$";
            return System.Text.RegularExpressions.Regex.IsMatch(email, regex);
        }

        public async Task<Dictionary<string,string>> ValidarRegistroUsuario(UsersEntity usuario)
        {
            var result = new Dictionary<string, string>();
            bool emailValido = ValidarEmail(usuario.email);
            if (emailValido) { 
                var userExist = await FindByEmailAsync(usuario.email);
                if (userExist != null) {
                    result["error_email"] = "Email já existe";
                }
            }
            else
                result["error_email"] = "Email inválido";
                
            return result;
        }

        public async Task<Dictionary<string,string>> ValidLogin(UsersEntity usuario)
        {
            var result = new Dictionary<string, string>();
            var userOptional = await FindByEmailAsync(usuario.email);
            if (userOptional is null)
            {
                result["erro_email"] = "Email Usuário não encontrado!";
                result["erro"] = "true";
                return result;
            }

            bool validSenha = BCrypt.Net.BCrypt.Verify(usuario.password, userOptional.password);

            if (!validSenha)
            {
                result["erro_credenciais"] = "Credenciais incorretas!";
                result["erro"] = "true";
                return result;
            }

            var token = _jwt.GenerateToken(userOptional.id.ToString(), userOptional.email);

            result["id"] = userOptional.id.ToString();
            result["token"] = token;
            result["name"] = userOptional.name;

            return result;
        }


        public async Task<UsersEntity> EditarUsuario(UsersEntity usuario, string? novaSenha)
        {

            bool novaSenhaExist = false;
            string senhaHashed = "";
            string senhaAntigaHash = usuario.password;
            if (novaSenha != null && novaSenha != "")
            {
                bool mesmaSenha = BCrypt.Net.BCrypt.Verify(novaSenha, usuario.password);
                if (!mesmaSenha)
                {
                    senhaHashed = BCrypt.Net.BCrypt.HashPassword(novaSenha);
                    novaSenhaExist = true;
                }
                if (novaSenhaExist)
                    usuario.password = senhaHashed;
            }
            await _usersRepository.EditarUsuario(usuario);
            return usuario;
        }

       
    }
}
