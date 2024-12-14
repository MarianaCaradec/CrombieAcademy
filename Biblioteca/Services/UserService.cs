using BibliotecaAPIWeb.Data;
using BibliotecaAPIWeb.InterfacesServices;
using BibliotecaAPIWeb.Models;

namespace BibliotecaAPIWeb.Services
{
    public class UserService : IUserService
    {
        private readonly DataRepository _dataRepository;

        public UserService(DataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public UserDto AddUser(UserDto user)
        {
            if (user == null)
            {
                throw new ArgumentException("The book list cannot be null or empty.", nameof(user));
            }

            try
            {
                _dataRepository.CreateUser(user);
                return user;
            } catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to add users to the database.", ex);
            }

        }

        public IEnumerable<User> GetAll()
        {
            return _dataRepository.GetUsers();
        }

        public User GetUserById(int id)
        {
            return _dataRepository.GetUserById(id);
        }

        public List<User> GetUserByType(string userType)
        {
            List<User> users = _dataRepository.GetUserByType(userType);

            return users;
        }

        public UserDto Update(UserDto user)
        {
            if (user == null)
            {
                return null;
            }

            _dataRepository.UpdateUserById(user);
            return user;
        }

        public void Delete(int id)
        {
            _dataRepository.DeleteUser(id);
        }
    }
}
