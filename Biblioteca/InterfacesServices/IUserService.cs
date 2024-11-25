using BibliotecaAPIWeb.Models;

namespace BibliotecaAPIWeb.InterfacesServices
{
    public interface IUserService
    {
        User Update(User usuarioActualizado);
        void AddUser(User usuario);
        bool Delete(int id);
        List<User> GetAll();
        User GetUserById(int id);
        User GetUserByType(string type);
    }
}