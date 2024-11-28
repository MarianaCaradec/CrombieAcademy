using BibliotecaAPIWeb.Data;
using BibliotecaAPIWeb.InterfacesServices;
using BibliotecaAPIWeb.Models;

namespace BibliotecaAPIWeb.Services
{
    public class UserService : IUserService
    {
        private readonly ExcelRepository _excelRepository;

        public UserService()
        {
            _excelRepository = new ExcelRepository();
        }

        public UserDto AddUser(UserDto user)
        {
            if (user == null)
            {
                throw new ArgumentException("The book list cannot be null or empty.", nameof(user));
            }

            try
            {
                _excelRepository.CreateUserData(user);
                return user;
            } catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to add books to the Excel database.", ex);
            }

        }

        public List<UserDto> GetAll()
        {
            return _excelRepository.GetDataUsers();
        }

        public UserDto GetUserById(int id)
        {
            return _excelRepository.GetUserById(id);
        }

        public List<UserDto> GetUserByType(string userType)
        {
            List<UserDto> users = _excelRepository.GetUserByType(userType);

            return users;
        }

        public User Update(User user)
        {
            if (user == null)
            {
                return null;
            }

            _excelRepository.UpdateUserDataById(user);
            return user;
        }

        public void Delete(int id)
        {
            _excelRepository.DeleteUser(id);
        }
    }
}
