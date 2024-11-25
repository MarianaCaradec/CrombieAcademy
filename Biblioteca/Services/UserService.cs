using BibliotecaAPIWeb.Data;
using BibliotecaAPIWeb.InterfacesServices;
using BibliotecaAPIWeb.Models;

namespace BibliotecaAPIWeb.Services
{
    public class UserService : IUserService
    {
        private readonly ExcelRepository _excelRepository;

        public UserService(ExcelRepository excelRepository)
        {
            _excelRepository = excelRepository;
        }

        public void AddUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentException("The book list cannot be null or empty.", nameof(user));
            }

            try
            {
                _excelRepository.CreateUserData(user);
            } catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to add books to the Excel database.", ex);
            }

        }

        public List<User> GetAll()
        {
            return _excelRepository.GetDataUsers();
        }

        public User GetUserByType(string userType)
        {
            User user = _excelRepository.GetUserByType(userType);

            return user;            
        }

        public User GetUserById(int id)
        {
            return _excelRepository.GetUserById(id);
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

        public bool Delete(int id)
        {
            var deletedUser = _excelRepository.GetUserById(id);

            if (deletedUser == null)
            {
                return false;
            }

            _excelRepository.GetDataUsers().Remove(deletedUser);
            return true;
        }
    }
}
