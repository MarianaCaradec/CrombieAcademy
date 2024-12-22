using BibliotecaAPIWeb.Models;

namespace BibliotecaAPIWeb.InterfacesServices
{
    public interface IUserService
    {
        UserDto Update(UserDto usuarioActualizado);
        UserDto AddUser(UserDto usuario);
        void Delete(int id);
        IEnumerable<User> GetAll();
        User GetUserById(int id);
        User GetUserByIdWithSales(int id);
        List<User> GetUserByType(string type);
    }
}