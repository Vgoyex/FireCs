using Fire.Domain.Entities;
using Fire.Domain.Pagination;
using Fire.Infra.Repositories.Users;

namespace Fire.Application.Services
{
    public class UsersService
    {
        private readonly IUsersRepository _usersRepository;

        public UsersService(IUsersRepository repository)
        {
            _usersRepository = repository;
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

        public async Task<Dictionary<string,string>> ValidErrosUsuario(UsersEntity usuario)
        {
            var result = new Dictionary<string, string>();
            bool emailValido = ValidarEmail(usuario.email);
            var emailExists = await EmailExists(usuario);
            if (!emailValido)
                result["error_email"] = "Email inválido";
            if (emailExists)
                result["error_email"] = "Email já existe";

            return result;
        }

        public async Task<Dictionary<string,string>> ReturnLogin(UsersEntity usuario, string token)
        {
            var result = new Dictionary<string, string>();
            var userOptional = await FindByEmailAsync(usuario.email);

            if (userOptional is null)
            {
                result["erro_email"] = "Usuário não encontrado!";
                return result;
            }

            bool valido = BCrypt.Net.BCrypt.Verify(usuario.password, userOptional.password);
            if (!valido)
            {
                result["erro_credenciais"] = "Credenciais incorretas!";
                return result;
            }
            result["id"] = userOptional.id.ToString();
            result["token"] = token;
            result["name"] = userOptional.name;

            return result;
        }

        public async Task<bool> EmailExists(UsersEntity usuario)
        {
            var usuarioExists = await _usersRepository.FindByEmailAsync(usuario.email);
            if(usuarioExists is not null)
            {
                return true;
            }
            return false; 
        }

        public async Task<string> DeleteUserById(Guid id)
        {
            return await _usersRepository.DeleteUserById(id);
        }

        public async Task<UsersEntity> GetById(Guid id)
        {
            return await _usersRepository.GetById(id);
        }

        public async Task<UsersEntity> EditarUsuario(UsersEntity usuario)
        {
            var erros = await ValidErrosUsuario(usuario);
            if (erros is not null)
                return null;
            await _usersRepository.EditarUsuario(usuario);
            return usuario;
        }

        public static bool ValidarEmail(String email)
        {
            string regex = "^[A-Za-z0-9+_.-]+@[A-Za-z0-9.-]+$";
            return System.Text.RegularExpressions.Regex.IsMatch(email, regex);
        }
    }
}
