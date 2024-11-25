using BibliotecaAPIWeb.Data;
using BibliotecaAPIWeb.InterfacesServices;
using BibliotecaAPIWeb.Models;
using BibliotecaAPIWeb.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BibliotecaAPIWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: UsuarioController
        [HttpGet]
        public IActionResult GetAll()
        {
            List<User> users = _userService.GetAll();

            var usersDto = users.Select(user => new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                UserType = user.UserType,
                BooksLoaned = user.BooksLoaned,
                MaxBooksAllowed = user.MaxBooksAllowed,
                CanAskALoan = user.CanAskALoan,
                LoanDays = user.LoanDays,
            }).ToList();

            return Ok(usersDto);
        }

        // GET: UsuarioController/Details/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            User response = _userService.GetUserById(id);
            return response;
        }

        // POST: UsuarioController/Create
        [HttpPost]
        public User Post([FromBody] User newUser)
        {
            _userService.AddUser(newUser);
            return newUser;
        }

        // PUT: UsuarioController/Update/5
        [HttpPut("{idActualizar}")]
        public IActionResult Update([FromBody] User user)
        {
            var updatedUser = _userService.Update(user);

            if (user == null)
            {
                return null;
            }

            return Ok(updatedUser);
        }

       // DELETE: UsuarioController/Delete/5
         [HttpDelete("{idEliminar}")]
        public void Delete(int id)
        {
            _userService.Delete(id);
            return;
         }
    }
}
