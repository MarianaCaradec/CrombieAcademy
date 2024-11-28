using BibliotecaAPIWeb.Models;

namespace BibliotecaAPIWeb.InterfacesServices
{
    public interface IUserService
    {
        User Update(User usuarioActualizado);
        UserDto AddUser(UserDto usuario);
        void Delete(int id);
        List<UserDto> GetAll();
        UserDto GetUserById(int id);
        List<UserDto> GetUserByType(string type);
    }
}