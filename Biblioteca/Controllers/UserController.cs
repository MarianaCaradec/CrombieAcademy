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
            IEnumerable<User> users = _userService.GetAll();

            if(users == null)
            {
                return NotFound();
            }

            var mappedUser = users.Select(user => new User
            {
                Id = user.Id,
                Name = user.Name,
                UserType = user.UserType,
                MaxBooksAllowed = user.MaxBooksAllowed,
                Sales = user.Sales != null ? user.Sales : new List<Sales>()
            });

            return Ok(mappedUser);
        }

        //GET: UsuarioController/Details/5
        [HttpGet("{id}")]
        public User GetById(int id)
        {
            User response = _userService.GetUserByIdWithSales(id);
            return response;
        }

        [HttpGet("get/{type}")]
        public List<User> GetByType(string type)
        {
            List<User> response = _userService.GetUserByType(type);
            return response;
        }

        // POST: UsuarioController/Create
        [HttpPost]
        public UserDto Post([FromBody] UserDto newUser)
        {
            _userService.AddUser(newUser);
            return newUser;
        }

        // PUT: UsuarioController/Update/5
        [HttpPut("{idActualizar}")]
        public IActionResult Update([FromBody] UserDto user)
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
